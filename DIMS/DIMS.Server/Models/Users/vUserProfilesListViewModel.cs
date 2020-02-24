using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIMS.Server.Models.Users
{
    public class vUserProfilesListViewModel
    {
        public IEnumerable<vUserProfileViewModel> vUserProfiles { get; set; }
    }
}