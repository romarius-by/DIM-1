using HIMS.BL.Infrastructure;
using HIMS.BL.Interfaces;
using HIMS.BL.Models;
using HIMS.EF.DAL.Identity.Interfaces;
using HIMS.EF.DAL.Identity.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork Database { get; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await Database.UserSecurityManager.FindByEmailAsync(userDto.Email).ConfigureAwait(false);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                var result = await Database.UserSecurityManager.CreateAsync(user, userDto.Password).ConfigureAwait(false);

                if (result.Errors.Any())
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

                // add a role
                await Database.UserSecurityManager.AddToRoleAsync(user.Id, userDto.Role).ConfigureAwait(false);

                // create user profile
                //ClientProfile clientProfile = new ClientProfile { Id = user.Id, Address = userDto.Address, Name = userDto.Name };
                //Database.ClientManager.Create(clientProfile);

                await Database.SaveAsync().ConfigureAwait(false);
                return new OperationDetails(true, "The registration was done successfully!", "");
            }
            else
            {
                return new OperationDetails(false, "The user with the same name already exists!", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // finding the user
            ApplicationUser user = await Database.UserSecurityManager.FindAsync(userDto.Email, userDto.Password).ConfigureAwait(false);
            // authorize and return the ClaimsIdentity object
            if (user != null)
            {
                claim = await Database.UserSecurityManager.CreateIdentityAsync(user,
                                           DefaultAuthenticationTypes.ApplicationCookie).ConfigureAwait(false);
            }

            return claim;
        }

        // initial data
        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.ApplicationRoleManager.FindByNameAsync(roleName).ConfigureAwait(false);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.ApplicationRoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto).ConfigureAwait(false);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
