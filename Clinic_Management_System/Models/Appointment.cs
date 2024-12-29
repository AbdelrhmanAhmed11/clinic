namespace Clinic_Management_System.Models
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentNotes { get; set; }

        public int DoctorID {  get; set; }
        public int PatientID {  get; set; }

        public Doctor doctor { get; set; }
        public Patient patient { get; set; }
    }
}
