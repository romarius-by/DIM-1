using DIMS.BL.DTO;

namespace DIMS.BL.Interfaces
{
    public interface IvTaskService : IVService<vTaskDTO>
    {
        void Update(vTaskDTO vTaskDTO);
    }
}
