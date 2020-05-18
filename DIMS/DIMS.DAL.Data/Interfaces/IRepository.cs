using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.EF.DAL.Data
{
    public interface IRepository<T>
    {
        T GetById(int id);
        void Create(T item);
        void Update(T item);
        void DeleteById(int id);

        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Func<T, Boolean> predicate);

        Task<T> DeleteByIdAsync(int id);
    }
}
