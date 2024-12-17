using company.ass.BLL.interfaces;
using company.ass.BLL.Repositories;
using company.ass.DAL.Data.context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace company.ass.BLL
{
    public class Unitofwork : IUnitofwork
    {
        private readonly AppDbcontext _context;
        private IEmployeeRepository _EmployeeRepository;
        private IDepartmentRepository _DepartmentRepository;
        public Unitofwork(AppDbcontext context)
        {
            _context = context;

            _EmployeeRepository = new EmployeeRepository(context);
            _DepartmentRepository = new DepartmentRepositories(context);
        }

        public IEmployeeRepository EmployeeRepository => _EmployeeRepository;

        public IDepartmentRepository DepartmentRepository => _DepartmentRepository;
    }
}
