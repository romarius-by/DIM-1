using DIMS.BL.DTO;
using System.Collections.Generic;

namespace DIMS.BL.Interfaces
{
    public interface IvUserTaskService : IvService<vUserTaskDTO>
    {
        IEnumerable<vUserTaskDTO> GetByUserId(int? id);
    }
}
