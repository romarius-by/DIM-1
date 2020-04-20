using AutoMapper;
using HIMS.BL.DTO;
using HIMS.BL.Infrastructure;
using HIMS.BL.Interfaces;
using HIMS.EF.DAL.Data;
using HIMS.EF.DAL.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Services
{
    public class UserTaskService : IUserTaskService
    {
        private IUnitOfWork database;
        private UserTaskRepository userTaskRepository;

        public UserTaskService(IUnitOfWork uow, UserTaskRepository repository)
        {
            database = uow;
            userTaskRepository = repository;
        }

        public void DeleteById(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The user task id value is not set", String.Empty);

            database.UserTasks.Delete(id.Value);
            database.Save();
        }

        public void Dispose()
        {
            database.Dispose();
        }

        public TaskDTO GetTask(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The user task id value is not set", String.Empty);

            return Mapper.Map<EF.DAL.Data.Task, TaskDTO>(
                database.UserTasks.Get(id.Value).Task);
        }

        public TaskStateDTO GetTaskState(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The user task id value is not set", String.Empty);

            return Mapper.Map<TaskState, TaskStateDTO>(
                database.UserTasks.Get(id.Value).TaskState);
        }

        public IEnumerable<TaskTrackDTO> GetTaskTracks(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The user task id value is not set", String.Empty);

            return Mapper.Map<List<TaskTrack>, ICollection<TaskTrackDTO>>(
                database.UserTasks.Get(id.Value).TaskTracks.ToList());
        }

        public UserProfileDTO GetUserProfile(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The user task id value is not set", String.Empty);

            return Mapper.Map<UserProfile, UserProfileDTO>(
                database.UserTasks.Get(id.Value).UserProfile);
        }

        public UserTaskDTO GetById(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The user task id value is not set", String.Empty);

            var userTask = database.UserTasks.Get(id.Value);

            if (userTask == null)
                throw new ValidationException($"The user task with id = {id.Value} was not found", String.Empty);

            return Mapper.Map<UserTask, UserTaskDTO>(userTask);
        }

        public IEnumerable<UserTaskDTO> GetAll()
        {
            return Mapper.Map<List<UserTask>, ICollection<UserTaskDTO>>(
                database.UserTasks.GetAll().ToList());
        }

        public void Save(UserTaskDTO userTaskDTO)
        {
            var userTask = new UserTask
            {
                Task = Mapper.Map<TaskDTO, EF.DAL.Data.Task>(userTaskDTO.Task),
                TaskId = userTaskDTO.TaskId,
                StateId = userTaskDTO.StateId,
                TaskState = Mapper.Map<TaskStateDTO, TaskState>(userTaskDTO.TaskState),
                UserId = userTaskDTO.UserId,
                UserProfile = Mapper.Map<UserProfileDTO, UserProfile>(userTaskDTO.UserProfile),
                TaskTracks = Mapper.Map<IEnumerable<TaskTrackDTO>, List<TaskTrack>>(userTaskDTO.TaskTracks)
            };

            database.UserTasks.Create(userTask);
            database.Save();
        }

        public void Update(UserTaskDTO userTaskDTO)
        {
            var userTask = database.UserTasks.Get(userTaskDTO.UserTaskId);

            if (userTask != null)
            {
                Mapper.Map(userTaskDTO, userTask);
                database.Save();
            }
        }

        public IEnumerable<UserTaskDTO> GetByUserId(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The user profile id value is not set", String.Empty);

            return Mapper.Map<IEnumerable<UserTask>, IEnumerable<UserTaskDTO>>(
                userTaskRepository.GetByUserId(id.Value));
        }

        public async Task<OperationDetails> DeleteByIdAsync(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The id value is not set!", String.Empty);

            var res = await database.UserTasks.DeleteAsync(id.Value);

            if (res != null)
            {
                return new OperationDetails(true, "User Task has been succesfully deleted! User: ", res.UserProfile.Name + "" + res.UserProfile.LastName);
            }

            else
                return new OperationDetails(false, "Something went wrong!", " ");
        }
    }
}
