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
    public class vUserTrackService : IvUserTrackService
    {

        private readonly IUnitOfWork Database;

        public vUserTrackService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public vUserTrackDTO GetById(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The view user track id value is not set", String.Empty);
            }

            var _vUserTrack = Database.vUserTracks.GetById(id.Value);

            if (_vUserTrack == null)
            {
                throw new ValidationException($"The view user track with id = {id.Value} was not found", String.Empty);
            }

            return Mapper.Map<vUserTrack, vUserTrackDTO>(_vUserTrack);
        }

        public IEnumerable<vUserTrackDTO> GetAll()
        {
            return Mapper.Map<List<vUserTrack>, ICollection<vUserTrackDTO>>(
                Database.vUserTracks.GetAll().ToList());
        }
    }
}
