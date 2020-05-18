using AutoMapper;
using HIMS.BL.DTO;
using HIMS.BL.Models;
using HIMS.EF.DAL.Data;
using HIMS.Server.Models;
using HIMS.Server.Models.Directions;
using HIMS.Server.Models.Tasks;
using HIMS.Server.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIMS.Server.utils
{
    public static class AutomapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Sample, SampleDTO>();
                cfg.CreateMap<SampleDTO, Sample>();
                cfg.CreateMap<SampleViewModel, SampleDTO>();
                cfg.CreateMap<UserProfileViewModel, UserProfileDTO>();
                cfg.CreateMap<UserProfile, UserProfileDTO>();
                cfg.CreateMap<UserProfileDTO, UserProfile>();
                cfg.CreateMap<DirectionDTO, Direction>();
                cfg.CreateMap<Direction, DirectionDTO>();
                cfg.CreateMap<DirectionDTO, DirectionViewModel>();
                cfg.CreateMap<DirectionViewModel, DirectionDTO>();
                cfg.CreateMap<UserProfileDTO, UserProfileViewModel>();
                cfg.CreateMap<UserTaskViewModel, vUserTaskDTO>();
                cfg.CreateMap<UserTaskDTO, vUserTaskDTO>();

                cfg.CreateMap<vUserProfileDTO, vUserProfileViewModel>();
                cfg.CreateMap<vUserProfileViewModel, vUserProfileDTO>();
                cfg.CreateMap<vUserTaskDTO, vUserTask>();
                cfg.CreateMap<vUserTaskDTO, UserTask>();
                cfg.CreateMap<vTaskDTO, vTask>();
                cfg.CreateMap<vTaskDTO, Task>();

                cfg.CreateMap<vUserProgress, vUserProgressDTO>();
                cfg.CreateMap<vUserProgressDTO, vUserProgress>();
                cfg.CreateMap<vUserProgressDTO, vUserProgressViewModel>();
                cfg.CreateMap<TaskDTO, TaskViewModel>();
                cfg.CreateMap<TaskDTO, Task>();
                cfg.CreateMap<UserTaskDTO, UserTask>();



            });
        }
    }
}