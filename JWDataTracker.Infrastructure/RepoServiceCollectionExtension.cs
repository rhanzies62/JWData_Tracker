using JWDataTracker.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace JWDataTracker.Infrastructure
{
    public static class RepoServiceCollectionExtension
    {
        public static IServiceCollection AddInternalRepoServices(this IServiceCollection services)
        {
            services.AddDbContext<DataTrackerContext>(options => {
                options.UseSqlite("DataSource=DataTracker.db");
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
