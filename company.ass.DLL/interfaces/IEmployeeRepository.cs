using company.ass.DAL.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace company.ass.BLL.interfaces
{
    public interface IEmployeeRepository : IgenericRepository<Employee>
    {
        /*  IEnumerable<Employee> GetAll();
          Employee Get(int id);
          int add(Employee entity);
          int update(Employee entity);
          int Delete(Employee entity);*/
         Task<IEnumerable<Employee>> getbynameasync(string name);

    }
}
