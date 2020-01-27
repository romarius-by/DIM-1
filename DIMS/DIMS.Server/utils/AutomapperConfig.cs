using AutoMapper;
using HIMS.BL.Models;
using HIMS.EF.DAL.Data;
using HIMS.Server.Models;
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
            });
        }
    }
}