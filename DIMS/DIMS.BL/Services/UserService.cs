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
        private IUnitOfWork database { get; }

        public UserService(IUnitOfWork uow)
        {
            database = uow;
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await database.UserSecurityManager.FindByEmailAsync(userDto.Email).ConfigureAwait(false);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                var result = await database.UserSecurityManager.CreateAsync(user, userDto.Password).ConfigureAwait(false);

                if (result.Errors.Any())
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

                // add a role
                await database.UserSecurityManager.AddToRoleAsync(user.Id.ToString(), userDto.Role).ConfigureAwait(false);

                await database.SaveAsync().ConfigureAwait(false);
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
            ApplicationUser user = await database.UserSecurityManager.FindAsync(userDto.Email, userDto.Password).ConfigureAwait(false);
            // authorize and return the ClaimsIdentity object
            if (user != null)
            {
                claim = await database.UserSecurityManager.CreateIdentityAsync(user,
                                           DefaultAuthenticationTypes.ApplicationCookie).ConfigureAwait(false);
            }

            return claim;
        }

        // initial data
        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await database.ApplicationRoleManager.FindByNameAsync(roleName).ConfigureAwait(false);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await database.ApplicationRoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto).ConfigureAwait(false);
        }

        public void Dispose()
        {
            database.Dispose();
        }

        public async Task<OperationDetails> DeleteUserByEmail(string email)
        {
            ApplicationUser user = await database.UserSecurityManager.FindByEmailAsync(email);

            if (user != null)
            {
                var result = database.UserSecurityManager.Delete(user);

                if (result.Errors.Count() > 0)
                {
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                }

                return new OperationDetails(true, "User deletion by Email was done successfully! Email: ", email);
            }
            else
            {
                return new OperationDetails(false, "The user with such Email not found! Email: ", email);
            }
        }
    }
}
