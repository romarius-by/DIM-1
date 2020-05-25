using System.Collections.Generic;

namespace DIMS.BL.Interfaces
{
    public interface IvService<T>
    {
        T GetById(int? id);
        IEnumerable<T> GetAll();

        void Dispose();
    }
}
