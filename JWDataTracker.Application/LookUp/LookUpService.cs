using System;
using JWDataTracker.Infrastructure.Repository;

namespace JWDataTracker.Application.LookUp
{
    public class LookUpService : BaseService, ILookUpService
    {
        public LookUpService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public IEnumerable<LookUpDto> ListLookUpByCode(string code)
        {
            return unitOfWork.LookUpRepository.Get(i => i.Code == code).Select(l => new LookUpDto {
                 Code = l.Code,
                 LookUpId = l.LookUpId,
                 SortOrder = l.SortOrder,
                 Text = l.Text
            });
        }
    }
}

