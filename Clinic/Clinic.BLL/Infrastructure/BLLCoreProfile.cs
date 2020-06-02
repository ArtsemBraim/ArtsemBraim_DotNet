using Clinic.DAL.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic.BLL.Infrastructure
{
    public static class BLLCoreProfile
    {
        public static void ConfigureBllServices(this IServiceCollection services, string connectionString)
        {
            services.ConfigureDalServices(connectionString);
            
        }
    }
}
