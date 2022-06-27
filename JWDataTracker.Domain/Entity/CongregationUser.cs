using System;
using System.Collections.Generic;

namespace JWDataTracker.Infrastructure
{
    public partial class CongregationUser
    {
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
    }
}
