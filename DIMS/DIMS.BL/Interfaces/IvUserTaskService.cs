using HIMS.BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Interfaces
{
    public interface IvUserTaskService : IvService<vUserTaskDTO>
    {
        void Save(vUserTaskDTO vUserTaskDTO);
        void Update(vUserTaskDTO vUserTaskDTO);
        IEnumerable<vUserTaskDTO> GetByUserId(int? id);
    }
}
