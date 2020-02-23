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

        private IUnitOfWork Database { get; }

        public TaskService (IUnitOfWork uow)
        {
            Database = uow;
        }


        public void DeleteTask(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The Task id value is not set", String.Empty);

            Database.Directions.Delete(id.Value);

            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public TaskDTO GetTask(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The Task id value is not set", String.Empty);

            var task = Database.Tasks.Get(id.Value);

            if (task == null)
                throw new ValidationException($"The task with id = {id.Value} was not found", String.Empty);

            return Mapper.Map<EF.DAL.Data.Task, TaskDTO>(task);
        }

        public IEnumerable<TaskDTO> GetTasks()
        {
            return Mapper.Map<IEnumerable<EF.DAL.Data.Task>, ICollection<TaskDTO>>(Database.Tasks.GetAll());

        }

        public IEnumerable<UserTaskDTO> GetUserTasks(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The task id value is not set", String.Empty);

            return Mapper.Map<IEnumerable<UserTask>, ICollection<UserTaskDTO>>(Database.Tasks.
                Get(id.Value).UserTasks);
        }

        public void SaveTask(TaskDTO task)
        {
            var _task = new EF.DAL.Data.Task
            {
                Name = task.Name,
                Description = task.Description,
                StartDate = task.StartDate,
                DeadlineDate = task.DeadlineDate,
                UserTasks = Mapper.Map<List<UserTaskDTO>, ICollection<UserTask>>(task.UserTasks.ToList())
            };

            Database.Tasks.Create(_task);
            Database.Save();
            
        }

        public void UpdateTask(TaskDTO taskDTO)
        {
            var task = Database.Tasks.Get(taskDTO.TaskId);

            if (task != null)
            {
                Mapper.Map(taskDTO, task);

                Database.Save();

            }
        }
    }
}
