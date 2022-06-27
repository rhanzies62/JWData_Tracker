using System;
namespace JWDataTracker.Application.CongregationUser
{
    public class CongregationUserDto
    {
        public CongregationUserDto()
        {

        }
        public long CongregationUserId { get; set; }
        public long PublisherId { get; set; }
        public string Username { get; set; }
        public string Salt { get; set; }
        public string Password { get; set; }
        public long RoleId { get; set; }
        public string CreatedDate { get; set; }
        public long IsPasswordReset { get; set; }
        public string Email { get; set; }
        public long? CongregationId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
