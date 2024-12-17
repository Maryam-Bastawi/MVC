using company.ass.DAL.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace company.ass.BLL.interfaces
{
    public interface IgenericRepository<T>
    {
        Task<IEnumerable<T>> GetAllasync();
        Task<T> Getasync(int id);
        Task<int> addasync(T entity);
        Task<int> updateasync(T entity);
        Task<int> Deleteasync(T entity);
    }
}
