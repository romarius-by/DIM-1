using HIMS.BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Interfaces
{
    public interface IvUserTrackService
    {
        vUserTrackDTO GetVUserTrack(int? id);
        IEnumerable<vUserTrackDTO> GetVUserTracks();

        void Dispose();
    }
}
