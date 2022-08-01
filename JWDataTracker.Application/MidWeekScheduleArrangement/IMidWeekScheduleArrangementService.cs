using System;
namespace JWDataTracker.Application.MidWeekScheduleArrangement
{
    public interface IMidWeekScheduleArrangementService
    {
        IEnumerable<MidWeekScheduleArrangementDto> ListMidWeekScheduleArrangment();
    }
}

