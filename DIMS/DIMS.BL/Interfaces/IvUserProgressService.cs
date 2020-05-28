using DIMS.BL.DTO;
using System.Collections.Generic;

namespace DIMS.BL.Interfaces
{
    public interface IVUserProgressService : IVService<vUserProgressDTO>
    {
        IEnumerable<vUserProgressDTO> GetByUserId(int? id);

    }
}
