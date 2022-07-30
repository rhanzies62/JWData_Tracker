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
            var publisher = unitOfWork.PublisherRepository.Get(p => p.PublisherId == publisherId).Select(p => new PublisherDto
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
                Status = p.Status
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
                IsRp = p.IsRp == 1,
                LastName = p.LastName,
                PublisherId = p.PublisherId,
                Gender = p.Gender,
                Privilege = p.Privilege,
                Status = p.Status
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
