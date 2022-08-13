using System;
using JWDataTracker.Infrastructure.Repository;

namespace JWDataTracker.Application.MidWeekScheduleArrangement
{
    public class MidWeekScheduleArrangementService : BaseService, IMidWeekScheduleArrangementService
    {
        public MidWeekScheduleArrangementService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<MidWeekScheduleArrangementDto> ListMidWeekScheduleArrangment()
        {
            return (from mwsa in unitOfWork.MidWeekScheduleArrangementRepository.Get()
                    join c in unitOfWork.LookUpRepository.Get() on mwsa.CategoryId equals c.LookUpId
                    join r in unitOfWork.LookUpRepository.Get() on mwsa.RoleId equals r.LookUpId
                    select new MidWeekScheduleArrangementDto
                    {
                        CategoryId = mwsa.CategoryId,
                        RoleId = mwsa.RoleId,
                        CategoryName = c.Text,
                        Role = r.Text,
                        MidWeekScheduleArrangementId = mwsa.MidWeekScheduleArrangementId
                    });
        }
    }
}

