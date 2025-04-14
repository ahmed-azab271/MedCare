using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUnitOfWork
    {
        IDoctorRepo DoctorRepo { get; }
        IDoctorScheduleRepo DoctorScheduleRepo { get; }
        IPatientRepo PatientRepo { get; }
        Task<int> Compelete();

    }
}
