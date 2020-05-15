using Email.Interfaces;
using HIMS.BL.Interfaces;
using HIMS.BL.Models;
using HIMS.BL.Services;
using HIMS.Email.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace HIMS.Server.utils
{
    public class DependencesModule : NinjectModule
    {
        public override void Load()
        {
            
            Bind<ISampleService>().To<SampleService>();
            Bind<IUserService>().To<UserService>();
            Bind<IUserProfileService>().To<UserProfileService>();
            Bind<IUserTaskService>().To<UserTaskService>();
            Bind<ITaskService>().To<TaskService>();
            Bind<IDirectionService>().To<DirectionService>();
            Bind<ITaskStateService>().To<TaskStateService>();
            Bind<ITaskTrackService>().To<TaskTrackService>();

            Bind<IvTaskService>().To<vTaskService>();
            Bind<IvUserProfileService>().To<vUserProfileService>();
            Bind<IvUserProgressService>().To<vUserProgressService>();
            Bind<IvUserTaskService>().To<vUserTaskService>();
            Bind<IvUserTrackService>().To<vUserTrackService>();

            Bind<ISender>().To<Sender>()
                .InSingletonScope()
            .WithConstructorArgument("apiKey", ConfigurationManager.AppSettings["apiKey"])
            .WithConstructorArgument("email", ConfigurationManager.AppSettings["email"]);
        }
    }
}