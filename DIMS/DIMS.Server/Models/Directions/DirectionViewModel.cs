using HIMS.Server.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIMS.Server.Models.Directions
{
    public class DirectionViewModel
    {
        public int DirectionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual IEnumerable<UserProfileViewModel> UserProfiles { get; set; }

    }
}