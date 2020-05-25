using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Interfaces;
using DIMS.Server.Models.Tasks;
using DIMS.Server.Models.Users;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DIMS.Server.Controllers.Tasks
{
    public class TasksController : Controller
    {
        private readonly IvTaskService _taskService;
        private readonly IvUserProfileService _userProfileService;

        public TasksController(IvTaskService taskService, IvUserProfileService userProfileService)
        {
            _taskService = taskService;
            _userProfileService = userProfileService;
        }

        public ActionResult Index()
        {
            var TasksViewModel = new TasksListViewModel
            {
                Tasks = Mapper.Map<IEnumerable<vTaskDTO>, IEnumerable<vTaskViewModel>>(
                    _taskService.GetAll()),

                UserProfiles = Mapper.Map<IEnumerable<vUserProfileDTO>, IEnumerable<vUserProfileViewModel>>(
                    _userProfileService.GetAll())
            };

            return View(TasksViewModel);
        }
    }
}