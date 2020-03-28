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
    public class vUserProfileService : IvUserProfileService
    {

        private IUnitOfWork database;

        public vUserProfileService(IUnitOfWork uow)
        {
            database = uow;
        }

        public void Dispose()
        {
            database.Dispose();
        }

        public vUserProfileDTO GetItem(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The view user profile id value is not set", String.Empty);

            var vUserProfile = database.vUserProfiles.Get(id.Value);

            if (vUserProfile == null)
                throw new ValidationException($"The view user profile with id = {id.Value} was not found", String.Empty);

            return Mapper.Map<vUserProfile, vUserProfileDTO>(vUserProfile);
        }

        public vUserProfileDTO GetVUserProfileByEmail(string email)
        {
            if (email == null)
                throw new ValidationException("The view user profile email is not set", String.Empty);

            var vUserProfile = database.vUserProfiles.GetByEmail(email);

            if (vUserProfile == null)
                throw new ValidationException($"The view user profile with email = {email} was not found", String.Empty);

            return Mapper.Map<vUserProfile, vUserProfileDTO>(vUserProfile);
        }

        public IEnumerable<vUserProfileDTO> GetItems()
        {
            return Mapper.Map<List<vUserProfile>, ICollection<vUserProfileDTO>>(
                database.vUserProfiles.GetAll().ToList());
        }

        /*public async Task<OperationDetails> GetVUserProfileByEmailAsync(string email)
        {
            var vUserProfile = database.vUserProfiles.GetByEmail(email);
            return Mapper.Map<vUserProfile, vUserProfileDTO>(vUserProfile);
        }*/
    }
}
