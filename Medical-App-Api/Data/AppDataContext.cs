using Medical_App_Api.Model;
using Microsoft.EntityFrameworkCore;

namespace Medical_App_Api.Data
{
    public class AppDataContext : DbContext
    {
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<LoginAccount> LoginAccounts { get; set; }
        public DbSet<Patient> Patients { get; set; }


        public AppDataContext(DbContextOptions<AppDataContext> options)
        : base(options) { }

    }
}
