using JWDataTracker.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWDataTracker.Application.Congregation
{
    public interface ICongregationService
    {
        Response Add(CongregationDto model);
        Response Edit(CongregationDto model);
        IEnumerable<CongregationDto> List();
        Response Delete(int congregationId);
        CongregationDto GetById(int congregationId);
    }
}
