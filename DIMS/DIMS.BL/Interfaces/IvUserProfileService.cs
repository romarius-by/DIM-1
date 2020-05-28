using DIMS.BL.DTO;
using System.Threading.Tasks;

namespace DIMS.BL.Interfaces
{
    public interface IVUserProfileService : IVService<vUserProfileDTO>
    {
        Task<vUserProfileDTO> GetByEmailAsync(string email);

    }
}
