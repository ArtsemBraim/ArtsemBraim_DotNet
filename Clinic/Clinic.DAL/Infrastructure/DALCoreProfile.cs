using Clinic.DAL.EF;
using Clinic.DAL.Interfaces;
using Clinic.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.DAL.Infrastructure
{
    public static class DALCoreProfile
    {
        public static void ConfigureDalServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ClinicContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        }
    }
}
