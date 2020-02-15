using HIMS.BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Interfaces
{
    public interface ITaskService
    {
        void SaveTask(TaskDTO task);
        TaskDTO GetTask(int? id);
        void UpdateTask(TaskDTO taskDTO);
        void DeleteTask(int? id);

        IEnumerable<TaskDTO> GetTasks();
        IEnumerable<UserTaskDTO> GetUserTasks(int? id);
        void Dispose();
    }
}
