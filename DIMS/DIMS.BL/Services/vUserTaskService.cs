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
    public class vUserTaskService : IvUserTaskService
    {

        private IUnitOfWork Database;

        public vUserTaskService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public vUserTaskDTO GetItem(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The view user task id value is not set", String.Empty);

            var _vUserTask = Database.vUserTasks.Get(id.Value);

            if (_vUserTask == null)
                throw new ValidationException($"The view user task with id = {id.Value} was not found", String.Empty);

            return Mapper.Map<vUserTask, vUserTaskDTO>(_vUserTask);
        }

        public IEnumerable<vUserTaskDTO> GetItems()
        {
            return Mapper.Map<List<vUserTask>, ICollection<vUserTaskDTO>>(
                Database.vUserTasks.GetAll().ToList());
        }
    }
}
