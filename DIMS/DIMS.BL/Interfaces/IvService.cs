using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Interfaces
{
    public interface IvService<T>
    {
        T GetById(int? id);
        IEnumerable<T> GetAll();
        
        void Dispose();
    }
}
