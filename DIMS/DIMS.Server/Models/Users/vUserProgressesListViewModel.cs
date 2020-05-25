using System.Collections.Generic;

namespace DIMS.Server.Models.Users
{
    public class vUserProgressesListViewModel
    {
        public vUserProgressesListViewModel(vUserProfileViewModel vUserProfile, IEnumerable<vUserProgressViewModel> vUserProgresses)
        {
            this.vUserProfile = vUserProfile;
            this.vUserProgresses = vUserProgresses;
        }

        public vUserProfileViewModel vUserProfile { get; set; }
        public IEnumerable<vUserProgressViewModel> vUserProgresses { get; set; }
    }
}