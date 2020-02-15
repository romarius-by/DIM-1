using AutoMapper;
using HIMS.BL.DTO;
using HIMS.BL.Infrastructure;
using HIMS.BL.Interfaces;
using HIMS.EF.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Services
{
    public class TaskTrackService : ITaskTrackService
    {

        private IUnitOfWork Database;

        public TaskTrackService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void DeleteTaskTrack(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The task track id value is not set", String.Empty);

            Database.TaskTracks.Delete(id.Value);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public TaskTrackDTO GetTaskTrack(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The task track id value is not set", String.Empty);

            var task = Database.TaskTracks.Get(id.Value);

            if (task == null)
                throw new ValidationException($"The task track with id = {id.Value} was not found", String.Empty);

            return Mapper.Map<TaskTrack, TaskTrackDTO>(task);
        }

        public ICollection<TaskTrackDTO> GetTaskTracks()
        {
            return Mapper.Map<List<TaskTrack>, ICollection<TaskTrackDTO>>(
                Database.TaskTracks.GetAll().ToList());
        }

        public UserTaskDTO GetUserTask(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The task track id value is not set", String.Empty);

            return Mapper.Map<UserTask, UserTaskDTO>(
                Database.TaskTracks.Get(id.Value).UserTask);
        }

        public void SaveTaskTrack(TaskTrackDTO taskTrackDTO)
        {
            var taskTrack = new TaskTrack
            {
                TrackNote = taskTrackDTO.TrackNote,
                TrackDate = taskTrackDTO.TrackDate,
                UserTask = Mapper.Map<UserTaskDTO, UserTask>(taskTrackDTO.UserTask),
                UserTaskId = taskTrackDTO.UserTaskId
            };

            Database.TaskTracks.Create(taskTrack);
            Database.Save();
        }

        public void UpdateTaskTrack(TaskTrackDTO taskTrackDTO)
        {
            var taskTrack = Database.TaskTracks.Get(taskTrackDTO.TaskTrackId);

            if (taskTrack != null)
            {
                Mapper.Map(taskTrackDTO, taskTrack);
                Database.Save();
            }
        }
    }
}
