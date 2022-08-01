using System;
namespace JWDataTracker.Application.Privilege
{
    public class PrivilegeDto
    {
        public PrivilegeDto()
        {
        }
        public long PrivilegeId { get; set; }
        public long PublisherId { get; set; }
        public long Role { get; set; }
    }
}

