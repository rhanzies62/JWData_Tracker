using JWDataTracker.Domain.Grid;
using JWDataTracker.Helper;
using JWDataTracker.Infrastructure.Repository;
using Newtonsoft.Json;
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
                        IsRp = model.IsRp ? 1 : 0,
                        LastName = model.LastName,
                        Gender = model.Gender,
                        Privilege = model.Privilege,
                        Status = model.Status
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
                    publisherEntity.Gender = model.Gender;
                    publisherEntity.IsRp = model.IsRp ? 1 : 0;
                    publisherEntity.Privilege = model.Privilege;
                    publisherEntity.Status = model.Status;
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
            var publisher = (from p in unitOfWork.PublisherRepository.Get()
                             join gender in unitOfWork.LookUpRepository.Get() on p.Gender equals gender.LookUpId
                             join status in unitOfWork.LookUpRepository.Get() on p.Status equals status.LookUpId
                             join leftPrivilege in unitOfWork.LookUpRepository.Get() on p.Privilege equals leftPrivilege.LookUpId into _leftprivilege
                             from lprivilege in _leftprivilege.DefaultIfEmpty()
                             where p.PublisherId == publisherId
                             select new PublisherDto
                             {
                                 CreatedDate = JsonConvert.DeserializeObject<DateTime>(p.CreatedDate),
                                 CongregationId = p.CongregationId,
                                 FirstName = p.FirstName,
                                 GroupNumber = p.GroupNumber,
                                 IsRp = p.IsRp == 1,
                                 LastName = p.LastName,
                                 Gender = p.Gender,
                                 Privilege = p.Privilege,
                                 PublisherId = p.PublisherId,
                                 Status = p.Status,
                                 GenderLookUp = new LookUp.LookUpDto { Code = gender.Code, LookUpId = gender.LookUpId, SortOrder = gender.SortOrder, Text = gender.Text },
                                 StatusLookUp = new LookUp.LookUpDto { Code = status.Code, LookUpId = status.LookUpId, SortOrder = status.SortOrder, Text = status.Text },
                                 PrivilegeLookUp = lprivilege != null ? new LookUp.LookUpDto { Code = lprivilege.Code, LookUpId = lprivilege.LookUpId, SortOrder = lprivilege.SortOrder, Text = lprivilege.Text } : null,
                             }).FirstOrDefault();

            return publisher ?? new PublisherDto();
        }

        public IQueryable<PublisherDto> List()
        {

            return (from p in unitOfWork.Database.Publishers

                    join leftprivilege in unitOfWork.Database.LookUps on p.Privilege equals leftprivilege.LookUpId into _leftprivilege
                    from lprivilege in _leftprivilege.DefaultIfEmpty()

                    join gender in unitOfWork.Database.LookUps on p.Gender equals gender.LookUpId
                    join status in unitOfWork.Database.LookUps on p.Status equals status.LookUpId
                    select new PublisherDto
                    {
                        UserStatus = status.Text,
                        FirstName = p.FirstName,
                        GroupNumber = p.GroupNumber,
                        IsRp = p.IsRp == 1,
                        PublisherId = p.PublisherId,
                        LastName = p.LastName,
                        UserGender = gender.Text,
                        UserPrivilege = lprivilege != null ? lprivilege.Text : ""
                    });
        }

        public IQueryable<PublisherRecentPartGrid> ListPublisherRecentPartsGrid(long? publisherId)
        {
            return (from mwsi in unitOfWork.Database.MidWeekScheduleItems
                    join mwcategory in unitOfWork.Database.LookUps on mwsi.Category equals mwcategory.LookUpId
                    join mwrole in unitOfWork.Database.LookUps on mwsi.Role equals mwrole.LookUpId
                    join mws in unitOfWork.Database.MidWeekSchedules on mwsi.MidWeekScheduleId equals mws.MidWeekScheduleId
                    where mwsi.PublisherId == publisherId ||
                          mwsi.PartnerPublisherId == publisherId ||
                          mwsi.ReplacementPublisherId == publisherId ||
                          mwsi.ReplacementPartnerPublisherId == publisherId
                    select new PublisherRecentPartGrid
                    {
                        Category = mwcategory.Text,
                        IsPartner = mwsi.PartnerPublisherId == publisherId,
                        part = mwrole.Text,
                        ScheduledDate = mws.ScheduledDate
                    });
        }

        public GridResultGeneric<PublisherGridDto> ListPublishers(GridFilter filter)
        {
            return unitOfWork.DataGridRepository.ListPublishers(filter);
        }

        public GridResultGeneric<PublisherRecentPartGrid> ListPublisherRecentParts(GridFilter filter, int publisherId)
        {
            return unitOfWork.DataGridRepository.ListPublisherRecentPart(filter, publisherId);
        }
    }
}
