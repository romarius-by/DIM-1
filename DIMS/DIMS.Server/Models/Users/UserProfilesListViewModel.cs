using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIMS.Server.Models
{
    public class UserProfilesListViewModel
    {

        public IEnumerable<UserProfileViewModel> UserProfiles { get; set; }
    }
}