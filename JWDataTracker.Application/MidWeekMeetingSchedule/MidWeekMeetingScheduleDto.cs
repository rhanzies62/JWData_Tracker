using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
                return JsonConvert.DeserializeObject<DateTime>(ScheduledDate);
            }
        }

        public IEnumerable<MidWeekScheduleItemDto> MidWeekScheduleItems { get; set; }
    }
}
