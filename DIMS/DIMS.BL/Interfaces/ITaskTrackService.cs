using HIMS.BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Interfaces
{
    public interface ITaskTrackService : IService<TaskTrackDTO>
    {
        UserTaskDTO GetUserTask(int? id);
        IEnumerable<TaskTrackDTO> GetTracksForUser(int? userId);
    }
}
