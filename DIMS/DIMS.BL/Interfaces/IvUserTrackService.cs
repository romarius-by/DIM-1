using DIMS.BL.DTO;

namespace DIMS.BL.Interfaces
{
    public interface IVUserTrackService : IVService<VUserTrackDTO>
    {
        IEnumerable<vUserTrackDTO> GetTracksForUser(int userId);
    }
}
