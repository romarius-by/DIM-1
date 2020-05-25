using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Infrastructure;
using DIMS.BL.Interfaces;
using DIMS.EF.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DIMS.BL.Services
{
    public class vTaskService : IvTaskService
    {

        private readonly IUnitOfWork Database;

        public vTaskService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public vTaskDTO GetById(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The vTask id value is not set", String.Empty);
            }

            var _vTask = Database.vTasks.GetById(id.Value);

            if (_vTask == null)
            {
                throw new ValidationException($"The vTask with id = {id.Value} was not found", String.Empty);
            }

            return Mapper.Map<vTask, vTaskDTO>(_vTask);
        }

        public IEnumerable<vTaskDTO> GetAll()
        {
            return Mapper.Map<List<vTask>, ICollection<vTaskDTO>>(
                Database.vTasks.GetAll().ToList());
        }
    }
}
