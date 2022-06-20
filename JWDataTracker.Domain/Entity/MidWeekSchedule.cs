using System;
using System.Collections.Generic;

namespace JWDataTracker.Infrastructure
{
    public partial class MidWeekSchedule
    {
        public long MidWeekScheduleId { get; set; }
        public string ScheduledDate { get; set; }
        public long? CreatedBy { get; set; }
    }
}
