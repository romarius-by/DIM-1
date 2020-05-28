using AutoMapper;
using DIMS.EF.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIMS.BL.DTO
{
    class UserProfileDTOProfile : Profile
    {
        public UserProfileDTOProfile()
        {
            CreateMap<UserProfileDTO, UserProfile>();
        }
    }
}
