using CleanTeathPersistance.Repositories;
using CleanTeathPersistance.UnitsOfWork;
using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Contracts.Repositories.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTeathPersistance
{
    public static class RegisterPersistenceServices
    {
        public static IServiceCollection AddpersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<CleanTeethDbContext>(options =>
                options.UseSqlServer("name=CleanTeethConnectionString"));

            services.AddScoped<IDentalOfficeRepository, DentalOfficeRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWorkEFCore>();

            return services;
        }
    }
}
