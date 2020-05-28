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
        public ActionResult Index(int userId)
        {
            IEnumerable<TaskTrackDTO> taskTrackDTOs = _taskTrackService.GetTracksForUser(userId);

            var taskTrackListViewModel = new TaskTrackListViewModel
            {
                TaskTrackViewModels = Mapper.Map<IEnumerable<TaskTrackDTO>, IEnumerable<TaskTrackViewModel>>(taskTrackDTOs),
                UserId = userId
            };

            return View(taskTrackListViewModel);
        }
    }
}