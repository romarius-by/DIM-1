using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Models
{
    public class UserDTO
    {
        // Surrogate PK
        public string Id { get; set; }
        // UserSecurity
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        // UserProfile
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
