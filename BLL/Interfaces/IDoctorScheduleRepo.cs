using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IDoctorScheduleRepo : IGenericRepository<DoctorSchedule>
    {
        string Compare(DoctorSchedule doctorSchedule, int? excludeId = null);
    }
}
