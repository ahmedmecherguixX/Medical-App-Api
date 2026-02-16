using System.Data;
using Medical_App_Api.Data;
using Medical_App_Api.Model;
using Microsoft.EntityFrameworkCore;

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

            if (duration <= 0)
            {
                throw new Exception("non valid duration");
            }

            if (!await IsTimeSlotAvailable(0, doctorid, duration, datetime))
            {
                throw new Exception("doctor is not available in this time slot");
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

        public async Task UpdateAppointment(int appointmentid, int patientid, int doctorid, int duration, DateTime datetime,AppointmentStatus appointmentstatus)
        {
            var patient = _context.Patients.Find(patientid);
            if (patient == null)
            {
                throw new Exception("patient not found");
            }

            if (duration <= 0)
            {
                throw new Exception("non valid duration");
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

            var appointment = _context.Appointments.FirstOrDefault(a => a.Id == appointmentid);
            if (appointment == null)
            {
                throw new Exception("appointment not found");
            }

            if (!await IsTimeSlotAvailable(appointmentid, doctorid, duration, datetime))
            {
                throw new Exception("Doctor is not available in this time slot");
            }

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

        public async Task<bool> IsTimeSlotAvailable(int appointmentid, int doctorid, int duration, DateTime dateTime)
        {
            var newStart = dateTime;
            var newEnd = dateTime.AddMinutes(duration);

            return !await _context.Appointments.AnyAsync(a =>
                a.DoctorId == doctorid &&
                a.Id != appointmentid &&
                newStart < a.DateAndTime.AddMinutes(a.Duration) &&
                newEnd > a.DateAndTime
            );
        }




    }
}
