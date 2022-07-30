using System;
using System.Collections.Generic;

namespace JWDataTracker.Infrastructure
{
    public partial class MidWeekScheduleItem
    {
        public long MidWeekScheduleItemId { get; set; }
        public long Role { get; set; }
        public string HallNumber { get; set; }
        public long MidWeekScheduleId { get; set; }
        public long Category { get; set; }
        public long? PartnerPublisherId { get; set; }
        public long? PublisherId { get; set; }
        public long? Notes { get; set; }
        public long? ReplacementPublisherId { get; set; }
        public long? ReplacementPartnerPublisherId { get; set; }
    }
}
