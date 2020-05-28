﻿using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Infrastructure;
using DIMS.BL.Interfaces;
using DIMS.EF.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DIMS.BL.Services
{
    public class vUserProgressService : IvUserProgressService
    {
        private readonly IUnitOfWork Database;

        private readonly IMapper _mapper;

        public vUserProgressService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public IEnumerable<vUserProgressDTO> GetAll()
        {
            return _mapper.Map<List<vUserProgress>, ICollection<vUserProgressDTO>>(
                Database.vUserProgresses.GetAll().ToList());
        }

        public vUserProgressDTO GetById(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The view user progress id value is not set", String.Empty);
            }

            var _vUserProgress = Database.vUserProgresses.GetById(id.Value);

            if (_vUserProgress == null)
            {
                throw new ValidationException($"The view user progress with id = {id.Value} was not found", String.Empty);
            }

            return _mapper.Map<vUserProgress, vUserProgressDTO>(_vUserProgress);

        }

        public IEnumerable<vUserProgressDTO> GetByUserId(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The view user progress id value is not set", String.Empty);
            }

            var userProgress = Database.vUserProgresses.Find(m => m.UserId == id.Value);

            if (userProgress == null)
            {
                throw new ValidationException($"The user with id = {id.Value} was not found", String.Empty);
            }

            return _mapper.Map<IEnumerable<vUserProgress>, List<vUserProgressDTO>>(userProgress);
        }
    }
}
