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
    public class TaskStateService : ITaskStateService
    {
        private IUnitOfWork Database { get; }

        public TaskStateService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void DeleteById(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The task state id value is not set", String.Empty);
            }

            Database.TaskStates.DeleteById(id.Value);

            Database.Save();

        }

        public TaskStateDTO GetById(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The task state id value is not set", String.Empty);
            }

            var task = Database.TaskStates.GetById(id.Value);

            if (task == null)
            {
                throw new ValidationException($"The task state with id = {id.Value} was not found", String.Empty);
            }

            return Mapper.Map<TaskState, TaskStateDTO>(task);

        }

        public IEnumerable<UserTaskDTO> GetUserTasks(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The task state id value is not set", String.Empty);
            }

            return Mapper.Map<List<UserTask>, ICollection<UserTaskDTO>>(
                Database.TaskStates.GetById(id.Value).UserTasks.ToList());

        }

        public void Save(TaskStateDTO taskStateDTO)
        {
            var taskState = new TaskState
            {
                StateName = taskStateDTO.StateName,
                UserTasks = Mapper.Map<List<UserTask>>(taskStateDTO.UserTasks)
            };

            Database.TaskStates.Create(taskState);
            Database.Save();
        }

        public void Update(TaskStateDTO taskStateDTO)
        {
            var taskState = Database.TaskStates.GetById(taskStateDTO.StateId);

            if (taskState != null)
            {
                Mapper.Map(taskStateDTO, taskState);

                Database.Save();
            }
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public IEnumerable<TaskStateDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<TaskState>, IEnumerable<TaskStateDTO>>(
                Database.TaskStates.GetAll());
        }

        public async Task<bool> DeleteByIdAsync(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The id value is not set!", String.Empty);
            }

            var taskState = await Database.TaskStates.DeleteByIdAsync(id.Value);

            if (taskState != null)
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
