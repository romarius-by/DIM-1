using HIMS.BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Interfaces
{
    public interface IUserProfileService
    {
        void SaveUserProfile(UserProfileDTO userProfile);
        UserProfileDTO GetUserProfileById(int? id);
        void UpdateUserProfile(UserProfileDTO userProfile);
        void DeleteUserProfileById(int? id);
        void DeleteUserProfileByEmail(string email);

        IEnumerable<UserProfileDTO> GetUserProfiles();
        void Dispose();
    }
}
