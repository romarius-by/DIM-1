using HIMS.BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Interfaces
{
    public interface IvUserProgressService
    {
        vUserProgressDTO GetVUserProgress(int? id);
        IEnumerable<vUserProgressDTO> GetVUserProgresses();
        IEnumerable<vUserProgressDTO> GetVUserProgressesByUserId(int? id);

        void Dispose();
    }
}
