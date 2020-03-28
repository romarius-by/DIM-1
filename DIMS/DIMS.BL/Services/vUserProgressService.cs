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
    public class vUserProgressService : IvUserProgressService
    {

        private IUnitOfWork database;

        public vUserProgressService(IUnitOfWork uow)
        {
            database = uow;
        }

        public void Dispose()
        {
            database.Dispose();
        }

        public IEnumerable<vUserProgressDTO> GetItems()
        {
            return Mapper.Map<List<vUserProgress>, ICollection<vUserProgressDTO>>(
                database.vUserProgresses.GetAll().ToList());
        }

        public vUserProgressDTO GetItem(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The view user progress id value is not set", String.Empty);

            var _vUserProgress = database.vUserProgresses.Get(id.Value);

            if (_vUserProgress == null)
                throw new ValidationException($"The view user progress with id = {id.Value} was not found", String.Empty);

            return Mapper.Map<vUserProgress, vUserProgressDTO>(_vUserProgress);

        }

        public IEnumerable<vUserProgressDTO> GetVUserProgressesByUserId(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The view user progress id value is not set", String.Empty);

            return Mapper.Map<IEnumerable<vUserProgress>, List<vUserProgressDTO>>(database.vUserProgresses.Find(m => m.UserId == id.Value));
        }
    }
}
