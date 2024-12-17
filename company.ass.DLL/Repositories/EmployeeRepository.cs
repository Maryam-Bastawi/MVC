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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbcontext context) : base(context)
        {

        }

        public async Task<IEnumerable<Employee>> getbynameasync(string name)
        {
            return await _context.employee.Where(e => e.name.ToLower() == name.ToLower()).Include(e => e.workfor).ToListAsync();
        }
    }
}
