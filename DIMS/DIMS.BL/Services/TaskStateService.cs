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
        private IUnitOfWork database { get; }

        public TaskStateService (IUnitOfWork uow)
        {
            database = uow;
        }

        public void DeleteById(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The task state id value is not set", String.Empty);

            database.TaskStates.DeleteById(id.Value);

            database.Save();
            
        }

        public TaskStateDTO GetById(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The task state id value is not set", String.Empty);

            var task = database.TaskStates.GetById(id.Value);

            if (task == null)
                throw new ValidationException($"The task state with id = {id.Value} was not found", String.Empty);

            return Mapper.Map<TaskState, TaskStateDTO>(task);

        }

        public IEnumerable<UserTaskDTO> GetUserTasks(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The task state id value is not set", String.Empty);

            return Mapper.Map<List<UserTask>, ICollection<UserTaskDTO>>(
                database.TaskStates.GetById(id.Value).UserTasks.ToList());

        }

        public void Save(TaskStateDTO taskStateDTO)
        {
            var taskState = new TaskState
            {
                StateName = taskStateDTO.StateName,
                UserTasks = Mapper.Map<List<UserTask>>(taskStateDTO.UserTasks)
            };

            database.TaskStates.Create(taskState);
            database.Save();
        }

        public void Update(TaskStateDTO taskStateDTO)
        {
            var taskState = database.TaskStates.GetById(taskStateDTO.StateId);

            if (taskState != null)
            {
                Mapper.Map(taskStateDTO, taskState);

                database.Save();
            }
        }

        public void Dispose()
        {
            database.Dispose();
        }

        public IEnumerable<TaskStateDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<TaskState>, IEnumerable<TaskStateDTO>>(
                database.TaskStates.GetAll());
        }

        public async Task<OperationDetails> DeleteByIdAsync(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The id value is not set!", String.Empty);

            var taskState = await database.TaskStates.DeleteByIdAsync(id.Value);

            if (taskState != null)
                return new OperationDetails(true, "Task state has been successfully deleted! State: ", taskState.StateName);

            else
                return new OperationDetails(false, "Something went wrong!", " ");
        }
    }
}
