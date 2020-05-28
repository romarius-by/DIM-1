using AutoMapper;
using HIMS.BL.DTO;
using HIMS.BL.Infrastructure;
using HIMS.BL.Interfaces;
using HIMS.EF.DAL.Data;
using HIMS.EF.DAL.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Services
{
    public class vUserTrackService : IvUserTrackService
    {

        private IUnitOfWork Database;
        private TaskTrackRepository Repository;

        public vUserTrackService(IUnitOfWork uow, TaskTrackRepository repository)
        {
            Database = uow;
            Repository = repository;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public vUserTrackDTO GetById(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The view user track id value is not set", String.Empty);

            var _vUserTrack = Database.vUserTracks.GetById(id.Value);

            if (_vUserTrack == null)
                throw new ValidationException($"The view user track with id = {id.Value} was not found", String.Empty);

            return Mapper.Map<vUserTrack, vUserTrackDTO>(_vUserTrack);
        }

        public IEnumerable<vUserTrackDTO> GetAll()
        {
            var vUserTracks = Database.vUserTracks.GetAll().ToList();

            return Mapper.Map<List<vUserTrack>, ICollection<vUserTrackDTO>>(vUserTracks);
        }

        public IEnumerable<vUserTrackDTO> GetTracksForUser(int userId)
        {
            var vUserTracks = Repository.GetByUserId(userId);

            return Mapper.Map<IEnumerable<vUserTrack>, IEnumerable<vUserTrackDTO>>(vUserTracks);
        }
    }
}
