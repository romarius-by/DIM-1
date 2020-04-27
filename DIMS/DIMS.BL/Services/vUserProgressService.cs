﻿using AutoMapper;
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

        private IUnitOfWork Database;

        public vUserProgressService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public IEnumerable<vUserProgressDTO> GetAll()
        {
            return Mapper.Map<List<vUserProgress>, ICollection<vUserProgressDTO>>(
                Database.vUserProgresses.GetAll().ToList());
        }

        public vUserProgressDTO GetById(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The view user progress id value is not set", String.Empty);

            var _vUserProgress = Database.vUserProgresses.GetById(id.Value);

            if (_vUserProgress == null)
                throw new ValidationException($"The view user progress with id = {id.Value} was not found", String.Empty);

            return Mapper.Map<vUserProgress, vUserProgressDTO>(_vUserProgress);

        }

        public IEnumerable<vUserProgressDTO> GetByUserId(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The view user progress id value is not set", String.Empty);

            var userProgress = Database.vUserProgresses.Find(m => m.UserId == id.Value);

            if (userProgress == null)
            {
                throw new ValidationException($"The user with id = {id.Value} was not found", String.Empty);
            }

            return Mapper.Map<IEnumerable<vUserProgress>, List<vUserProgressDTO>>(userProgress);
        }
    }
}
