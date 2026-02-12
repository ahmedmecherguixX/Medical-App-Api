using Medical_App_Api.Model;
using Microsoft.EntityFrameworkCore;

namespace Medical_App_Api.Data
{
    public class AppDataContext : DbContext
    {
        DbSet<Appointment> Appointments { get; set; }
        DbSet<Doctor> Doctors { get; set; }
        DbSet<LoginAccount> LoginAccounts { get; set; }
        DbSet<Patient> patients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=MedicalAppApiDb.db");
        }
    }
}
