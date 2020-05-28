using DIMS.BL.DTO;
using System.Threading.Tasks;

namespace DIMS.BL.Interfaces
{
    public interface IvUserProfileService : IVService<vUserProfileDTO>
    {
        Task<vUserProfileDTO> GetByEmailAsync(string email);

    }
}
