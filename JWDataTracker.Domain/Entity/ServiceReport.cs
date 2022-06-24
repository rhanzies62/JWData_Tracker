using System;
using System.Collections.Generic;

namespace JWDataTracker.Infrastructure
{
    public partial class ServiceReport
    {
        public long ServiceReportId { get; set; }
        public long PublisherId { get; set; }
        public string Date { get; set; }
        public long Hours { get; set; }
        public long Placements { get; set; }
        public long VideoShowing { get; set; }
        public long BibleStudy { get; set; }
        public long ReturnVisit { get; set; }
    }
}
