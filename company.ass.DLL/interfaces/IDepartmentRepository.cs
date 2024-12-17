using company.ass.DAL.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace company.ass.BLL.interfaces
{
     public interface IDepartmentRepository : IgenericRepository<Departments>
    {
       /* IEnumerable<Departments> GetAll();
        Departments Get(int id);
        int add(Departments entity);
        int update(Departments entity);
        int Delete(Departments entity);*/

    }
}
