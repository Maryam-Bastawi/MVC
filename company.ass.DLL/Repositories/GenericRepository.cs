using company.ass.BLL.interfaces;
using company.ass.DAL.Data.context;
using company.ass.DAL.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace company.ass.BLL.Repositories
{
    public class GenericRepository<T> : IgenericRepository<T> where T : BaseEntity
    {
        private protected readonly AppDbcontext _context;

        public GenericRepository(AppDbcontext context)
        {
            _context = context;
        }
        //async: return task or void or task<>
        public async Task<IEnumerable<T>> GetAllasync()
        {
            if(typeof(T) == typeof(Employee))
            {
                return  (IEnumerable<T>) await _context.employee.Include(e => e.workfor).AsNoTracking().ToListAsync();
            }
            else
            {
                return await _context.Set<T>().AsNoTracking().ToListAsync();
            }

        }
        public async Task<T> Getasync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<int> addasync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> updateasync(T entity)
        {
             _context.Set<T>().Update(entity);
            return await _context.SaveChangesAsync();
        }



        public async Task<int> Deleteasync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync();





        }
    }
}
