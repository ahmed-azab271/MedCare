using BLL.Interfaces;
using DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repos
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DoctorDbContext dbContext;

        public GenericRepository(DoctorDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync() => await dbContext.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(int id) => await dbContext.Set<T>().FindAsync(id);

        public async Task Add(T entity) => await dbContext.Set<T>().AddAsync(entity);

        public void Update(T entity) => dbContext.Set<T>().Update(entity);

        public void Delete(T entity) =>  dbContext.Set<T>().Remove(entity);


    }
}
