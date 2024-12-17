using company.ass.BLL.interfaces;
using company.ass.DAL.Data.context;
using company.ass.DAL.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace company.ass.BLL.Repositories
{
    public class DepartmentRepositories : GenericRepository<Departments>, IDepartmentRepository
    {

        public DepartmentRepositories(AppDbcontext context) : base(context)
        {

        }


    }
}