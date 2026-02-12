namespace Medical_App_Api.Model
{
    public class Appointment
    {
        //though about using Guid type instead but thats just overkill in my opinion at least until i am told to do Guid Id
        int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime DateAndTime { get; set; }
        public AppointmentStatus Status { get; set; }
        //in minuts
        public int Duration { get; set; }
        public string? Description { get; set; }

        public Patient patient { get; set; } = null!;
        public Doctor doctor { get; set; } = null!;
    }
}
