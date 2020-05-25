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
    public class vUserProfileService : IvUserProfileService
    {

        private readonly IUnitOfWork Database;

        public vUserProfileService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public vUserProfileDTO GetById(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The view user profile id value is not set", String.Empty);
            }

            var vUserProfile = Database.vUserProfiles.GetById(id.Value);

            if (vUserProfile == null)
            {
                throw new ValidationException($"The view user profile with id = {id.Value} was not found", String.Empty);
            }

            return Mapper.Map<vUserProfile, vUserProfileDTO>(vUserProfile);
        }

        public async Task<vUserProfileDTO> GetByEmailAsync(string email)
        {
            if (email == null)
            {
                throw new ValidationException("The view user profile email is not set", String.Empty);
            }

            var vUserProfile = await Database.vUserProfiles.GetByEmailAsync(email);

            if (vUserProfile == null)
            {
                throw new ValidationException($"The view user profile with email = {email} was not found", String.Empty);
            }

            return Mapper.Map<vUserProfile, vUserProfileDTO>(vUserProfile);
        }

        public IEnumerable<vUserProfileDTO> GetAll()
        {
            return Mapper.Map<List<vUserProfile>, ICollection<vUserProfileDTO>>(
                Database.vUserProfiles.GetAll().ToList());
        }
    }
}
