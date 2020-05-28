using DIMS.BL.DTO;

namespace DIMS.BL.Interfaces
{
    public interface IvTaskService : IvService<vTaskDTO>
    {
        void Update(vTaskDTO vTaskDTO);
    }
}
