using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIMS.BL.DTO
{
    public class UserTaskDTO
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserTaskDTO()
        {
            this.TaskTracks = new HashSet<TaskTrackDTO>();
        }

        public int UserTaskId { get; set; }
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public int StateId { get; set; }

        public virtual TaskDTO Task { get; set; }
        public virtual TaskStateDTO TaskState { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual IEnumerable<TaskTrackDTO> TaskTracks { get; set; }
        public virtual UserProfileDTO UserProfile { get; set; }
    }
}
