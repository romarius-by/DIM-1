using HIMS.EF.DAL.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.EF.DAL.Data.Repositories
{
    public class vUserProgressRepository : IViewRepository<vUserProgress>
    {

        private readonly DIMSDBContext _dIMSDBContext;

        public vUserProgressRepository(DIMSDBContext dIMSDBContext)
        {
            _dIMSDBContext = dIMSDBContext;
        }

        public IEnumerable<vUserProgress> Find(Func<vUserProgress, bool> predicate)
        {
            return _dIMSDBContext.vUserProgresses.Where(predicate).ToList();
        }

        public vUserProgress GetById(int id)
        {
            return _dIMSDBContext.vUserProgresses.Find(id);
        }

        public IEnumerable<vUserProgress> GetAll()
        {
            return _dIMSDBContext.vUserProgresses;
        }
    }
}
