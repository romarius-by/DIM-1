using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Infrastructure;
using DIMS.BL.Interfaces;
using DIMS.EF.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS.BL.Services
{
    public class DirectionService : IDirectionService
    {

        private IUnitOfWork Database { get; }

        public DirectionService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        public void DeleteById(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The Direction's id value is not set", String.Empty);
            }

            Database.Directions.DeleteById(id.Value);
            Database.Save();
        }

        public async Task<bool> DeleteByIdAsync(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The Direction's id value is not set", String.Empty);
            }

            var direction = await Database.Directions.DeleteByIdAsync(id.Value);

            if (direction != null)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public DirectionDTO GetById(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The Direction's id value is not set", String.Empty);
            }

            var direction = Database.Directions.GetById(id.Value);

            if (direction == null)
            {
                throw new ValidationException($"The Direction with id = ${id.Value} was not found", String.Empty);
            }

            return Mapper.Map<Direction, DirectionDTO>(direction);
        }

        public IEnumerable<DirectionDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<Direction>, List<DirectionDTO>>(Database.Directions.GetAll());
        }

        public void Save(DirectionDTO direction)
        {
            var _direction = new Direction
            {
                Name = direction.Name,
                Description = direction.Description,
                UserProfiles = Mapper.Map<List<UserProfileDTO>, ICollection<UserProfile>>(direction.UserProfiles.ToList())
            };

            Database.Directions.Create(_direction);

            Database.Save();
        }

        public void Update(DirectionDTO direction)
        {
            var _direction = Database.Directions.GetById(direction.DirectionId);

            if (_direction != null)
            {
                Mapper.Map(direction, _direction);

                Database.Save();
            }
        }
    }
}
