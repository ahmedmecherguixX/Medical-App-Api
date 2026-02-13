using System.Security.Principal;
using Medical_App_Api.Data;
using Medical_App_Api.Model;

namespace Medical_App_Api.Services
{
    public class DoctorServices
    {
        private readonly AppDataContext _context;
        public DoctorServices(AppDataContext context)
        {
            _context = context;
        }

        public async Task AddDoctor(int id, string name)
        {
            var doctor = _context.Doctors.FirstOrDefault(x => x.Id == id);

            if (doctor is not null)
            {
                throw new Exception($"another doctor already exist with this id: {id}");
            }

            doctor = new Doctor { Id = id, Name = name };
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveDoctor(int id) 
        {
            var doctor = _context.Doctors.FirstOrDefault(x => x.Id == id);

            if (doctor is null)
            {
                throw new Exception($"no doctor exist with this id: {id}");
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
        }

    }
}
