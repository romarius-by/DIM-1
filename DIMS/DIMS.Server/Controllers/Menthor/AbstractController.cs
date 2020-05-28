using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Interfaces;
using DIMS.Server.Models.Tasks;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DIMS.Server.Controllers.Menthor
{
    public abstract class AbstractController : Controller
    {
        protected readonly ITaskService _taskService;
        protected readonly IvTaskService _vTaskService;
        protected readonly IUserTaskService _userTaskService;
        protected readonly IvUserProfileService _vUserProfileService;
        protected readonly IvTaskStateService _vTaskStateService;
        protected readonly IvUserTaskService _vUserTaskService;
        protected readonly IMapper _mapper;

        public AbstractController(ITaskService taskService, IvTaskService vTaskService, IUserTaskService userTaskService, IvUserProfileService vUserProfileService, IvTaskStateService vTaskStateService, IvUserTaskService vUserTaskService, IMapper mapper)
        {
            _taskService = taskService;
            _vTaskService = vTaskService;
            _userTaskService = userTaskService;
            _vUserProfileService = vUserProfileService;
            _vTaskStateService = vTaskStateService;
            _vUserTaskService = vUserTaskService;
            _mapper = mapper;
        }

        public List<string> GetUsersForTask(int id)
        {
            var userTaskDtos = _userTaskService.GetAllUserTasksByTaskId(id);
            var userTasks = _mapper.Map<IEnumerable<UserTaskDTO>, List<UserTaskViewModel>>(userTaskDtos);

            var usersTaskList = new List<string>();

            foreach (var user in userTasks)
            {
                usersTaskList.Add(user.UserId.ToString());
            }

            return usersTaskList;
        }
    }
}