using HIMS.BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Interfaces
{
    public interface ITaskStateService
    {
        void SaveTaskState(TaskStateDTO taskStateDTO);
        TaskStateDTO GetTaskState(int? id);
        void UpdateTaskState(TaskStateDTO taskStateDTO);
        void DeleteTaskState(int? id);

        IEnumerable<UserTaskDTO> GetUserTasks(int? id);
        void Dispose();
    }
}
