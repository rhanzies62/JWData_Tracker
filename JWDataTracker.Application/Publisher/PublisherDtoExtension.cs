using JWDataTracker.Helper;
using JWDataTracker.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWDataTracker.Application.Publisher
{
    public static class PublisherDtoExtension
    {
        public static Response Validate(this PublisherDto model, IUnitOfWork unitOfWork)
        {
            var response = new Response(true, String.Empty);
            try
            {
                if (!unitOfWork.CongregationRepository.Get(c => c.CongregationId == model.CongregationId).Any())
                    return new Response(false, "Congregation is not existing");

                if(string.IsNullOrEmpty(model.FirstName) || string.IsNullOrEmpty(model.LastName))
                   return new Response(false, "First name and Last name is required");

                if(model.IsMs && model.IsElder)
                    return new Response(false, "Elder can't be Ministerial Servant at the same time and vice versa");

                if(model.IsRp && model.IsUnBaptized)
                    return new Response(false, "Regular pioneer can't be unbaptized publisher");

                if(model.PublisherId > 0)
                {
                    if(unitOfWork.PublisherRepository.Get(i => i.PublisherId != model.PublisherId && 
                                                          i.FirstName.Trim().ToUpper() == model.FirstName.Trim().ToUpper() &&
                                                          i.LastName.Trim().ToUpper() == model.LastName.Trim().ToUpper()).Any())
                    {
                        return new Response(false, "First name and Last name is already existing");
                    }
                } else
                {
                    if (unitOfWork.PublisherRepository.Get(i => i.FirstName.Trim().ToUpper() == model.FirstName.Trim().ToUpper() &&
                                      i.LastName.Trim().ToUpper() == model.LastName.Trim().ToUpper()).Any())
                    {
                        return new Response(false, "First name and Last name is already existing");
                    }
                }
            }
            catch (Exception e)
            {
                response = new Response(false, e.GetBaseException().Message);
            }
            return response;
        }
    }
}
