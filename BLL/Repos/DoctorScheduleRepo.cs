using BLL.Interfaces;
using DAL.Contexts;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repos
{
    public class DoctorScheduleRepo : GenericRepository<DoctorSchedule> , IDoctorScheduleRepo
    {
        private readonly DoctorDbContext dbContext;

        public DoctorScheduleRepo(DoctorDbContext _dbContext) : base(_dbContext) 
        {
            dbContext = _dbContext;
        }

        public string Compare(DoctorSchedule doctorSchedule, int? excludeId = null)
        {
            var query = dbContext.DoctorSchedule.Where(d => d.DoctorId == doctorSchedule.DoctorId
                                                        && d.Day == doctorSchedule.Day);
            if (excludeId != null) 
                query = query.Where(I => I.Id != excludeId); 
            
            var confillect = query.Where(b => (b.StartAt < doctorSchedule.StartAt.AddMinutes(30) 
                                                && b.StartAt.AddMinutes(30) > doctorSchedule.StartAt)).ToList();

            if (confillect.Any())
                return "The Date Is Taken";
            return string.Empty;
        }
    }
}
