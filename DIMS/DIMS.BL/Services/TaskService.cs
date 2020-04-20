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
    public class TaskService : ITaskService
    {

        private IUnitOfWork database { get; }

        public TaskService (IUnitOfWork uow)
        {
            database = uow;
        }


        public void DeleteById(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The Task id value is not set", String.Empty);

            database.Directions.DeleteById(id.Value);

            database.Save();
        }

        public void Dispose()
        {
            database.Dispose();
        }

        public TaskDTO GetById(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The Task id value is not set", String.Empty);

            var task = database.Tasks.GetById(id.Value);

            if (task == null)
                throw new ValidationException($"The task with id = {id.Value} was not found", String.Empty);

            return Mapper.Map<EF.DAL.Data.Task, TaskDTO>(task);
        }

        public IEnumerable<TaskDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<EF.DAL.Data.Task>, ICollection<TaskDTO>>(database.Tasks.GetAll());

        }

        public IEnumerable<UserTaskDTO> GetUserTasks(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The task id value is not set", String.Empty);

            return Mapper.Map<IEnumerable<UserTask>, ICollection<UserTaskDTO>>(database.Tasks.
                GetById(id.Value).UserTasks);
        }

        public void Save(TaskDTO task)
        {
            var _task = new EF.DAL.Data.Task
            {
                Name = task.Name,
                Description = task.Description,
                StartDate = task.StartDate,
                DeadlineDate = task.DeadlineDate,
                UserTasks = Mapper.Map<List<UserTaskDTO>, ICollection<UserTask>>(task.UserTasks.ToList())
            };

            database.Tasks.Create(_task);
            database.Save();
            
        }

        public void Update(TaskDTO taskDTO)
        {
            var task = database.Tasks.GetById(taskDTO.TaskId);

            if (task != null)
            {
                Mapper.Map(taskDTO, task);

                database.Save();

            }
        }

        public async Task<OperationDetails> DeleteByIdAsync(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The id value is not set!", String.Empty);
            }

            var res = await database.Tasks.DeleteByIdAsync(id.Value);

            if (res != null)
            {
                return new OperationDetails(true, "The Task has been deleted successfully! Task: ", res.Name);
            }

            else
                return new OperationDetails(false, "Something went wrong!", " ");
        }
    }
}
