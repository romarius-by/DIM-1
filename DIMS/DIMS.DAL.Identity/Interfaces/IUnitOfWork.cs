using HIMS.EF.DAL.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.EF.DAL.Identity.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationRoleManager ApplicationRoleManager { get; }
        ApplicationUserManager UserSecurityManager { get; }
        Task SaveAsync();
    }
}
