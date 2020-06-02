using Clinic.BLL.Interfaces;
using Clinic.BLL.Services;
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
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IPatientService, PatientService>();
        }
    }
}
