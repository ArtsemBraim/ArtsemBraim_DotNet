using Clinic.DAL.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Clinic.DAL.EF
{
    public class ClinicContext : IdentityDbContext<IdentityUser>
    {
        public ClinicContext(DbContextOptions<ClinicContext> options)
            : base (options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Reception> Receptions { get; set; }
    }
}
