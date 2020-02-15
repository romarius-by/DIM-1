using HIMS.BL.DTO;
using HIMS.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Tests.DAL.BL.Services
{
    public class StubUserProfileService : IUserProfileService
    {


        public void DeleteUserProfileByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public void DeleteUserProfileById(int? id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public UserProfileDTO GetUserProfileById(int? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserProfileDTO> GetUserProfiles()
        {
            throw new NotImplementedException();
        }

        public void SaveUserProfile(UserProfileDTO userProfile)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserProfile(UserProfileDTO userProfile)
        {
            throw new NotImplementedException();
        }
    }
}
