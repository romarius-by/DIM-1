using AutoMapper;
using DIMS.EF.DAL.Data;

namespace DIMS.BL.DTO
{
    internal class vUserProfileDTOProfile : Profile
    {
        public vUserProfileDTOProfile()
        {
            CreateMap<vUserProfileDTO, vUserProfile>();
        }
    }
}
