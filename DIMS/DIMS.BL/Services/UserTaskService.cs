using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Infrastructure;
using DIMS.BL.Interfaces;
using DIMS.EF.DAL.Data;
using DIMS.EF.DAL.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS.BL.Services
{
    using EntityTask = EF.DAL.Data.Task;

    public class UserTaskService : IUserTaskService
    {
        private readonly IUnitOfWork Database;
        private readonly UserTaskRepository Repository;
        private readonly IMapper _mapper;

        public UserTaskService(IUnitOfWork uow, UserTaskRepository repository, IMapper mapper)
        {
            Database = uow;
            Repository = repository;
            _mapper = mapper;
        }

        public void DeleteById(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The user task id value is not set", string.Empty);
            }

            Database.UserTasks.DeleteById(id.Value);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public TaskDTO GetTask(int? id)
        {
            if (!id.HasValue)
            {
                Logger.Logger.Warning("The user task id value is not set");
            }

            return _mapper.Map<EntityTask, TaskDTO>(Database.UserTasks.GetById(id.Value).Task);
        }

        public TaskStateDTO GetTaskState(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The user task id value is not set", string.Empty);
            }

            return _mapper.Map<TaskState, TaskStateDTO>(
                Database.UserTasks.GetById(id.Value).TaskState);
        }

        public IEnumerable<TaskTrackDTO> GetTaskTracks(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The user task id value is not set", string.Empty);
            }

            return _mapper.Map<List<TaskTrack>, ICollection<TaskTrackDTO>>(
                Database.UserTasks.GetById(id.Value).TaskTracks.ToList());
        }

        public UserProfileDTO GetUserProfile(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The user task id value is not set", string.Empty);
            }

            return _mapper.Map<UserProfile, UserProfileDTO>(
                Database.UserTasks.GetById(id.Value).UserProfile);
        }

        public UserTaskDTO GetById(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The user task id value is not set", string.Empty);
            }

            var userTask = Database.UserTasks.GetById(id.Value);

            if (userTask == null)
            {
                throw new ValidationException($"The user task with id = {id.Value} was not found", string.Empty);
            }

            return _mapper.Map<UserTask, UserTaskDTO>(userTask);
        }

        public IEnumerable<UserTaskDTO> GetAll()
        {
            return _mapper.Map<List<UserTask>, ICollection<UserTaskDTO>>(
                Database.UserTasks.GetAll().ToList());
        }

        public void Save(UserTaskDTO userTaskDTO)
        {
            var userTask = new UserTask
            {
                Task = _mapper.Map<TaskDTO, EntityTask>(userTaskDTO.Task),
                TaskId = userTaskDTO.TaskId,
                StateId = userTaskDTO.StateId,
                TaskState = _mapper.Map<TaskStateDTO, TaskState>(userTaskDTO.TaskState),
                UserId = userTaskDTO.UserId,
                UserProfile = _mapper.Map<UserProfileDTO, UserProfile>(userTaskDTO.UserProfile),
                TaskTracks = _mapper.Map<IEnumerable<TaskTrackDTO>, List<TaskTrack>>(userTaskDTO.TaskTracks)
            };

            Database.UserTasks.Create(userTask);
            Database.Save();
        }

        public void Update(UserTaskDTO userTaskDTO)
        {
            var userTask = Database.UserTasks.GetById(userTaskDTO.UserTaskId);

            if (userTask != null)
            {
                _mapper.Map(userTaskDTO, userTask);
                Database.Save();
            }
        }

        public IEnumerable<UserTaskDTO> GetByUserId(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The user profile id value is not set", string.Empty);
            }

            return _mapper.Map<IEnumerable<UserTask>, IEnumerable<UserTaskDTO>>(
                Repository.GetByUserId(id.Value));
        }

        public async Task<bool> DeleteByIdAsync(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The id value is not set!", string.Empty);
            }

            var userTask = await Database.UserTasks.DeleteByIdAsync(id.Value);

            if (userTask != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
