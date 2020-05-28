using AutoMapper;
using Ninject;
using Ninject.Modules;

namespace DIMS.Server.utils
{
    public class AutoMapperModule : NinjectModule
    {
        public override void Load()
        {
            var mapperConfiguration = CreateConfiguration();
            Bind<MapperConfiguration>().ToConstant(mapperConfiguration).InSingletonScope();

            // This teaches Ninject how to create automapper instances say if for instance
            // MyResolver has a constructor with a parameter that needs to be injected
            Bind<IMapper>().ToMethod(ctx =>
                 new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));
        }

        private MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // Add all profiles in current assembly
                cfg.AddMaps(GetType().Assembly);
                //cfg.CreateMap<Sample, SampleDTO>();
            });

            return config;
        }
    }


    //cfg.CreateMap<Sample, SampleDTO>();
    //cfg.CreateMap<SampleDTO, Sample>();
    //cfg.CreateMap<SampleViewModel, SampleDTO>();
    //cfg.CreateMap<UserProfileViewModel, UserProfileDTO>();
    //cfg.CreateMap<UserProfile, UserProfileDTO>();
    //cfg.CreateMap<UserProfileDTO, UserProfile>();
    //cfg.CreateMap<DirectionDTO, Direction>();
    //cfg.CreateMap<Direction, DirectionDTO>();
    //cfg.CreateMap<DirectionDTO, DirectionViewModel>();
    //cfg.CreateMap<DirectionViewModel, DirectionDTO>();
    //cfg.CreateMap<UserProfileDTO, UserProfileViewModel>();

    //cfg.CreateMap<vUserProfileDTO, vUserProfileViewModel>();
    //cfg.CreateMap<vUserProfileViewModel, vUserProfileDTO>();

    //cfg.CreateMap<vUserProgress, vUserProgressDTO>();
    //cfg.CreateMap<vUserProgressDTO, vUserProgress>();
    //cfg.CreateMap<vUserProgressDTO, vUserProgressViewModel>();
}