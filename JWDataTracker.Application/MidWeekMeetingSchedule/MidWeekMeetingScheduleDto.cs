using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWDataTracker.Application.MidWeekMeetingSchedule
{
    public class MidWeekMeetingScheduleDto
    {
        public long MidWeekScheduleId { get; set; }
        public string ScheduledDate { get; set; }
        public long? CreatedBy { get; set; }
        public long Attendance { get; set; }
        public long CongregationId { get; set; }
        public DateTime ScheduleDT
        {
            get
            {
                DateTime.TryParse(ScheduledDate, out DateTime scheduleDt);
                return scheduleDt;
            }
        }

        public IEnumerable<MidWeekScheduleItemDto> MidWeekScheduleItems { get; set; }
    }
}
