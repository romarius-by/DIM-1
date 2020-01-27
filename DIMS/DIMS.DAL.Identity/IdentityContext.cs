using HIMS.EF.DAL.Identity.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.EF.DAL.Identity
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityContext(string conectionString) : base(conectionString)
        {
        }
    }
}
