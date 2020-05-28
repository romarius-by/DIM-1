using DIMS.BL.DTO;
using System.Collections.Generic;

namespace DIMS.BL.Interfaces
{
    public interface IvUserTaskService : IVService<vUserTaskDTO>
    {
        void Save(vUserTaskDTO vUserTaskDTO);
        void Update(vUserTaskDTO vUserTaskDTO);
        IEnumerable<vUserTaskDTO> GetByUserId(int id);
    }
}
