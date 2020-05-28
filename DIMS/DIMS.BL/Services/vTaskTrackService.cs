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
    public class vTaskTrackService : IvTaskTrackService
    {

        private IUnitOfWork Database;

        public vTaskTrackService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public vTaskTrackDTO GetById(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The TaskTrack id value is not set", String.Empty);

            var taskTrack = Database.TaskTracks.GetById(id.Value);

            if (taskTrack == null)
                throw new ValidationException($"The TaskTrack with id = {id.Value} was not found", String.Empty);

            return Mapper.Map<TaskTrack, vTaskTrackDTO>(taskTrack);
        }

        public IEnumerable<vTaskTrackDTO> GetAll()
        {
            var taskTrackDto = Database.TaskTracks.GetAll().ToList();
            return Mapper.Map<List<TaskTrack>, ICollection<vTaskTrackDTO>>(taskTrackDto);
        }

        public void Update(vTaskTrackDTO vTaskTrackDTO)
        {
            var taskTrack = Database.TaskTracks.GetById(vTaskTrackDTO.TaskTrackId);

            if (taskTrack != null)
            {
                Mapper.Map(vTaskTrackDTO, taskTrack);

                Database.Save();
            }
        }
    }
}
