using HIMS.BL.Infrastructure;
using HIMS.BL.Models;
using HIMS.EF.DAL.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
        Task<OperationDetails> DeleteByEmail(string email);

        Task<ApplicationUser> FindByEmail(string email);
        Task<ApplicationUser> FindByName(string email);
        Task<ApplicationUser> FindById(string email);

        Task<string> GenerateToken(UserDTO user);
    }
}
