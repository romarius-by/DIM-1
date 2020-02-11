using HIMS.EF.DAL.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.EF.DAL.Data.Repositories
{
    public class vUserProfileRepository : IViewRepository<vUserProfile>
    {

        private readonly DIMSDBContext _dIMSDBContext;

        public vUserProfileRepository(DIMSDBContext dIMSDBContext)
        {
            _dIMSDBContext = dIMSDBContext;
        }

        public IEnumerable<vUserProfile> Find(Func<vUserProfile, bool> predicate)
        {
            return _dIMSDBContext.vUserProfiles.Where(predicate).ToList();
        }

        public vUserProfile Get(int id)
        {
            return _dIMSDBContext.vUserProfiles.Find(id);
        }

        public IEnumerable<vUserProfile> GetAll()
        {
            return _dIMSDBContext.vUserProfiles;
        }
    }
}
