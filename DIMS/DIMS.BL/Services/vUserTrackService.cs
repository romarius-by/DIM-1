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
    public class vUserTrackService : IvUserTrackService
    {

        private IUnitOfWork database;

        public vUserTrackService(IUnitOfWork uow)
        {
            database = uow; 
        }

        public void Dispose()
        {
            database.Dispose();
        }

        public vUserTrackDTO GetById(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The view user track id value is not set", String.Empty);

            var _vUserTrack = database.vUserTracks.GetById(id.Value);

            if (_vUserTrack == null)
                throw new ValidationException($"The view user track with id = {id.Value} was not found", String.Empty);

            return Mapper.Map<vUserTrack, vUserTrackDTO>(_vUserTrack);
        }

        public IEnumerable<vUserTrackDTO> GetAll()
        {
            return Mapper.Map<List<vUserTrack>, ICollection<vUserTrackDTO>>(
                database.vUserTracks.GetAll().ToList());
        }
    }
}
