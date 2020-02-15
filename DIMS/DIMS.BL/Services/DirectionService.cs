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
    public class DirectionService : IDirectionService
    {

        private IUnitOfWork dimsDatabase { get; }
        
        public DirectionService(IUnitOfWork unitOfWork)
        {
            dimsDatabase = unitOfWork;
        }

        public void DeleteDirection(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The Direction's id value is not set", String.Empty);

            dimsDatabase.Directions.Delete(id.Value);
            dimsDatabase.Save();
        }

        public void Dispose()
        {
            dimsDatabase.Dispose();
        }

        public DirectionDTO GetDirection(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The Direction's id value is not set", String.Empty);

            var direction = dimsDatabase.Directions.Get(id.Value);

            if (direction == null)
                throw new ValidationException($"The Direction with id = ${id.Value} was not found", String.Empty);

            return Mapper.Map<Direction, DirectionDTO>(direction);
        }

        public IEnumerable<DirectionDTO> GetDirections()
        {
            return Mapper.Map<IEnumerable<Direction>, List<DirectionDTO>>(dimsDatabase.Directions.GetAll());
        }

        public void SaveDirection(DirectionDTO direction)
        {
            var _direction = new Direction
            {
                Name = direction.Name,
                Description = direction.Description,
                UserProfiles = Mapper.Map<List<UserProfileDTO>, ICollection <UserProfile>>(direction.UserProfiles.ToList())
            };

        }

        public void UpdateDireciton(DirectionDTO direction)
        {
            var _direction = dimsDatabase.Directions.Get(direction.DirectionId);

            if (_direction != null)
            {
                Mapper.Map(direction, _direction);

                dimsDatabase.Save();
            }


        }
    }
}
