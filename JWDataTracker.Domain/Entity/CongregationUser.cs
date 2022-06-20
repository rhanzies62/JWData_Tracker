using System;
using System.Collections.Generic;

namespace JWDataTracker.Infrastructure
{
    public partial class CongregationUser
    {
        public long CongergationUserId { get; set; }
        public string FirstName { get; set; }
        public long LastName { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public long IsActive { get; set; }
        public long? CongregationId { get; set; }
    }
}
