using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class DoctorSchedule
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public  TimeOnly StartAt { get; set; }
        public TimeOnly EndAt => StartAt.AddMinutes(30);


        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
        public string? PatientId { get; set; }
        public Patient? Patient { get; set; }
    }
}
