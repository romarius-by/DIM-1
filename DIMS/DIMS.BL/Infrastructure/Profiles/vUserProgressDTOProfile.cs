using AutoMapper;
using DIMS.EF.DAL.Data;

namespace DIMS.BL.DTO
{
    internal class vUserProgressDTOProfile : Profile
    {
        public vUserProgressDTOProfile()
        {
            CreateMap<vUserProgressDTO, vUserProgress>();
        }
    }
}
