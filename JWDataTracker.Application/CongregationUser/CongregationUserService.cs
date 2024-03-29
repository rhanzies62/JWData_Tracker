﻿using JWDataTracker.Helper;
using JWDataTracker.Infrastructure.Repository;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using entity = JWDataTracker.Infrastructure;

namespace JWDataTracker.Application.CongregationUser
{
    public class CongregationUserService : BaseService, ICongregationUserService
    {
        public CongregationUserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public Response Add(CongregationUserDto model)
        {
            var response = new Response(true, string.Empty);
            try
            {
                if (unitOfWork.CongregationRepository.GetByID(model.CongregationId) == null)
                    return response = new Response(false, "Congregation is not existing");

                if (!unitOfWork.PublisherRepository.Get(i => i.PublisherId == model.PublisherId).Any())
                    return response = new Response(false, "Publisher is not existing");

                if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
                    return response = new Response(false, "Username/Password is required");

                if (model.RoleId == 0)
                    return response = new Response(false, "Role Id is not specified");

                if (unitOfWork.CongregationUserRepository.Get(i => i.Username == model.Username).Any())
                    return response = new Response(false, "Username already exists");

                var salt = GenerateSalt();
                var entity = new entity.CongregationUser()
                {
                    CreatedDate = JsonConvert.SerializeObject(DateTime.UtcNow),
                    Email = model.Email,
                    IsPasswordReset = 0,
                    Password = HashPassword(model.Password,salt),
                    PublisherId = model.PublisherId,
                    RoleId = model.RoleId,
                    Username = model.Username,
                    Salt = Convert.ToBase64String(salt),
                    CongregationId = model.CongregationId
                };

                unitOfWork.CongregationUserRepository.Insert(entity);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                response = new Response(false, e.GetBaseException().Message);
            }
            return response;
        }
        public Response Edit(CongregationUserDto model)
        {
            var response = new Response(true, string.Empty);
            try
            {
                var entity = unitOfWork.CongregationUserRepository.GetByID(model.CongregationUserId);
                if (entity == null)
                    return new Response(false, "User not found");

                if (unitOfWork.CongregationUserRepository.Get(i => i.Username == model.Username).Any())
                    return response = new Response(false, "Username already exists");

                entity.Username = model.Username;
                entity.CongregationId = model.CongregationId;
                entity.Email = model.Email;
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                response = new Response(false, e.GetBaseException().Message);
                response.HasException = true;
            }
            return response;
        }
        public IEnumerable<CongregationUserDto> List()
        {
            return (from p in unitOfWork.PublisherRepository.Get()
                    join cu in unitOfWork.CongregationUserRepository.Get() on p.PublisherId equals cu.PublisherId
                    select new CongregationUserDto
                    {
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Username = cu.Username,
                        CongregationUserId = cu.CongregationUserId,
                        Email = cu.Email
                    });
        }
        public Response Delete(int congregationUserId)
        {
            var response = new Response(true, string.Empty);
            try
            {
                unitOfWork.CongregationUserRepository.Delete(congregationUserId);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                response = new Response(false, e.GetBaseException().Message);
            }
            return response;
        }
        public CongregationUserDto GetById(int congregationUserId)
        {
            return unitOfWork.CongregationUserRepository.Get(i => i.CongregationId == congregationUserId)
                    .Select(i => new CongregationUserDto
                    {
                        CongregationId = i.CongregationId,
                        CongregationUserId = i.CongregationUserId,
                        CreatedDate = JsonConvert.DeserializeObject<DateTime>(i.CreatedDate),
                        Email = i.Email,
                        RoleId = i.RoleId,
                        IsPasswordReset = i.IsPasswordReset,
                        PublisherId = i.PublisherId,
                        Username = i.Username,
                    }).FirstOrDefault();
        }
        public Response GetUserByUsernameAndPassword(CongregationUserDto model)
        {
            var response = new Response(true, String.Empty);
            try
            {
                var user = (from cu in unitOfWork.CongregationUserRepository.Get()
                            join p in unitOfWork.PublisherRepository.Get() on cu.PublisherId equals p.PublisherId
                            where cu.Username == model.Username
                            select new CongregationUserDto
                            {
                                FirstName = p.FirstName,
                                Email = cu.Email,
                                CongregationId = cu.CongregationId,
                                CongregationUserId = cu.CongregationUserId,
                                CreatedDate = JsonConvert.DeserializeObject<DateTime>(cu.CreatedDate),
                                IsPasswordReset = cu.IsPasswordReset,
                                LastName = p.LastName,
                                PublisherId = p.PublisherId,
                                RoleId = cu.RoleId,
                                Username = cu.Username,
                                Salt = cu.Salt,
                                Password = cu.Password
                            }).FirstOrDefault();

                if (user == null)
                    return new Response(false, "User not found");

                var hashPassword = HashPassword(model.Password, Convert.FromBase64String(user.Salt));
                if (hashPassword != user.Password)
                    return new Response(false, "Incorrect username/password");

                response.Data = user;
            }
            catch (Exception e)
            {
                response = new Response(false, "Error Occured");
            }
            return response;
        }
        private string HashPassword(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
        }
        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            return salt;
        }
    }
}
