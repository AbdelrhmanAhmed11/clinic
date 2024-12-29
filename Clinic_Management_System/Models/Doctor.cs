using System.Collections;

namespace Clinic_Management_System.Models
{
    public class Doctor
    {
        public int DoctorID { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSpecialty {  get; set; }

        public ICollection<Appointment> appointments { get; set; }
    }
}
