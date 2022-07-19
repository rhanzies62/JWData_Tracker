using System;
using System.Collections.Generic;

namespace JWDataTracker.Infrastructure
{
    public partial class Publisher
    {
        public long PublisherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CreatedDate { get; set; }
        public long? CongregationId { get; set; }
        public long IsElder { get; set; }
        public long IsMs { get; set; }
        public long IsRp { get; set; }
        public long IsUnBaptized { get; set; }
        public long GroupNumber { get; set; }
        public long IsBaptized { get; set; }

        public virtual Congregation Congregation { get; set; }
    }
}
