using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.DTO
{
    public class UserProfileDTO
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserProfileDTO()
        {
            this.UserTasks = new HashSet<UserTaskDTO>();
        }

        public int UserId { get; set; }
        public int DirectionId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public string Education { get; set; }
        public DateTime? BirthDate { get; set; }
        public double? UniversityAverageScore { get; set; }
        public double? MathScore { get; set; }
        public string Address { get; set; }
        public string MobilePhone { get; set; }
        public string Skype { get; set; }
        public DateTime? StartDate { get; set; }
        public virtual DirectionDTO Direction { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserTaskDTO> UserTasks { get; set; }

    }
}
