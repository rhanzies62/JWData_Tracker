using JWDataTracker.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWDataTracker.Application.CongregationUser
{
    public interface ICongregationUserService
    {
        Response Add(CongregationUserDto model);
        Response Edit(CongregationUserDto model);
        IEnumerable<CongregationUserDto> List();
        Response Delete(int congregationId);
        CongregationUserDto GetById(int congregationId);
    }
}
