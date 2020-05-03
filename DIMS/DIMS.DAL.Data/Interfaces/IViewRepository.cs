using System;
using System.Collections.Generic;

namespace HIMS.EF.DAL.Data.Interfaces
{
    public interface IViewRepository<T>
    {
        T GetById(int id);
        
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Func<T, bool> predicate);
    }
}
