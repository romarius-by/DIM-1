using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Infrastructure;
using DIMS.BL.Interfaces;
using DIMS.EF.DAL.Data;
using System;
using System.Collections.Generic;

namespace DIMS.BL.Services
{
    public class vTaskStateService : IvTaskStateService
    {
        private IUnitOfWork Uow { get; }

        private readonly IMapper _mapper;

        public vTaskStateService(IUnitOfWork uow, IMapper mapper)
        {
            Uow = uow;
            _mapper = mapper;
        }

        public TaskStateDTO GetById(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The task state id value is not set", String.Empty);
            }

            var task = Uow.TaskStates.GetById(id.Value);

            if (task == null)
            {
                throw new ValidationException($"The task state with id = {id.Value} was not found", String.Empty);
            }

            return _mapper.Map<TaskState, TaskStateDTO>(task);

        }

        public void Dispose()
        {
            Uow.Dispose();
        }

        public IEnumerable<TaskStateDTO> GetAll()
        {
            var taskStates = Uow.TaskStates.GetAll();

            return _mapper.Map<IEnumerable<TaskState>, IEnumerable<TaskStateDTO>>(taskStates);
        }
    }
}
