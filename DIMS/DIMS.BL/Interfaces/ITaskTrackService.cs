using DIMS.BL.DTO;

namespace DIMS.BL.Interfaces
{
    public interface ITaskTrackService : IService<TaskTrackDTO>
    {
        UserTaskDTO GetUserTask(int id);
        IEnumerable<TaskTrackDTO> GetTracksForUser(int userId);
    }
}
