namespace Clinic_Management_System.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int PatientAge { get; set; }

        public ICollection<Appointment> appointments { get; set;}
    }
}
