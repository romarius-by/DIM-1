using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Interfaces;
using DIMS.Server.Models.Tasks;
using System.Collections.Generic;
using System.Web.Mvc;


namespace DIMS.Server.Controllers.Menthor
{
    [Authorize]
    public class TaskTrackController : Controller
    {
        private readonly ITaskTrackService _taskTrackService;
        private readonly IvTaskTrackService _vTaskTrackService;
        private readonly IvUserTrackService _vUserTrackService;
        private readonly IMapper _mapper;

        public TaskTrackController(ITaskTrackService taskTrackService, IvTaskTrackService vTaskTrackService, IvUserTrackService vUserTrackService, IMapper mapper)
        {
            _taskTrackService = taskTrackService;
            _vTaskTrackService = vTaskTrackService;
            _vUserTrackService = vUserTrackService;
            _mapper = mapper;
        }
        
        [HttpGet]
        [Route("track/{userId}")]
        public ActionResult Index(int userId)
        {
            var vUserTrackDTOs = _vUserTrackService.GetTracksForUser(userId);

            var taskTrackViewModels = Mapper.Map<IEnumerable<vUserTrackDTO>, IEnumerable<TaskTrackViewModel>>(vUserTrackDTOs);

            return View(taskTrackViewModels);
        }

        public ActionResult Edit(int id)
        {
            var taskTrackDto = _vUserTrackService.GetById(id);

            if (taskTrackDto == null)
            {
                return HttpNotFound();
            }

            var taskTrackViewModel = Mapper.Map<vUserTrackDTO, TaskTrackViewModel>(taskTrackDto);

            return View(taskTrackViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaskTrackViewModel taskTrackViewModel, int id)
        {
            var vTaskTrackDto = _vTaskTrackService.GetById(id);
            vTaskTrackDto.TrackDate = taskTrackViewModel.TrackDate;
            vTaskTrackDto.TrackNote = taskTrackViewModel.TrackNote;

            if (TryUpdateModel(vTaskTrackDto))
            {
                try
                {
                    _vTaskTrackService.Update(vTaskTrackDto);
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            return PartialView(taskTrackViewModel);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {

            var vUserTrackDto = _vUserTrackService.GetById(id);

            if (vUserTrackDto == null)
            {
                return HttpNotFound();
            }

            var taskTrackViewModel = Mapper.Map<vUserTrackDTO, TaskTrackViewModel>(vUserTrackDto);

            return View(taskTrackViewModel);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {

            var vUserTrackDto = _vUserTrackService.GetById(id);

            if (vUserTrackDto == null)
            {
                return HttpNotFound();
            }

            var taskTrackViewModel = Mapper.Map<vUserTrackDTO, TaskTrackViewModel>(vUserTrackDto);

            return View(taskTrackViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            var vUserTrackDto = _vUserTrackService.GetById(id);

            try
            {
                _taskTrackService.DeleteById(id);
            }
            catch (RetryLimitExceededException)
            {
                return RedirectToAction("Delete", new { id, saveChangesError = true });
            }

            return RedirectToAction("Index", new { userId = vUserTrackDto.UserId });
        }
    }
}