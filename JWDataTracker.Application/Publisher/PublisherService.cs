﻿using JWDataTracker.Application.MidWeekMeetingSchedule;
using JWDataTracker.Domain.Grid;
using JWDataTracker.Helper;
using JWDataTracker.Infrastructure.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using entity = JWDataTracker.Infrastructure;
namespace JWDataTracker.Application.Publisher
{
    public class PublisherService : BaseService, IPublisherService
    {
        public PublisherService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Response Add(PublisherDto model)
        {
            var response = new Response(true, String.Empty);
            try
            {
                response = model.Validate(unitOfWork);
                if (response.IsSuccess)
                {
                    var publisherEntity = new entity.Publisher()
                    {
                        CreatedDate = JsonConvert.SerializeObject(DateTime.UtcNow),
                        CongregationId = model.CongregationId,
                        FirstName = model.FirstName,
                        GroupNumber = model.GroupNumber,
                        IsElder = model.IsElder ? 1 : 0,
                        IsMs = model.IsMs ? 1 : 0,
                        IsRp = model.IsRp ? 1 : 0,
                        IsUnBaptized = model.IsUnBaptized ? 1 : 0,
                        LastName = model.LastName
                    };
                    unitOfWork.PublisherRepository.Insert(publisherEntity);
                    unitOfWork.Save();
                    response = new Response(true, String.Empty);
                }
            }
            catch (Exception e)
            {
                response = new Response(false, e.GetBaseException().Message);
            }
            return response;
        }

        public Response Delete(long publisherId)
        {
            var response = new Response(true, String.Empty);
            try
            {
                unitOfWork.PublisherRepository.Delete(publisherId);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                response = new Response(false, e.GetBaseException().Message);
            }
            return response;
        }

        public Response Edit(PublisherDto model)
        {
            var response = new Response(true, String.Empty);
            try
            {
                var publisherEntity = unitOfWork.PublisherRepository.GetByID(model.PublisherId);
                if (publisherEntity == null)
                    return new Response(false, "Publisher is not existing");

                response = model.Validate(unitOfWork);
                if (response.IsSuccess)
                {
                    publisherEntity.CongregationId = model.CongregationId;
                    publisherEntity.FirstName = model.FirstName;
                    publisherEntity.LastName = model.LastName;
                    publisherEntity.GroupNumber = model.GroupNumber;
                    publisherEntity.IsElder = model.IsElder ? 1 : 0;
                    publisherEntity.IsMs = model.IsMs ? 1 : 0;
                    publisherEntity.IsRp = model.IsRp ? 1 : 0;
                    publisherEntity.IsUnBaptized = model.IsUnBaptized ? 1 : 0;
                    unitOfWork.Save();
                }
            }
            catch (Exception e)
            {
                response = new Response(false, e.GetBaseException().Message);
            }
            return response;
        }

        public PublisherDto GetById(int publisherId)
        {
            var publisher = unitOfWork.PublisherRepository.Get(p => p.PublisherId == publisherId).Select(p => new PublisherDto
            {
                CreatedDate = JsonConvert.DeserializeObject<DateTime>(p.CreatedDate),
                CongregationId = p.CongregationId,
                FirstName = p.FirstName,
                GroupNumber = p.GroupNumber,
                IsElder = p.IsElder == 1,
                IsMs = p.IsMs == 1,
                IsRp = p.IsRp == 1,
                IsUnBaptized = p.IsUnBaptized == 1,
                LastName = p.LastName
            }).FirstOrDefault();

            return publisher ?? new PublisherDto();
        }

        public IEnumerable<PublisherDto> List()
        {
            return unitOfWork.PublisherRepository.Get().Select(p => new PublisherDto
            {
                CreatedDate = JsonConvert.DeserializeObject<DateTime>(p.CreatedDate),
                CongregationId = p.CongregationId,
                FirstName = p.FirstName,
                GroupNumber = p.GroupNumber,
                IsElder = p.IsElder == 1,
                IsMs = p.IsMs == 1,
                IsRp = p.IsRp == 1,
                IsUnBaptized = p.IsUnBaptized == 1,
                LastName = p.LastName,
                PublisherId = p.PublisherId
            });
        }

        public GridResultGeneric<PublisherGridDto> ListPublishers(GridFilter filter)
        {
            return unitOfWork.DataGridRepository.ListPublishers(filter);
        }

        public GridResultGeneric<PublisherRecentPartGrid> ListPublisherRecentParts(GridFilter filter,int publisherId)
        {
            return unitOfWork.DataGridRepository.ListPublisherRecentPart(filter, publisherId);
        }
    }
}
