using JWDataTracker.Helper;
using JWDataTracker.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using entity = JWDataTracker.Infrastructure;
namespace JWDataTracker.Application.Congregation
{
    public class CongregationService : BaseService, ICongregationService
    {
        public CongregationService(IUnitOfWork _unitOfWork) : base(_unitOfWork)
        {

        }

        public Response Add(CongregationDto model)
        {
            var response = new Response(true, String.Empty);
            try
            {
                if (unitOfWork.CongregationRepository.Get(i => i.Name.Trim().ToUpper() == model.Name.Trim().ToUpper()) != null)
                    return new Response(false, "Congregation Name Already Exist");

                var entity = new entity.Congregation()
                {
                    CreatedDate = DateTime.UtcNow.ToLongDateString(),
                    Name = model.Name
                };

                unitOfWork.CongregationRepository.Insert(entity);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                response = new Response(false, e.GetBaseException().Message);
            }
            return response;
        }

        public Response Delete(int congregationId)
        {
            var response = new Response(true, String.Empty);
            try
            {
                var entity = unitOfWork.CongregationRepository.Get(i => i.CongregationId == congregationId);
                if (entity == null)
                    return new Response(false, "Congregation is not existing");

                unitOfWork.CongregationRepository.Delete(congregationId);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                response = new Response(false, e.GetBaseException().Message);
            }
            return response;
        }

        public Response Edit(CongregationDto model)
        {
            var response = new Response(true, String.Empty);
            try
            {
                var entity = unitOfWork.CongregationRepository.Get(i => i.CongregationId == model.CongregationId).FirstOrDefault();
                if (entity == null)
                    return new Response(false, "Congregation is not existing");

                entity.Name = model.Name;
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                response = new Response(false, e.GetBaseException().Message);
            }
            return response;
        }

        public CongregationDto GetById(int congregationId)
        {
            return unitOfWork.CongregationRepository.Get(i => i.CongregationId == congregationId).Select(i => new CongregationDto
            {
                Name = i.Name,
                CongregationId = i.CongregationId
            }).FirstOrDefault();
        }

        public IEnumerable<CongregationDto> List()
        {
            return unitOfWork.CongregationRepository.Get().Select(i => new CongregationDto
            {
                Name = i.Name,
                CongregationId = i.CongregationId
            });
        }
    }
}
