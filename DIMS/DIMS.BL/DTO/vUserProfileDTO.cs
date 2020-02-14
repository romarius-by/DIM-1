using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.DTO
{
    public class vUserProfileDTO
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Direction { get; set; }
        public string Sex { get; set; }
        public string Education { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<double> UniversityAverageScore { get; set; }
        public Nullable<double> MathScore { get; set; }
        public string Address { get; set; }
        public string MobilePhone { get; set; }
        public string Skype { get; set; }
        public System.DateTime StartDate { get; set; }

    }
}
