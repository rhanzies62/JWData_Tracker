using System;
namespace JWDataTracker.Application.MidWeekScheduleArrangement
{
    public class MidWeekScheduleArrangementDto
    {
        public MidWeekScheduleArrangementDto()
        {
        }
        public long MidWeekScheduleArrangementId { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public long RoleId { get; set; }
        public string Role { get; set; }
    }
}

