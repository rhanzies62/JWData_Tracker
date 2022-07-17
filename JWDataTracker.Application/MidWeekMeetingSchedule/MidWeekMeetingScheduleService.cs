using JWDataTracker.Application.Publisher;
using JWDataTracker.Helper;
using JWDataTracker.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using entity = JWDataTracker.Infrastructure;
namespace JWDataTracker.Application.MidWeekMeetingSchedule
{
    public class MidWeekMeetingScheduleService : BaseService, IMidWeekMeetingScheduleService
    {
        public MidWeekMeetingScheduleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Response Add(MidWeekMeetingScheduleDto model)
        {
            var response = new Response(true, string.Empty);
            try
            {
                response = model.Validate();
                if (response.IsSuccess)
                {
                    var midWeekSchedule = new entity.MidWeekSchedule()
                    {
                        Attendance = 0,
                        CongregationId = model.CongregationId,
                        CreatedBy = model.CreatedBy,
                        ScheduledDate = model.ScheduledDate
                    };
                    unitOfWork.MidWeekScheduleRepository.Insert(midWeekSchedule);
                    unitOfWork.Save();


                    foreach (var mwsi in model.MidWeekScheduleItems)
                    {
                        unitOfWork.MidWeekScheduleItemRepository.Insert(new entity.MidWeekScheduleItem
                        {
                            Category = mwsi.Category,
                            HallNumber = mwsi.HallNumber,
                            MidWeekScheduleId = midWeekSchedule.MidWeekScheduleId,
                            PartnerPublisherId = mwsi.PartnerPublisherId,
                            PublisherId = mwsi.PublisherId,
                            Role = mwsi.Role
                        });
                    }
                    unitOfWork.Save();
                }
            }
            catch (Exception e)
            {
                response = new Response(false, e.GetBaseException().Message);
            }
            return response;
        }

        public Response Delete(long midWeekScheduleId)
        {
            var response = new Response(true, string.Empty);
            try
            {

            }
            catch (Exception e)
            {
                response = new Response(false, e.GetBaseException().Message);
            }
            return response;
        }

        public Response Edit(MidWeekMeetingScheduleDto model)
        {
            var response = new Response(true, string.Empty);
            try
            {
                response = model.Validate();
                if (response.IsSuccess)
                {
                    var midWeekSchedule = unitOfWork.MidWeekScheduleRepository.GetByID(model.MidWeekScheduleId);
                    if (midWeekSchedule == null) return new Response(false, "Can't find Mid Week Schedule");
                    foreach (var mwsi in model.MidWeekScheduleItems)
                    {
                        entity.MidWeekScheduleItem midWeekScheduleItem;
                        if (mwsi.MidWeekScheduleItemId == 0)
                        {
                            midWeekScheduleItem = new entity.MidWeekScheduleItem();
                            unitOfWork.MidWeekScheduleItemRepository.Insert(midWeekScheduleItem);
                        }
                        else
                        {
                            midWeekScheduleItem = unitOfWork.MidWeekScheduleItemRepository.GetByID(mwsi.MidWeekScheduleItemId);
                            unitOfWork.MidWeekScheduleItemRepository.Update(midWeekScheduleItem);
                        }
                        midWeekScheduleItem.Category = mwsi.Category;
                        midWeekScheduleItem.HallNumber = mwsi.HallNumber;
                        midWeekScheduleItem.MidWeekScheduleId = midWeekSchedule.MidWeekScheduleId;
                        midWeekScheduleItem.PartnerPublisherId = mwsi.PartnerPublisherId;
                        midWeekScheduleItem.PublisherId = mwsi.PublisherId;
                        midWeekScheduleItem.Role = mwsi.Role;
                        unitOfWork.Save();
                    }
                }
            }
            catch (Exception e)
            {
                response = new Response(false, e.GetBaseException().Message);
            }
            return response;
        }

        public MidWeekMeetingScheduleDto GetMidWeekScheduleByDate(DateTime date, int congregationId)
        {
            var query = (from mws in unitOfWork.MidWeekScheduleRepository.Get()
                         where DateTime.Parse(mws.ScheduledDate).Month == date.Month &&
                               DateTime.Parse(mws.ScheduledDate).Date == date.Date &&
                               DateTime.Parse(mws.ScheduledDate).Year == date.Year &&
                               mws.CongregationId == congregationId
                         select new MidWeekMeetingScheduleDto
                         {
                             Attendance = mws.Attendance,
                             MidWeekScheduleId = mws.MidWeekScheduleId,
                             ScheduledDate = mws.ScheduledDate,
                             MidWeekScheduleItems = (from mwsi in unitOfWork.MidWeekScheduleItemRepository.Get()
                                                     select new MidWeekScheduleItemDto
                                                     {
                                                         HallNumber = mwsi.HallNumber,
                                                         MidWeekScheduleId = mwsi.MidWeekScheduleId,
                                                         MidWeekScheduleItemId = mwsi.MidWeekScheduleId,
                                                         PartnerPublisherId = mwsi.MidWeekScheduleId,
                                                         PublisherId = mwsi.PublisherId,
                                                         Role = mwsi.Role,
                                                         Publisher = (from p in unitOfWork.PublisherRepository.Get()
                                                                      where p.PublisherId == mwsi.PublisherId
                                                                      select new PublisherDto
                                                                      {
                                                                          PublisherId = p.PublisherId,
                                                                          FirstName = p.FirstName,
                                                                          LastName = p.LastName
                                                                      }).FirstOrDefault(),
                                                         PartnerPublisher = (from p in unitOfWork.PublisherRepository.Get()
                                                                             where p.PublisherId == mwsi.PartnerPublisherId
                                                                             select new PublisherDto
                                                                             {
                                                                                 PublisherId = p.PublisherId,
                                                                                 FirstName = p.FirstName,
                                                                                 LastName = p.LastName
                                                                             }).FirstOrDefault()
                                                     })

                         }).FirstOrDefault();
            return query;
        }

        public IEnumerable<MidWeekMeetingScheduleDto> List(long congregatonId)
        {
            throw new NotImplementedException();
        }
    }
}
