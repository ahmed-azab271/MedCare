using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IDoctorRepo : IGenericRepository<Doctor>
    {
        Task<IEnumerable<Doctor>> GetDocsWithSchAsync();
        Task<IEnumerable<Doctor>> GetDocsAllIncludedAsync();
        Task<Doctor> GetDocsWithSchAsyncById(int id);
        Task<Doctor> GetDocsAllIncludedAsyncById(int id);
    }
}
