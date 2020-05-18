using HIMS.Server.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIMS.Server.Models.Tasks
{
    public class TaskManagePageViewModel
    {
        public TaskViewModel taskViewModel { get; set; }
        public IEnumerable<UserTaskViewModel> userTaskListViewModel { get; set; }
        public TaskStateViewModel taskStateViewModel { get; set; }
        public IEnumerable<TaskStateViewModel> taskStateListViewModel { get; set; }
        public IEnumerable<vUserProfileViewModel> userProfileListViewModel { get; set; }
    }
}