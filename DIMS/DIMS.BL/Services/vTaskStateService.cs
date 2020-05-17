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
    public class vTaskStateService : IvTaskStateService
    {
        private IUnitOfWork database { get; }

        public vTaskStateService (IUnitOfWork uow)
        {
            database = uow;
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

        public void Dispose()
        {
            database.Dispose();
        }

        public IEnumerable<TaskStateDTO> GetAll()
        {
            var taskStates = database.TaskStates.GetAll();

            return Mapper.Map<IEnumerable<TaskState>, IEnumerable<TaskStateDTO>>(taskStates);
        }
    }
}
