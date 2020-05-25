using DIMS.BL.DTO;
using System.Threading.Tasks;

namespace DIMS.BL.Interfaces
{
    public interface IvUserProfileService : IvService<vUserProfileDTO>
    {
        Task<vUserProfileDTO> GetByEmailAsync(string email);

    }
}
