using JWDataTracker.Application.Publisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWDataTracker.Application.MidWeekMeetingSchedule
{
    public class MidWeekScheduleItemDto
    {
        public long MidWeekScheduleItemId { get; set; }
        public string Role { get; set; }
        public long PublisherId { get; set; }
        public long PartnerPublisherId { get; set; }
        public string HallNumber { get; set; }
        public long MidWeekScheduleId { get; set; }

        public PublisherDto Publisher { get; set; }
        public PublisherDto PartnerPublisher { get; set; }

    }
}
