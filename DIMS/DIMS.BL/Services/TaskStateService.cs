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

        public void DeleteItem(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The task state id value is not set", String.Empty);

            database.TaskStates.Delete(id.Value);

            database.Save();
            
        }

        public TaskStateDTO GetItem(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The task state id value is not set", String.Empty);

            var task = database.TaskStates.Get(id.Value);

            if (task == null)
                throw new ValidationException($"The task state with id = {id.Value} was not found", String.Empty);

            return Mapper.Map<TaskState, TaskStateDTO>(task);

        }

        public IEnumerable<UserTaskDTO> GetUserTasks(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The task state id value is not set", String.Empty);

            return Mapper.Map<List<UserTask>, ICollection<UserTaskDTO>>(
                database.TaskStates.Get(id.Value).UserTasks.ToList());

        }

        public void SaveItem(TaskStateDTO taskStateDTO)
        {
            var taskState = new TaskState
            {
                StateName = taskStateDTO.StateName,
                UserTasks = Mapper.Map<List<UserTask>>(taskStateDTO.UserTasks)
            };

            database.TaskStates.Create(taskState);
            database.Save();
        }

        public void UpdateItem(TaskStateDTO taskStateDTO)
        {
            var taskState = database.TaskStates.Get(taskStateDTO.StateId);

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

        public IEnumerable<TaskStateDTO> GetItems()
        {
            return Mapper.Map<IEnumerable<TaskState>, IEnumerable<TaskStateDTO>>(
                database.TaskStates.GetAll());
        }

        public async Task<OperationDetails> DeleteItemAsync(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The id value is not set!", String.Empty);

            var res = await database.TaskStates.DeleteAsync(id.Value);

            if (res != null)
                return new OperationDetails(true, "Task state has been successfully deleted! State: ", res.StateName);

            else
                return new OperationDetails(false, "Something went wrong!", " ");
        }
    }
}
