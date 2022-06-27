using JWDataTracker.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWDataTracker.Application.MidWeekMeetingSchedule
{
    public interface IMidWeekMeetingScheduleService
    {
        Response Add(MidWeekMeetingScheduleDto model);
        Response Edit(MidWeekMeetingScheduleDto model);
        IEnumerable<MidWeekMeetingScheduleDto> List(long congregatonId);
        Response Delete(long midWeekScheduleId);
        MidWeekMeetingScheduleDto GetMidWeekScheduleByDate(DateTime date, int congregationId);
    }
}
