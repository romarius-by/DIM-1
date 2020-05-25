using System;
using System.Collections.Generic;

namespace DIMS.BL.DTO
{
    public class TaskDTO
    {

        public TaskDTO()
        {
            UserTasks = new HashSet<UserTaskDTO>();
        }

        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public virtual ICollection<UserTaskDTO> UserTasks { get; set; }
    }
}
