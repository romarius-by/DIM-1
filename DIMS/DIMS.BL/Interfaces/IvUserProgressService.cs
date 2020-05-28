using DIMS.BL.DTO;
using System.Collections.Generic;

namespace DIMS.BL.Interfaces
{
    public interface IvUserProgressService : IVService<vUserProgressDTO>
    {
        IEnumerable<vUserProgressDTO> GetByUserId(int? id);

    }
}
