using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.EF.DAL.Data.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly DIMSDBContext _himsDbContext;
        private SampleRepository _sampleRepository;

        public EFUnitOfWork(string connectionString)
        {
            this._himsDbContext = new DIMSDBContext(connectionString);
        }

        public IRepository<Sample> Samples => _sampleRepository ?? new SampleRepository(_himsDbContext);

        public void Save()
        {
            _himsDbContext.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    //Release managed resources
                    _himsDbContext.Dispose();
                }
                //release unmanaged resources
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
