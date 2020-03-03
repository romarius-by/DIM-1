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
    public class TaskStateService : ITaskStateService
    {
        private IUnitOfWork Database { get; }

        public TaskStateService (IUnitOfWork uow)
        {
            Database = uow;
        }

        public void DeleteTaskState(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The task state id value is not set", String.Empty);

            Database.TaskStates.Delete(id.Value);

            Database.Save();
            
        }

        public TaskStateDTO GetTaskState(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The task state id value is not set", String.Empty);

            var task = Database.TaskStates.Get(id.Value);

            if (task == null)
                throw new ValidationException($"The task state with id = {id.Value} was not found", String.Empty);

            return Mapper.Map<TaskState, TaskStateDTO>(task);

        }

        public IEnumerable<UserTaskDTO> GetUserTasks(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The task state id value is not set", String.Empty);

            return Mapper.Map<List<UserTask>, ICollection<UserTaskDTO>>(
                Database.TaskStates.Get(id.Value).UserTasks.ToList());

        }

        public void SaveTaskState(TaskStateDTO taskStateDTO)
        {
            var taskState = new TaskState
            {
                StateName = taskStateDTO.StateName,
                UserTasks = Mapper.Map<List<UserTask>>(taskStateDTO.UserTasks)
            };

            Database.TaskStates.Create(taskState);
            Database.Save();
        }

        public void UpdateTaskState(TaskStateDTO taskStateDTO)
        {
            var taskState = Database.TaskStates.Get(taskStateDTO.StateId);

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
    }
}
