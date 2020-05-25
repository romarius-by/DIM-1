using System.Collections.Generic;

namespace DIMS.BL.DTO
{
    public class TaskStateDTO
    {
        public TaskStateDTO()
        {
            UserTasks = new HashSet<UserTaskDTO>();
        }

        public int StateId { get; set; }
        public string StateName { get; set; }
        public virtual ICollection<UserTaskDTO> UserTasks { get; set; }
    }
}
