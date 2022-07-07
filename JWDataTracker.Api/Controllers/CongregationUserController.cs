using JWDataTracker.Application.CongregationUser;
using JWDataTracker.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWDataTracker.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CongregationUserController : ControllerBase
    {
        private readonly ICongregationUserService _congregationUserService;
        public CongregationUserController(ICongregationUserService congregationUserService)
        {
            _congregationUserService = congregationUserService;
        }

        [HttpPost(Name = "AddUser")]
        public Response AddUser(CongregationUserDto model)
        {
            var response = _congregationUserService.Add(model);
            return response;
        }
    }
}
