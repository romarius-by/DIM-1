using HIMS.Server.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIMS.Server.Models.Tasks
{
    public class TasksListViewModel
    {
        public IEnumerable<vTaskViewModel> Tasks { get; set; }
        public IEnumerable<vUserProfileViewModel> UserProfiles { get; set; }
    }
}