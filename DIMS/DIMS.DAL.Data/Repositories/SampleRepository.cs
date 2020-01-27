using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.EF.DAL.Data.Repositories
{
    public class SampleRepository : IRepository<Sample>
    {
        private readonly HIMSDbContext _himsDbContext;

        public SampleRepository(HIMSDbContext himsDbContext)
        {
            _himsDbContext = himsDbContext;
        }

        public void Create(Sample item)
        {
            _himsDbContext.Samples.Add(item);
        }

        public void Delete(int id)
        {
            var entity = _himsDbContext.Samples.Find(id);
            if (entity != null)
            {
                _himsDbContext.Samples.Remove(entity);
            }
        }

        public IEnumerable<Sample> Find(Func<Sample, bool> predicate)
        {
            return _himsDbContext.Samples.Where(predicate).ToList();
        }

        public Sample Get(int id)
        {
            return _himsDbContext.Samples.Find(id);
        }

        public IEnumerable<Sample> GetAll()
        {
            return _himsDbContext.Samples;
        }

        public void Update(Sample item)
        {
            _himsDbContext.Entry(item).State = EntityState.Modified;
        }
    }
}
