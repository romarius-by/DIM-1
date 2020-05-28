using AutoMapper;
using DIMS.EF.DAL.Data;

namespace DIMS.BL.DTO
{
    internal class vUserTaskDTOProfile : Profile
    {
        public vUserTaskDTOProfile()
        {
            CreateMap<vUserTaskDTO, vUserTask>();
        }
    }
}
