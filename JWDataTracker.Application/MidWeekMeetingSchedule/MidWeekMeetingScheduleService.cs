using JWDataTracker.Application.Publisher;
using JWDataTracker.Helper;
using JWDataTracker.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                                                                      select new PublisherDto { 
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
