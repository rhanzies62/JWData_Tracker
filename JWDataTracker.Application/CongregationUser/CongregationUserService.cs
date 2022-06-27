using JWDataTracker.Helper;
using JWDataTracker.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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
                if (unitOfWork.CongregationRepository.GetByID(model.CongregationUserId) == null)
                    return response = new Response(false, "Congregation is not existing");

                if (unitOfWork.CongregationUserRepository.Get(i => i.Username == model.Username).Any())
                    return response = new Response(false, "Username already exists");

                var entity = new entity.CongregationUser()
                {
                    CreatedDate = DateTime.UtcNow.ToLongDateString(),
                    Email = model.Email,
                    IsPasswordReset = 0,
                    Password = model.Password,
                    PublisherId = model.PublisherId,
                    RoleId = model.RoleId,
                    Username = model.Username,
                    Salt = model.Salt,
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
            }
            catch (Exception e)
            {
                response = new Response(false, e.GetBaseException().Message);
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
                        CreatedDate = i.CreatedDate,
                        Email = i.Email,
                        RoleId = i.RoleId,
                        IsPasswordReset = i.IsPasswordReset,
                        PublisherId = i.PublisherId,
                        Username = i.Username,
                    }).FirstOrDefault();
        }
    }
}
