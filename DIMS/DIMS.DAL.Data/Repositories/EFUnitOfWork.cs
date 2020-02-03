using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.EF.DAL.Data.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly DIMSDBContext _dimsDbContext;
        private SampleRepository _sampleRepository;
        private UserProfileRepository _userProfileRepository;

        public EFUnitOfWork(string connectionString)
        {
            this._dimsDbContext = new DIMSDBContext(connectionString);
        }

        public IRepository<Sample> Samples => _sampleRepository ?? new SampleRepository(_dimsDbContext);

        public IRepository<UserProfile> UserProfiles => _userProfileRepository ?? new UserProfileRepository(_dimsDbContext);

        public void Save()
        {
            _dimsDbContext.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    //Release managed resources
                    _dimsDbContext.Dispose();
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
