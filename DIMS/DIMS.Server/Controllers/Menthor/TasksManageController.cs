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
    public class TasksManageController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IUserTaskService _userTaskService;
        private readonly IvUserProfileService _vUserProfileService;
        private readonly ITaskStateService _taskStateService;

        public TasksManageController(ITaskService taskService, IUserTaskService userTaskService, IvUserProfileService vUserProfileService, ITaskStateService taskStateService)
        {
            _taskService = taskService;
            _userTaskService = userTaskService;
            _vUserProfileService = vUserProfileService;
            _taskStateService = taskStateService;
        }

        //[Authorize(Roles = "admin, mentor")]
        //[Route("tasks")]
        public ActionResult Index()
        {
            IEnumerable<TaskDTO> taskDTOs = _taskService.GetItems();

            var taskListViewModel = Mapper.Map<IEnumerable<TaskDTO>, IEnumerable<vTaskViewModel>>(taskDTOs);

            return View(taskListViewModel);
        }

        public ActionResult Create()
        {
            var userProfileDtos = _vUserProfileService.GetItems();
            var userProfileListViewModel = Mapper.Map<IEnumerable<vUserProfileDTO>, IEnumerable<vUserProfileViewModel>>(userProfileDtos);
            var taskStateDtos = _taskStateService.GetItems();
            var taskStateListViewModel = Mapper.Map<IEnumerable<TaskStateDTO>, IEnumerable<TaskStateViewModel>>(taskStateDtos);
            TaskManagePageViewModel taskManagePageViewModel = new TaskManagePageViewModel
            {
                userProfileListViewModel = userProfileListViewModel,
                taskStateListViewModel = taskStateListViewModel
            };
            return View(taskManagePageViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, Description, StartDate, DeadlineDate")]TaskViewModel taskViewModel, UserTaskViewModel userTaskViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var taskDto = Mapper.Map<TaskViewModel, TaskDTO>(taskViewModel);
                    var userProfileDtos = _vUserProfileService.GetItems();
                    var userProfileListViewModel = Mapper.Map<IEnumerable<vUserProfileDTO>, IEnumerable<vUserProfileViewModel>>(userProfileDtos);
                    IEnumerable<string> selectedUsers = Request.Form.GetValues("SelectedUserProfiles");
                    int selectedState = Convert.ToInt32(Request.Form.Get("SelectedState"));
                    if (selectedUsers != null)
                        foreach (var item in selectedUsers)
                        {
                            var userTaskDto = Mapper.Map<UserTaskViewModel, UserTaskDTO>(userTaskViewModel);
                            int userId = Convert.ToInt32(item);
                            userTaskDto.UserId = userId;
                            userTaskDto.StateId = selectedState;
                            userTaskDto.TaskId = taskDto.TaskId;
                            taskDto.UserTasks.Add(userTaskDto);
                        }
                    _taskService.SaveItem(taskDto);
                    /*TaskManagePageViewModel taskManagePageViewModel = new TaskManagePageViewModel
                    {
                        userProfileListViewModel = userProfileListViewModel,
                        taskViewModel = taskViewModel,
                        userTaskViewModel = userTaskViewModel,
                    };*/

                    /*userTaskDto.Task = taskDto;
                    userTaskDto.UserProfile = new UserProfileDTO();
                    userTaskDto.TaskState = new TaskStateDTO();
                    userTaskDto.TaskTracks = new List<TaskTrackDTO>();*//*
                    userTaskDto.TaskId = taskDto.TaskId;
                    userTaskDto.UserId = 2006;
                    userTaskDto.StateId = 1;
                    taskDto.UserTasks.Add(userTaskDto);*/
                    /*var userTasksDto = _taskService.GetUserTasks(taskDto.TaskId);
                    Mapper.Map<UserTaskViewModel, UserTaskDTO>(userTasksDto);*/
                    //_userTaskService.SaveItem(userTaskDto);
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            TaskManagePageViewModel taskManagePageViewModel = new TaskManagePageViewModel();
            return View(taskManagePageViewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var taskDto = _taskService.GetItem(id);
            var userTaskDtos = _taskService.GetUserTasks(id); ;
            var userProfileDtos = _vUserProfileService.GetItems();
            var taskStateDtos = _taskStateService.GetItems();

            if (taskDto == null)
            {
                return HttpNotFound();
            }

            TaskManagePageViewModel taskManagePageViewModel = new TaskManagePageViewModel
            {
                userProfileListViewModel = Mapper.Map<IEnumerable<vUserProfileDTO>, IEnumerable<vUserProfileViewModel>>(userProfileDtos),
                taskViewModel = Mapper.Map<TaskDTO, TaskViewModel>(taskDto),
                userTaskListViewModel = Mapper.Map<IEnumerable<UserTaskDTO>, IEnumerable<UserTaskViewModel>>(userTaskDtos),
                taskStateListViewModel = Mapper.Map<IEnumerable<TaskStateDTO>, IEnumerable<TaskStateViewModel>>(taskStateDtos)
            };

            return View(taskManagePageViewModel);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaskManagePageViewModel taskManagePageViewModel, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var taskDto = _taskService.GetItem(id);
            var userTaskDtos = _userTaskService.GetItems();

            try
            {
                /*Mapper.Map(TaskManagePageViewModel.taskViewModel, taskDto);
                
                taskDto.TaskId = id.Value;
                Mapper.Map(TaskManagePageViewModel.userTaskListViewModel, userTaskDtos);
                foreach (var userTask in userTaskDtos)
                {
                    userTask.TaskId = id.Value;
                    _userTaskService.UpdateItem(userTask);
                }
                _taskService.UpdateItem(taskDto);*/
                //Mapper.Map(taskViewModel, taskDto);
                Mapper.Map(taskManagePageViewModel.userTaskListViewModel, userTaskDtos);
                foreach (var userTask in userTaskDtos)
                {
                    if (userTask.TaskId == taskDto.TaskId)
                    {
                        userTask.Task = null;
                        userTask.TaskState = null;
                        userTask.TaskTracks = null;
                        userTask.UserProfile = null;
                        _userTaskService.UpdateItem(userTask);
                    }
                }
                Mapper.Map(taskManagePageViewModel.taskViewModel, taskDto);
                taskDto.TaskId = id.Value;
                _taskService.UpdateItem(taskDto);
               /* foreach (var userTask in userTaskDtos)
                {
                    _userTaskService.UpdateItem(userTask);
                }*/
                return RedirectToAction("Index");
            }

            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return PartialView(taskManagePageViewModel);
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var taskDto = _taskService.GetItem(id);
            var userTaskDtos = _taskService.GetUserTasks(id); ;
            var userProfileDtos = _vUserProfileService.GetItems();
            var taskStateDtos = _taskStateService.GetItems();

            if (taskDto == null)
                return HttpNotFound();

            TaskManagePageViewModel taskManagePageViewModel = new TaskManagePageViewModel
            {
                userProfileListViewModel = Mapper.Map<IEnumerable<vUserProfileDTO>, IEnumerable<vUserProfileViewModel>>(userProfileDtos),
                taskViewModel = Mapper.Map<TaskDTO, TaskViewModel>(taskDto),
                userTaskListViewModel = Mapper.Map<IEnumerable<UserTaskDTO>, IEnumerable<UserTaskViewModel>>(userTaskDtos),
                taskStateListViewModel = Mapper.Map<IEnumerable<TaskStateDTO>, IEnumerable<TaskStateViewModel>>(taskStateDtos)
            };

            return View(taskManagePageViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                _taskService.DeleteItem(id);
            }
            catch (RetryLimitExceededException)
            {
                return RedirectToAction("Delete", new { id, saveChangesError = true });
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var taskDto = _taskService.GetItem(id.Value);

            if (taskDto == null)
                return HttpNotFound();

            var task = Mapper.Map<TaskDTO, vTaskViewModel>(taskDto);

            return PartialView(task);
        }

        /*[Route("edit/{id?}")]
        public ActionResult Edit(vTaskViewModel taskViewModel, int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var taskDto = _taskService.GetItem(id.Value);

            if (taskDto == null)
                return HttpNotFound();

            var TaskViewModel = Mapper.Map<TaskDTO, vTaskViewModel>(taskDto);

            return PartialView(TaskViewModel);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(product);
            }
        }

        public ViewResult Create() => View("Edit", new Product());

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            Product deletedProduct = repository.DeleteProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = $"{deletedProduct.Name} was deleted";
            }
            return RedirectToAction("Index");
        }*/

    }
}