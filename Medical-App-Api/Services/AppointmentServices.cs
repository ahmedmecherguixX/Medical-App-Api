using System.Data;
using Medical_App_Api.Data;
using Medical_App_Api.Model;
using Microsoft.VisualBasic;

namespace Medical_App_Api.Services
{
    public class AppointmentServices
    {
        public AppDataContext _context;
        public AppointmentServices(AppDataContext context)
        {
            _context = context;
        }

        public async Task AddAppointment(int patientid,int doctorid,int duration, DateTime datetime)
        {
            var patient = _context.Patients.Find(patientid);
            if (patient == null) 
            {
                throw new Exception("patient not found");
            }

            var doctor = _context.Doctors.Find(doctorid);
            if(doctor == null)
            {
                throw new Exception("doctor not found");
            }

            if (datetime < DateTime.Now)
            {
                throw new Exception("the date has already passed");
            }

            var appointment = new Appointment
            {
                PatientId = patientid,
                DoctorId = doctorid,
                Duration = duration,
                DateAndTime = datetime,
                Status = AppointmentStatus.Scheduled,
            };


            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAppointment(int patientid, int doctorid, int duration, DateTime datetime,AppointmentStatus appointmentstatus)
        {
            var patient = _context.Patients.Find(patientid);
            if (patient == null)
            {
                throw new Exception("patient not found");
            }

            var doctor = _context.Doctors.Find(doctorid);
            if (doctor == null)
            {
                throw new Exception("doctor not found");
            }

            if (datetime < DateTime.Now)
            {
                throw new Exception("the date has already passed");
            }

            var appointment = new Appointment
            {
                PatientId = patientid,
                DoctorId = doctorid,
                Duration = duration,
                DateAndTime = datetime,
                Status = appointmentstatus,
            };


            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task DescribeAppointment(int appointmentid,string description)
        {
            var appointment = _context.Appointments.FirstOrDefault(x => x.Id == appointmentid);

            if(appointment == null)
            {
                throw new Exception("appointment not found");
            }

            appointment.Description = description;

            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAppointment(int appointmentid)
        {
            var appointment = _context.Appointments.FirstOrDefault(x => x.Id == appointmentid);

            if (appointment == null)
            {
                throw new Exception("appointment not found");
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
        }

    }
}
