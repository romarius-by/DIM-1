using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Infrastructure;
using DIMS.BL.Interfaces;
using DIMS.EF.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS.BL.Services
{
    public class TaskTrackService : ITaskTrackService
    {

        private readonly IUnitOfWork Database;

        public TaskTrackService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void DeleteById(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The task track id value is not set", String.Empty);
            }

            Database.TaskTracks.DeleteById(id.Value);
            Database.Save();
        }

        public async Task<bool> DeleteByIdAsync(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The id value is not set!", String.Empty);
            }

            var taskTrack = await Database.TaskTracks.DeleteByIdAsync(id.Value);

            if (taskTrack != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public TaskTrackDTO GetById(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The task track id value is not set", String.Empty);
            }

            var task = Database.TaskTracks.GetById(id.Value);

            if (task == null)
            {
                throw new ValidationException($"The task track with id = {id.Value} was not found", String.Empty);
            }

            return Mapper.Map<TaskTrack, TaskTrackDTO>(task);
        }

        public IEnumerable<TaskTrackDTO> GetAll()
        {
            return Mapper.Map<List<TaskTrack>, ICollection<TaskTrackDTO>>(
                Database.TaskTracks.GetAll().ToList());
        }

        public UserTaskDTO GetUserTask(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The task track id value is not set", String.Empty);
            }

            return Mapper.Map<UserTask, UserTaskDTO>(
                Database.TaskTracks.GetById(id.Value).UserTask);
        }

        public void Save(TaskTrackDTO taskTrackDTO)
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

        public void Update(TaskTrackDTO taskTrackDTO)
        {
            var taskTrack = Database.TaskTracks.GetById(taskTrackDTO.TaskTrackId);

            if (taskTrack != null)
            {
                Mapper.Map(taskTrackDTO, taskTrack);
                Database.Save();
            }
        }
    }
}
