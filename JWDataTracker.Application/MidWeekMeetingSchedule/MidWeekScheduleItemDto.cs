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

        public MidWeekScheduleItemDto()
        {

        }

        public MidWeekScheduleItemDto(long category, long publisherid, string publisherName, string hallNumber, long role)
        {
            this.Category = category;
            this.PublisherId = publisherid;
            this.HallNumber = hallNumber;
            this.PublisherName = publisherName;
            this.Role = role;
        }

        public MidWeekScheduleItemDto(long category,long publisherid, string publisherName, string hallNumber, long role, long partnerPublisherId, string partnerName)
        {
            this.Category = category;
            this.PublisherId = publisherid;
            this.PartnerPublisherId = partnerPublisherId;
            this.HallNumber = hallNumber;
            this.PublisherName = publisherName;
            this.PartnerPublisherId = partnerPublisherId;
            this.PartnerName = partnerName;
            this.Role = role;
        }
        public long MidWeekScheduleItemId { get; set; }
        public long Role { get; set; }
        public long? PublisherId { get; set; }
        public string PublisherName { get; set; }
        public long? PartnerPublisherId { get; set; }
        public string PartnerName { get; set; }
        public string HallNumber { get; set; }
        public long MidWeekScheduleId { get; set; }
        public long Category { get; set; }

        public PublisherDto Publisher { get; set; }
        public PublisherDto PartnerPublisher { get; set; }

    }
}
