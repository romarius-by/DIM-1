using HIMS.BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIMS.Server.Models.Tasks
{
    public class TaskStateViewModel
    {
        public int StateId { get; set; }
        public string StateName { get; set; }

        public IEnumerable<UserTaskDTO> UserTasks { get; set; }
    }
}