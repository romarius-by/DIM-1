using AutoMapper;
using HIMS.BL.DTO;
using HIMS.BL.Interfaces;
using HIMS.Server.Models.Tasks;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Mvc;


namespace HIMS.Server.Controllers.Menthor
{
    [Authorize]
    [RoutePrefix("task")]
    public class TaskTrackController : Controller
    {
        private readonly ITaskTrackService _taskTrackService;

        public TaskTrackController(ITaskTrackService taskTrackService)
        {
            _taskTrackService = taskTrackService;
        }

        [HttpGet]
        [Route("track")]
        public ActionResult Index(int? userId)
        {
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IEnumerable<TaskTrackDTO> taskTrackDTOs = _taskTrackService.GetTracksForUser(userId);

            var taskTrackListViewModel = new TaskTrackListViewModel
            {
                TaskTrackViewModels = Mapper.Map<IEnumerable<TaskTrackDTO>, IEnumerable<TaskTrackViewModel>>(taskTrackDTOs),
                UserId = userId.Value
            };

            return View(taskTrackListViewModel);
        }
        /*
        [HttpGet]
        [Route("track/{id?}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskTrackDTO taskTrackDTO = _taskTrackService.GetItem(id);
            if (taskTrackDTO == null)
                return HttpNotFound();

            var taskTrack = Mapper.Map<TaskTrackDTO, TaskTrackViewModel>(taskTrackDTO);
            return View(taskTrack);
        }

        [HttpPost, ActionName("Edit")]
        [Route("track/{id?}")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var taskTrackDTO = _taskTrackService.GetItem(id);

            if (TryUpdateModel(taskTrackDTO))
            {
                try
                {
                    _taskTrackService.UpdateItem(taskTrackDTO);
                    return RedirectToAction("Index", new { userId = taskTrackDTO.UserId });
                }
                catch (RetryLimitExceededException /* dex *//*)
                {
                    ModelState.AddModelError("", "Unable to save changes.Try again, and if the problem  persists, see your system administrator.");
                }
            }
            var taskTrack = Mapper.Map<TaskTrackDTO, TaskTrackViewModel>(taskTrackDTO);
            return View(taskTrack);
        }

        [HttpGet]
        [Route("track/{id?}/{saveChangesError?}")]
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }

            TaskTrackDTO taskTrackDTO = _taskTrackService.GetItem(id);

            if (taskTrackDTO == null)
            {
                return HttpNotFound();
            }

            var taskTrack = Mapper.Map<TaskTrackDTO, TaskTrackViewModel>(taskTrackDTO);

            return View(taskTrack);
        }

        [HttpDelete]
        [Route("track/{id?}")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            var taskTrack = _taskTrackService.GetItem(id);
            try
            {
                _taskTrackService.DeleteItem(id);
            }
            catch (RetryLimitExceededException /* dex *//*)
            {
                return RedirectToAction("Delete", new { id, saveChangesError = true });
            }

            return RedirectToAction("Index", new { userId = id });
        }

        [HttpGet]
        [Route("track/details/{id?}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TaskTrackDTO taskTrackDTO = _taskTrackService.GetItem(id);

            if (taskTrackDTO == null)
            {
                return HttpNotFound();
            }

            var taskTrack = Mapper.Map<TaskTrackDTO, TaskTrackViewModel>(taskTrackDTO);

            return View(taskTrack);
        }

        [HttpGet]
        [Route("track/{taskId?}/{userId?}")]
        public ActionResult Create(int? taskId, int? userId)
        {
            if (taskId == null || userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskTrackDTO taskTrackDTO = _taskTrackService.GetTask(taskId, userId);
            if (taskTrackDTO == null)
                return HttpNotFound();

            var taskTrack = Mapper.Map<TaskTrackDTO, TaskTrackViewModel>(taskTrackDTO);
            return View(taskTrack);
        }

        [HttpPost]
        [Route("track")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaskTrackViewModel taskTrack)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var taskTrackDto = Mapper.Map<TaskTrackViewModel, TaskTrackDTO>(taskTrack);
                    _taskTrackService.SaveItem(taskTrackDto);
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try agagin, and if the problem persists see your system administrator.");
            }
            return View(taskTrack);
        }*/
    }
}