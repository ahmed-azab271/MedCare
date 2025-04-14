using BLL.Interfaces;
using DAL.Contexts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repos
{
    public class DoctorRepo : GenericRepository<Doctor>, IDoctorRepo
    {
        private readonly DoctorDbContext dbContext;

        public DoctorRepo(DoctorDbContext _dbContext) : base(_dbContext) 
        {
            dbContext = _dbContext;
        }

        public async Task<IEnumerable<Doctor>> GetDocsWithSchAsync()
            => await dbContext.Doctors.Include(S=>S.DoctorSchedules).ToListAsync();

        public async Task<Doctor> GetDocsWithSchAsyncById(int id)
            => await dbContext.Doctors.Include(S => S.DoctorSchedules).FirstOrDefaultAsync(i => i.Id == id);

        public async Task<IEnumerable<Doctor>> GetDocsAllIncludedAsync() 
            => await dbContext.Doctors.Include(P=>P.Patients).Include(S=>S.DoctorSchedules).ToListAsync();

        public async Task<Doctor> GetDocsAllIncludedAsyncById(int id)
            => await dbContext.Doctors.Include(P => P.Patients).Include(S => S.DoctorSchedules)
            .FirstOrDefaultAsync(i => i.Id == id);
    }
}
