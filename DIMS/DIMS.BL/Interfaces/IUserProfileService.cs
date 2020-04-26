using HIMS.BL.DTO;
using HIMS.BL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Interfaces
{
    public interface IUserProfileService : IService<UserProfileDTO>
    {
        Task<bool> DeleteByEmailAsync(string email);
    }
}
