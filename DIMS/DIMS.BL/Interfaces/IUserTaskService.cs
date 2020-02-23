using HIMS.BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Interfaces
{
    public interface IUserTaskService
    {
        void SaveUserTask(UserTaskDTO userTaskDTO);
        UserTaskDTO GetUserTask(int? id);
        void UpdateUserTask(UserTaskDTO userTaskDTO);
        void DeleteUserTask(int? id);

        ICollection<UserTaskDTO> GetUserTask();
        TaskDTO GetTask(int? id);
        TaskStateDTO GetTaskState(int? id);
        ICollection<TaskTrackDTO> GetTaskTracks(int? id);
        UserProfileDTO GetUserProfile(int? id);
        void Dispose();
    }
}
