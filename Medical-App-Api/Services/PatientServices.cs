using Medical_App_Api.Data;
using Medical_App_Api.Model;
using Microsoft.EntityFrameworkCore;

namespace Medical_App_Api.Services
{
    public class PatientServices
    {
        private readonly AppDataContext _context;
        public PatientServices(AppDataContext context)
        {
            _context = context;
        }

        public async Task AddPatient(int id, string name)
        {
            var patient = _context.Patients.FirstOrDefault(x => x.Id == id);

            if(patient is not null)
            {
                throw new Exception($"there is already a patient with this id: {id}");
            }

            patient = new Patient { Id = id, Name = name };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        public async Task RemovePatient(int id) 
        {
            var patient = _context.Patients.FirstOrDefault(x => x.Id == id);
            if(patient is null)
            {
                throw new Exception($"there is not a patient with this id: {id}");
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        } 
    }
}
