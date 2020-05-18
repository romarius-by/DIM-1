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
    public class vUserTaskService : IvUserTaskService
    {

        private IUnitOfWork Database;
        private vUserTaskRepository Repository;

        public vUserTaskService(IUnitOfWork uow, vUserTaskRepository repository)
        {
            Database = uow;
            Repository = repository;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public IEnumerable<vUserTaskDTO> GetByUserId(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The view user task id value is not set", String.Empty);

            return Mapper.Map<IEnumerable<vUserTask>, IEnumerable<vUserTaskDTO>>(
                Repository.GetByUserId(id.Value));
        }

        public vUserTaskDTO GetById(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The view user task id value is not set", String.Empty);

            var _vUserTask = Database.vUserTasks.GetById(id.Value);

            if (_vUserTask == null)
                throw new ValidationException($"The view user task with id = {id.Value} was not found", String.Empty);

            return Mapper.Map<vUserTask, vUserTaskDTO>(_vUserTask);
        }

        public IEnumerable<vUserTaskDTO> GetAll()
        {
            return Mapper.Map<List<vUserTask>, ICollection<vUserTaskDTO>>(
                Database.vUserTasks.GetAll().ToList());
        }

        public void Save(vUserTaskDTO vUserTaskDTO)
        {
            var userTask = new UserTask
            {
                TaskId = vUserTaskDTO.TaskId,
                StateId = vUserTaskDTO.StateId,
                UserId = vUserTaskDTO.UserId
            };

            Database.UserTasks.Create(userTask);
            Database.Save();
        }

        public void Update(vUserTaskDTO vUserTaskDTO)
        {
            var userTask = Database.UserTasks.GetById(vUserTaskDTO.UserTaskId);

            if (userTask != null)
            {
                Mapper.Map(vUserTaskDTO, userTask);
                Database.Save();
            }
        }
    }
}
