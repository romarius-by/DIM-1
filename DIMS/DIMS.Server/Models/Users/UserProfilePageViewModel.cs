using HIMS.Server.Models.Directions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIMS.Server.Models.Users
{
    public class UserProfilePageViewModel
    {
        

        public UserProfileViewModel UserProfileViewModel { get; set; }
        public UserProfilesListViewModel UserProfilesListViewModel { get; set; }
        public vUserProfileViewModel vUserProfileViewModel { get; set; }
        public vUserProfilesListViewModel vUserProfilesListViewModel { get; set; }
        public vUserProgressesListViewModel vUserProgressesListViewModel { get; set; }

        public IEnumerable<DirectionViewModel> DirectionViewModels { get; set; }
    }
}