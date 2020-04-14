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

        public void DeleteItem(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The Direction's id value is not set", String.Empty);

            dimsDatabase.Directions.Delete(id.Value);
            dimsDatabase.Save();
        }

        public async Task<OperationDetails> DeleteItemAsync(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The Direction's id value is not set", String.Empty);

            var res = await dimsDatabase.Directions.DeleteAsync(id.Value);

            if (res != null)
            {
                return new OperationDetails(true, "The Direction has been successfully deleted! Direction: ", res.Name);
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

        public DirectionDTO GetItem(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The Direction's id value is not set", String.Empty);

            var direction = dimsDatabase.Directions.Get(id.Value);

            if (direction == null)
                throw new ValidationException($"The Direction with id = ${id.Value} was not found", String.Empty);

            return Mapper.Map<Direction, DirectionDTO>(direction);
        }

        public IEnumerable<DirectionDTO> GetItems()
        {
            return Mapper.Map<IEnumerable<Direction>, List<DirectionDTO>>(dimsDatabase.Directions.GetAll());
        }

        public void SaveItem(DirectionDTO direction)
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

        public void UpdateItem(DirectionDTO direction)
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
