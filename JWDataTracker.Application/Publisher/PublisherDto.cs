using JWDataTracker.Application.MidWeekMeetingSchedule;
using JWDataTracker.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWDataTracker.Application.Publisher
{
    public class PublisherDto
    {
        public long PublisherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? CongregationId { get; set; }
        public bool IsElder { get; set; }
        public bool IsMs { get; set; }
        public bool IsRp { get; set; }
        public bool IsUnBaptized { get; set; }
        public long GroupNumber { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } }
        public RecentPart MostRecentPart
        {
            get
            {
                if (RecentParts == null) return null;
                else
                {
                    return RecentParts.FirstOrDefault();
                }
            }
        }
        public IEnumerable<RecentPart> RecentParts { get; set; }
    }

    public class RecentPart
    {
        public DateTime Date { get; set; }
        public MidWeekScheduleItemDto Part { get; set; }
    }
}
