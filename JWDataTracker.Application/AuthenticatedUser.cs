using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWDataTracker.Application
{
    public struct CustomClaimTypes
    {
        public const string USERID = "userid";
        public const string CLIENTID = "clientId";
        public const string USERTYPEID = "userTypeId";
        public const string USERNAME = "userName";
        public const string LANGUAGEID = "languageId";
        public const string NOTIFICATIONTOKEN = "notificationToken";
    }
    public interface IAuthenticatedUser
    {
        long Id { get; }

    }
    public class AuthenticatedUser : IAuthenticatedUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthenticatedUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public long Id => GetUserId();

        private int GetUserId()
        {
            var subject = _httpContextAccessor.HttpContext
                              .User.Claims
                              .FirstOrDefault(claim => claim.Type == CustomClaimTypes.USERID).Value;

            return int.TryParse(subject, out var id) ? id : 1;
        }
    }
}
