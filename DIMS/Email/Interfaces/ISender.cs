using HIMS.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Interfaces
{
    public interface ISender
    {
        Task MessageToUserAsync(UserDTO user, string subject, string html);
        Task MessageToUserAsync(IEnumerable<UserDTO> users, string subject, string html);
    }
}
