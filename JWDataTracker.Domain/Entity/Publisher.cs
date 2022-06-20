using System;
using System.Collections.Generic;

namespace JWDataTracker.Infrastructure
{
    public partial class Publisher
    {
        public Publisher()
        {
            MidWeekScheduleItemPartnerPublishers = new HashSet<MidWeekScheduleItem>();
            MidWeekScheduleItemPublishers = new HashSet<MidWeekScheduleItem>();
        }

        public long PublisherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CreatedDate { get; set; }
        public long? CongregationId { get; set; }
        public long IsElder { get; set; }
        public long IsMs { get; set; }
        public long IsRp { get; set; }

        public virtual Congregation Congregation { get; set; }
        public virtual ICollection<MidWeekScheduleItem> MidWeekScheduleItemPartnerPublishers { get; set; }
        public virtual ICollection<MidWeekScheduleItem> MidWeekScheduleItemPublishers { get; set; }
    }
}
