using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Interfaces
{
    public interface IService<T> : IvService<T>
    {
        void SaveItem(T item);
        void UpdateItem(T item);
        void DeleteItem(int? id);

    }
}
