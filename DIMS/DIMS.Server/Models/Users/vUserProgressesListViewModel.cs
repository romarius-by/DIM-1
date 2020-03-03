using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIMS.Server.Models.Users
{
    public class vUserProgressesListViewModel
    {
        public vUserProfileViewModel vUserProfile { get; set; }
        public IEnumerable<vUserProgressViewModel> vUserProgresses { get; set; }
    }
}