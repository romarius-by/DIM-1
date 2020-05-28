using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Infrastructure;
using DIMS.BL.Interfaces;
using DIMS.EF.DAL.Data;
using System.Collections.Generic;
using System.Linq;

namespace DIMS.BL.Services
{
    public class vTaskService : IvTaskService
    {

        private readonly IUnitOfWork Database;
        private readonly IMapper _mapper;

        public vTaskService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public vTaskDTO GetById(int id)
        {
            var _vTask = Database.vTasks.GetById(id);

            if (_vTask == null)
            {
                throw new ValidationException($"The vTask with id = {id} was not found", string.Empty);
            }

            return _mapper.Map<vTask, vTaskDTO>(_vTask);
        }

        public IEnumerable<vTaskDTO> GetAll()
        {
            return _mapper.Map<List<vTask>, ICollection<vTaskDTO>>(
                Database.vTasks.GetAll().ToList());
        }

        public void Update(vTaskDTO vTaskDTO)
        {
            var task = Database.Tasks.GetById(vTaskDTO.TaskId);

            if (task != null)
            {
                _mapper.Map(vTaskDTO, task);

                Database.Save();
            }
        }
    }
}
