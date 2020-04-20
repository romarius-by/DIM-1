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
    public class vTaskService : IvTaskService
    {

        private IUnitOfWork database;

        public vTaskService(IUnitOfWork uow)
        {
            database = uow;
        }

        public void Dispose()
        {
            database.Dispose();
        }

        public vTaskDTO GetById(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The vTask id value is not set", String.Empty);

            var _vTask = database.vTasks.GetById(id.Value);

            if (_vTask == null)
                throw new ValidationException($"The vTask with id = {id.Value} was not found", String.Empty);

            return Mapper.Map<vTask, vTaskDTO>(_vTask);
        }

        public IEnumerable<vTaskDTO> GetAll()
        {
            return Mapper.Map<List<vTask>, ICollection<vTaskDTO>>(
                database.vTasks.GetAll().ToList());
        }
    }
}
