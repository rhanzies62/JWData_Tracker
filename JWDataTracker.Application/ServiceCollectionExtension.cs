using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JWDataTracker.Infrastructure;
using JWDataTracker.Application.Congregation;
using JWDataTracker.Application.CongregationUser;
using JWDataTracker.Application.MidWeekMeetingSchedule;

namespace JWDataTracker.Application
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInternalServices(this IServiceCollection services)
        {
            services.AddScoped<ICongregationService, CongregationService>();
            services.AddScoped<ICongregationUserService, CongregationUserService>();
            services.AddScoped<IMidWeekMeetingScheduleService, MidWeekMeetingScheduleService>();
            services.AddInternalRepoServices();
            return services;
        }
    }
}
