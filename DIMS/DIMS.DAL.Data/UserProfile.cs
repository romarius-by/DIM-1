//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HIMS.EF.DAL.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class UserProfile
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserProfile()
        {
            this.UserTasks = new HashSet<UserTask>();
        }
    
        [Key]
        public int UserId { get; set; }
        public int DirectionId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public string Education { get; set; }
        public System.DateTime BirthDate { get; set; }
        public Nullable<double> UniversityAverageScore { get; set; }
        public Nullable<double> MathScore { get; set; }
        public string Address { get; set; }
        public string MobilePhone { get; set; }
        public string Skype { get; set; }
        public System.DateTime StartDate { get; set; }
    
        public virtual Direction Direction { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserTask> UserTasks { get; set; }
    }
}
