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
           
            }catch(Exception e)
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

            }
            catch (Exception e)
            {
                response = new Response(false, e.GetBaseException().Message);
            }
            return response;
        }
        public IEnumerable<CongregationUserDto> List()
        {
            return null;
        }
        public Response Delete(int congregationId)
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
        public CongregationUserDto GetById(int congregationId)
        {
            return null;
        }
    }
}
