using ImSoftware.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ImSoftware.DAL.Repositories.Contract;
using ImSoftware.DAL.Repositories;
using ImSoftware.BLL.Services.Contract;
using ImSoftware.BLL.Services;
using ImSoftware.Utility;

namespace ImSoftware.IOC
{
    public static class Dependence
    {
        public static void InjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ImSoftwareContext>(options =>
            { 
                options.UseSqlServer(configuration.GetConnectionString("sqlConnect"));
            });

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<IUserService, UserService>();
        }
    }
}
