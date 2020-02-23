using HIMS.BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Interfaces
{
    public interface ITaskTrackService
    {
        TaskTrackDTO GetTaskTrack(int? id);
        void SaveTaskTrack(TaskTrackDTO taskTrackDTO);
        void UpdateTaskTrack(TaskTrackDTO taskTrackDTO);
        void DeleteTaskTrack(int? id);

        UserTaskDTO GetUserTask(int? id);
        ICollection<TaskTrackDTO> GetTaskTracks();
        void Dispose();
    }
}
