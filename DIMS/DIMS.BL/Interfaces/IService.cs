using HIMS.BL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Interfaces
{
    public interface IService<T> : IvService<T>
    {
        void Save(T item);
        void Update(T item);
        void DeleteById(int? id);

        Task<OperationDetails> DeleteByIdAsync(int? id);

    }
}
