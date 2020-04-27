using HIMS.BL.Interfaces;
using HIMS.BL.Models;
using HIMS.EF.DAL.Identity.Interfaces;
using HIMS.EF.DAL.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Services
{
    public class UserAuthService : IAuthService<UserDTO>
    {

        private IUnitOfWork Database { get; }

        public UserAuthService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<string> GenerateTokenAsync(UserDTO userDto)
        {
            var user = await Database.UserSecurityManager.FindByEmailAsync(userDto.Email).ConfigureAwait(false);

            var token = await Database.UserSecurityManager.GenerateEmailConfirmationTokenAsync(user.Id).ConfigureAwait(false);

            return token;
        }
        
    }
}
