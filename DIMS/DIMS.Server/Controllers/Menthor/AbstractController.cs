using AutoMapper;
using HIMS.BL.DTO;
using HIMS.BL.Interfaces;
using HIMS.EF.DAL.Data;
using HIMS.Server.Models.Tasks;
using HIMS.Server.Models.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HIMS.Server.Controllers.Menthor
{
    public abstract class AbstractController : Controller
    {
        protected readonly ITaskService _taskService;
        protected readonly IvTaskService _vTaskService;
        protected readonly IUserTaskService _userTaskService;
        protected readonly IvUserProfileService _vUserProfileService;
        protected readonly IvTaskStateService _vTaskStateService;
        protected readonly IvUserTaskService _vUserTaskService;

        public AbstractController(ITaskService taskService, IvTaskService vTaskService, IUserTaskService userTaskService, IvUserProfileService vUserProfileService, IvTaskStateService vTaskStateService, IvUserTaskService vUserTaskService)
        {
            _taskService = taskService;
            _vTaskService = vTaskService;
            _userTaskService = userTaskService;
            _vUserProfileService = vUserProfileService;
            _vTaskStateService = vTaskStateService;
            _vUserTaskService = vUserTaskService;
        }

        public List<string> GetUsersForTask(int id)
        {
            var userTaskDtos = _userTaskService.GetAllUserTasksByTaskId(id);
            var userTasks = Mapper.Map<IEnumerable<UserTaskDTO>, List<UserTaskViewModel>>(userTaskDtos);

            var usersTaskList = new List<string>();

            foreach (var user in userTasks)
            {
                usersTaskList.Add(user.UserId.ToString());
            }

            return usersTaskList;
        }
    }
}