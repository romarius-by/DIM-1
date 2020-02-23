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

        private IUnitOfWork Database;

        public vTaskService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public vTaskDTO GetVTask(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The vTask id value is not set", String.Empty);

            var _vTask = Database.vTasks.Get(id.Value);

            if (_vTask == null)
                throw new ValidationException($"The vTask with id = {id.Value} was not found", String.Empty);

            return Mapper.Map<vTask, vTaskDTO>(_vTask);
        }

        public ICollection<vTaskDTO> GetVTasks()
        {
            return Mapper.Map<List<vTask>, ICollection<vTaskDTO>>(
                Database.vTasks.GetAll().ToList());
        }
    }
}
