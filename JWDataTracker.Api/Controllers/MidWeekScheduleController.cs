using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JWDataTracker.Application.MidWeekMeetingSchedule;
using JWDataTracker.Application;
using JWDataTracker.Helper;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWDataTracker.Api.Controllers
{
    [ApiController]
    public class MidWeekScheduleController : BaseController
    {
        private readonly IMidWeekMeetingScheduleService midWeekScheduleService;
        public MidWeekScheduleController(IAuthenticatedUser currentUser, IMidWeekMeetingScheduleService _midWeekScheduleService) : base(currentUser)
        {
            midWeekScheduleService = _midWeekScheduleService;
        }

        [HttpPost]
        public Response AddEdit(MidWeekMeetingScheduleDto model)
        {
            if(model.MidWeekScheduleId == 0)
            {
                model.CreatedBy = _currentUser.Id;
                var response = midWeekScheduleService.Add(model);
                return response;
            } else
            {
                var response = midWeekScheduleService.Edit(model);
                return response;
            }
        }
    }
}

