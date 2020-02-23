using HIMS.EF.DAL.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.EF.DAL.Data.Repositories
{
    public class vUserTrackRepository : IViewRepository<vUserTrack>
    {

        private readonly DIMSDBContext _dIMSDBContext;

        public vUserTrackRepository(DIMSDBContext dIMSDBContext)
        {
            _dIMSDBContext = dIMSDBContext;
        }

        public IEnumerable<vUserTrack> Find(Func<vUserTrack, bool> predicate)
        {
            return _dIMSDBContext.vUserTracks.Where(predicate).ToList();
        }

        public vUserTrack Get(int id)
        {
            return _dIMSDBContext.vUserTracks.Find(id);
        }

        public IEnumerable<vUserTrack> GetAll()
        {
            return _dIMSDBContext.vUserTracks;
        }
    }
}
