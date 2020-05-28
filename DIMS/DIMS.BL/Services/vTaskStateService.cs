using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Infrastructure;
using DIMS.BL.Interfaces;
using DIMS.EF.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIMS.BL.Services
{
    public class vTaskStateService : IvTaskStateService
    {
        private IUnitOfWork database { get; }

        private readonly IMapper _mapper;

        public vTaskStateService (IUnitOfWork uow, IMapper mapper)
        {
            database = uow;
            _mapper = mapper;
        }

        public TaskStateDTO GetById(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The task state id value is not set", String.Empty);

            var task = database.TaskStates.GetById(id.Value);

            if (task == null)
                throw new ValidationException($"The task state with id = {id.Value} was not found", String.Empty);

            return _mapper.Map<TaskState, TaskStateDTO>(task);

        }

        public void Dispose()
        {
            database.Dispose();
        }

        public IEnumerable<TaskStateDTO> GetAll()
        {
            var taskStates = database.TaskStates.GetAll();

            return _mapper.Map<IEnumerable<TaskState>, IEnumerable<TaskStateDTO>>(taskStates);
        }
    }
}
