using AutoMapper;
using DIMS.BL.Models;
using DIMS.EF.DAL.Data;

namespace DIMS.BL.DTO
{
    internal class SampleDTOProfile : Profile
    {
        public SampleDTOProfile()
        {
            CreateMap<SampleDTO, Sample>();
        }
    }
}
