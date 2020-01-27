using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.EF.DAL.Data
{
    public interface IRepository<T>
    {
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);

        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Func<T, Boolean> predicate);
    }
}
