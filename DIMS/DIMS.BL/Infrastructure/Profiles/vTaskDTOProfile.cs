using AutoMapper;
using DIMS.EF.DAL.Data;

namespace DIMS.BL.DTO
{
    internal class vTaskDTOProfile : Profile
    {
        public vTaskDTOProfile()
        {
            CreateMap<vTaskDTO, vTask>();
        }
    }
}
