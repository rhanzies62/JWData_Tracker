using System;
using System.Collections.Generic;

namespace JWDataTracker.Infrastructure
{
    public partial class WeekendMeetingSchedule
    {
        public long WeekendMeetingScheduleId { get; set; }
        public string Speaker { get; set; }
        public string Topic { get; set; }
        public long ChairmanId { get; set; }
        public long WatchTowerConductorId { get; set; }
        public long ParagraphReaderId { get; set; }
        public long BibleTextReaderId { get; set; }
        public long FieldServiceGroupHost { get; set; }
        public string SpeakerCongregation { get; set; }
        public string Date { get; set; }
        public long CongregationId { get; set; }
    }
}
