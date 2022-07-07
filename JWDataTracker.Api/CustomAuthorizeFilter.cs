using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;

namespace JWDataTracker.Api
{
    public class CustomAuthorizeFilter : TypeFilterAttribute
    {
        public CustomAuthorizeFilter() : base(typeof(AuthorizeActionFilter))
        {
        }
    }

    public class AuthorizeActionFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext actionContext)
        {
            if (actionContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var claim = actionContext.HttpContext.Request.Headers["Claims"].FirstOrDefault();

                var genericIdentity = new GenericIdentity("test");
                genericIdentity.AddClaims(actionContext.HttpContext.User.Claims);

                var genericPrincipal = new GenericPrincipal(genericIdentity, null);
                Thread.CurrentPrincipal = genericPrincipal;
                actionContext.HttpContext.User.AddIdentity(genericIdentity);
                return;

                //if (!string.IsNullOrWhiteSpace(claim))
                //{
                //    var jwtToken = tokenHandler.ReadJwtToken(claim);

                //    var userName = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;

                //    if (!string.IsNullOrEmpty(userName))
                //    {
                //        var genericIdentity = new GenericIdentity(userName);
                //        genericIdentity.AddClaims(jwtToken.Claims);

                //        var genericPrincipal = new GenericPrincipal(genericIdentity, null);
                //        Thread.CurrentPrincipal = genericPrincipal;
                //        actionContext.HttpContext.User.AddIdentity(genericIdentity);
                //        return;
                //    }
                //}
            }
            else
            {
                actionContext.Result = new ForbidResult();
            }
        }
    }
}
