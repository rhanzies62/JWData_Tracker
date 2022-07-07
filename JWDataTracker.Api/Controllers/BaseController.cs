using JWDataTracker.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWDataTracker.Api.Controllers
{
    [Authorize, CustomAuthorizeFilter, ApiController, Route("api/[controller]/[action]")]
    public class BaseController : ControllerBase
    {
        public readonly IAuthenticatedUser _currentUser;
        public BaseController(IAuthenticatedUser currentUser)
        {
            _currentUser = currentUser;
        }
    }
}
