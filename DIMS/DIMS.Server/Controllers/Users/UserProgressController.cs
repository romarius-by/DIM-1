using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Interfaces;
using DIMS.Server.Models.Tasks;
using DIMS.Server.Models.Users;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DIMS.Server.Controllers.Users
{
    public class UserProgressController : Controller
    {
        private readonly IVUserProgressService _userProgressService;
        private readonly IVUserProfileService _userProfileService;
        private readonly IVTaskService _taskService;
        private readonly IMapper _mapper;

        public UserProgressController(IVUserProgressService userProgressService, 
            IVUserProfileService userProfileService, 
            IMapper mapper,
            IVTaskService taskService)
        {
            _userProgressService = userProgressService;
            _userProfileService = userProfileService;
            _taskService = taskService;
            _mapper = mapper;
        }

        public ActionResult Index(int userId)
        {
            var userProfileDTO = _userProfileService.GetById(userId);

            var userProfileViewModel = _mapper.Map<VUserProfileDTO, VUserProfileViewModel>(userProfileDTO);

            var userProgressDTO = _userProgressService.GetByUserId(userId);

            var userProgressViewModel = _mapper.Map<IEnumerable<VUserProgressDTO>, IEnumerable<VUserProgressViewModel>>(userProgressDTO);

            var userProgresses = new VUserProgressesListViewModel(userProfileViewModel, userProgressViewModel);

            return View(userProgresses);
        }

        public ActionResult Details(int trackId)
        {
            var userProgressDTO = _userProgressService.GetById(trackId);

            var userProgressViewModel = _mapper.Map<VUserProgressDTO, VUserProgressViewModel>(userProgressDTO);

            var taskDTO = _taskService.GetById(userProgressDTO.TaskId);

            var taskViewModel = _mapper.Map<VTaskDTO, vTaskViewModel>(taskDTO);

            var userProgressDetails = new VUserProgressDetailsViewModel(userProgressViewModel, taskViewModel);

            return View(userProgressDetails);
        }
    }
}