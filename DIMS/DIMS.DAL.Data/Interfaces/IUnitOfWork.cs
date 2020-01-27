using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.EF.DAL.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Sample> Samples { get; }

        void Save();
    }
}
