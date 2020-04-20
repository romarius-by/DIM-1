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

        public void DeleteById(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The Direction's id value is not set", String.Empty);

            dimsDatabase.Directions.DeleteById(id.Value);
            dimsDatabase.Save();
        }

        public async Task<OperationDetails> DeleteByIdAsync(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The Direction's id value is not set", String.Empty);

            var direction = await dimsDatabase.Directions.DeleteByIdAsync(id.Value);

            if (direction != null)
            {
                return new OperationDetails(true, "The Direction has been successfully deleted! Direction: ", direction.Name);
            }

            else
            {
                return new OperationDetails(false, "Something went wrong!", " ");
            }
            
        }

        public void Dispose()
        {
            dimsDatabase.Dispose();
        }

        public DirectionDTO GetById(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The Direction's id value is not set", String.Empty);

            var direction = dimsDatabase.Directions.GetById(id.Value);

            if (direction == null)
                throw new ValidationException($"The Direction with id = ${id.Value} was not found", String.Empty);

            return Mapper.Map<Direction, DirectionDTO>(direction);
        }

        public IEnumerable<DirectionDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<Direction>, List<DirectionDTO>>(dimsDatabase.Directions.GetAll());
        }

        public void Save(DirectionDTO direction)
        {
            var _direction = new Direction
            {
                Name = direction.Name,
                Description = direction.Description,
                UserProfiles = Mapper.Map<List<UserProfileDTO>, ICollection <UserProfile>>(direction.UserProfiles.ToList())
            };

            dimsDatabase.Directions.Create(_direction);

            dimsDatabase.Save();

        }

        public void Update(DirectionDTO direction)
        {
            var _direction = dimsDatabase.Directions.GetById(direction.DirectionId);

            if (_direction != null)
            {
                Mapper.Map(direction, _direction);

                dimsDatabase.Save();
            }


        }

        
    }
}
