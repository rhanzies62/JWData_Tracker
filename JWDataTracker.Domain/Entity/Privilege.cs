using System;
using System.Collections.Generic;

namespace JWDataTracker.Infrastructure
{
    public partial class Privilege
    {
        public long PrivilegeId { get; set; }
        public long PublisherId { get; set; }
        public long Role { get; set; }
    }
}
