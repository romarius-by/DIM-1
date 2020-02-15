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

        private IUnitOfWork Database;

        public vUserTrackService(IUnitOfWork uow)
        {
            Database = uow; 
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public vUserTrackDTO GetVUserTrack(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The view user track id value is not set", String.Empty);

            var _vUserTrack = Database.vUserTracks.Get(id.Value);

            if (_vUserTrack == null)
                throw new ValidationException($"The view user track with id = {id.Value} was not found", String.Empty);

            return Mapper.Map<vUserTrack, vUserTrackDTO>(_vUserTrack);
        }

        public ICollection<vUserTrackDTO> GetVUserTracks()
        {
            return Mapper.Map<List<vUserTrack>, ICollection<vUserTrackDTO>>(
                Database.vUserTracks.GetAll().ToList());
        }
    }
}
