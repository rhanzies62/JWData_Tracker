using System;
using System.Collections.Generic;

namespace JWDataTracker.Infrastructure
{
    public partial class MidWeekScheduleItem
    {
        public long MidWeekScheduleItemId { get; set; }
        public string Role { get; set; }
        public long PublisherId { get; set; }
        public long PartnerPublisherId { get; set; }
        public string HallNumber { get; set; }
        public long MidWeekScheduleId { get; set; }
        public string Category { get; set; }

        public virtual Publisher PartnerPublisher { get; set; }
        public virtual Publisher Publisher { get; set; }
    }
}
