using DIMS.Server.Models.Directions;
using System.Collections.Generic;

namespace DIMS.Server.Models.Users
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