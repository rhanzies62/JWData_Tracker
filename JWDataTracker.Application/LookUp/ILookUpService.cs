using System;
namespace JWDataTracker.Application.LookUp
{
    public interface ILookUpService
    {
        IEnumerable<LookUpDto> ListLookUpByCode(string code);
    }
}

