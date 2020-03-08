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
    public class UserTaskService : IUserTaskService
    {
        private IUnitOfWork Database;

        public UserTaskService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void DeleteItem(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The user task id value is not set", String.Empty);

            Database.UserTasks.Delete(id.Value);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public TaskDTO GetTask(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The user task id value is not set", String.Empty);

            return Mapper.Map<EF.DAL.Data.Task, TaskDTO>(
                Database.UserTasks.Get(id.Value).Task);
        }

        public TaskStateDTO GetTaskState(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The user task id value is not set", String.Empty);

            return Mapper.Map<TaskState, TaskStateDTO>(
                Database.UserTasks.Get(id.Value).TaskState);
        }

        public IEnumerable<TaskTrackDTO> GetTaskTracks(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The user task id value is not set", String.Empty);

            return Mapper.Map<List<TaskTrack>, ICollection<TaskTrackDTO>>(
                Database.UserTasks.Get(id.Value).TaskTracks.ToList());
        }

        public UserProfileDTO GetUserProfile(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The user task id value is not set", String.Empty);

            return Mapper.Map<UserProfile, UserProfileDTO>(
                Database.UserTasks.Get(id.Value).UserProfile);
        }

        public UserTaskDTO GetItem(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The user task id value is not set", String.Empty);

            var userTask = Database.UserTasks.Get(id.Value);

            if (userTask == null)
                throw new ValidationException($"The user task with id = {id.Value} was not found", String.Empty);

            return Mapper.Map<UserTask, UserTaskDTO>(userTask);
        }

        public IEnumerable<UserTaskDTO> GetItems()
        {
            return Mapper.Map<List<UserTask>, ICollection<UserTaskDTO>>(
                Database.UserTasks.GetAll().ToList());
        }

        public void SaveItem(UserTaskDTO userTaskDTO)
        {
            var userTask = new UserTask
            {
                Task = Mapper.Map<TaskDTO, EF.DAL.Data.Task>(userTaskDTO.Task),
                TaskId = userTaskDTO.TaskId,
                StateId = userTaskDTO.StateId,
                TaskState = Mapper.Map<TaskStateDTO, TaskState>(userTaskDTO.TaskState),
                UserId = userTaskDTO.UserId,
                UserProfile = Mapper.Map<UserProfileDTO, UserProfile>(userTaskDTO.UserProfile),
                TaskTracks = Mapper.Map<ICollection<TaskTrackDTO>, List<TaskTrack>>(userTaskDTO.TaskTracks)
            };

            Database.UserTasks.Create(userTask);
            Database.Save();
        }

        public void UpdateItem(UserTaskDTO userTaskDTO)
        {
            var userTask = Database.UserTasks.Get(userTaskDTO.UserTaskId);

            if (userTask != null)
            {
                Mapper.Map(userTaskDTO, userTask);
                Database.Save();
            }
        }
    }
}
