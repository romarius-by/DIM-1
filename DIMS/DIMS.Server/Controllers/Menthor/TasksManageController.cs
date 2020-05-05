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
        private readonly IvTaskService _vTaskService;
        private readonly IUserTaskService _userTaskService;
        private readonly IvUserProfileService _vUserProfileService;
        private readonly IvTaskStateService _vTaskStateService;

        public TasksManageController(ITaskService taskService, IvTaskService vTaskService, IUserTaskService userTaskService, IvUserProfileService vUserProfileService, IvTaskStateService vTaskStateService)
        {
            _taskService = taskService;
            _vTaskService = vTaskService;
            _userTaskService = userTaskService;
            _vUserProfileService = vUserProfileService;
            _vTaskStateService = vTaskStateService;
        }

        [Authorize(Roles = "admin, mentor")]
        [Route("tasks")]
        public ActionResult Index()
        {
            IEnumerable<vTaskDTO> taskDTOs = _vTaskService.GetItems();

            var taskListViewModel = Mapper.Map<IEnumerable<vTaskDTO>, IEnumerable<vTaskViewModel>>(taskDTOs);

            return View(taskListViewModel);
        }

        public ActionResult Create()
        {
            var userProfileDtos = _vUserProfileService.GetItems();
            var userProfileListViewModel = Mapper.Map<IEnumerable<vUserProfileDTO>, IEnumerable<vUserProfileViewModel>>(userProfileDtos);
            var taskStateDtos = _vTaskStateService.GetItems();
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
                        foreach (var userId in selectedUsers)
                        {
                            var userTaskDto = Mapper.Map<UserTaskViewModel, UserTaskDTO>(userTaskViewModel);

                            userTaskDto.UserId = Convert.ToInt32(userId);
                            userTaskDto.StateId = selectedState;
                            userTaskDto.TaskId = taskDto.TaskId;

                            taskDto.UserTasks.Add(userTaskDto);
                        }
                    _taskService.SaveItem(taskDto);
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

            ViewBag.TaskUsers = GetUsersForTask(id);

            var taskDto = _taskService.GetItem(id);
            var userProfileDtos = _vUserProfileService.GetItems();
            var taskStateDtos = _vTaskStateService.GetItems();

            if (taskDto == null)
            {
                return HttpNotFound();
            }

            TaskManagePageViewModel taskManagePageViewModel = new TaskManagePageViewModel
            {
                userProfileListViewModel = Mapper.Map<IEnumerable<vUserProfileDTO>, IEnumerable<vUserProfileViewModel>>(userProfileDtos),
                taskViewModel = Mapper.Map<TaskDTO, TaskViewModel>(taskDto),
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

            var taskDto = Mapper.Map<TaskViewModel, TaskDTO>(taskManagePageViewModel.taskViewModel);
            taskDto.TaskId = id.Value;

            var newUsers = new List<string>();
            var oldUsers = GetUsersForTask(id);

            IEnumerable<string> selectedUsers = Request.Form.GetValues("SelectedUsers");
            int selectedState = Convert.ToInt32(Request.Form.Get("SelectedState"));
            if (selectedUsers != null)
                foreach (var item in selectedUsers)
                {
                    newUsers.Add(item);
                }

            var UsersForDeleteTask = oldUsers.Except(newUsers);
            var UsersForAddTask = newUsers.Except(oldUsers);

            if (TryUpdateModel(taskDto))
            {
                try
                {
                    _taskService.UpdateItem(taskDto);
                    foreach (var item in UsersForAddTask)
                    {
                        var userTask = new UserTaskViewModel
                        {
                            UserId = Convert.ToInt32(item),
                            TaskId = taskDto.TaskId,
                            StateId = selectedState
                        };

                        var userTaskDto = Mapper.Map<UserTaskViewModel, UserTaskDTO>(userTask);
                        _userTaskService.SaveItem(userTaskDto);
                    }

                    foreach (var userId in UsersForDeleteTask)
                    {
                        _userTaskService.DeleteItemByTaskIdAndUserId(taskDto.TaskId, Convert.ToInt32(userId));
                    }

                    var userTaskDtos = _userTaskService.GetAllUserProfilesByTaskId(taskDto.TaskId);
                    foreach (var userTask in userTaskDtos)
                    {
                        if (userTask.StateId != selectedState)
                        {
                            userTask.StateId = selectedState;
                            _userTaskService.UpdateItem(userTask);
                        }
                    }

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            
            return PartialView(taskManagePageViewModel.taskViewModel);
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var taskDto = _taskService.GetItem(id);
            var userTaskDtos = _taskService.GetUserTasks(id);
            var userProfileDtos = _vUserProfileService.GetItems();
            var taskStateDtos = _vTaskStateService.GetItems();

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
            var userProfileDtos = _vUserProfileService.GetItems();
            var userTaskDtos = _userTaskService.GetAllUserProfilesByTaskId(id.Value);

            if (taskDto == null)
                return HttpNotFound();

            TaskManagePageViewModel taskManagePageViewModel = new TaskManagePageViewModel
            {
                taskViewModel = Mapper.Map<TaskDTO, TaskViewModel>(taskDto),
                userTaskListViewModel = Mapper.Map<IEnumerable<UserTaskDTO>, IEnumerable<UserTaskViewModel>>(userTaskDtos),
                userProfileListViewModel = Mapper.Map<IEnumerable<vUserProfileDTO>, IEnumerable<vUserProfileViewModel>>(userProfileDtos)
            };

            return PartialView(taskManagePageViewModel);
        }

        public List<string> GetUsersForTask(int? id)
        {
            var users = Mapper.Map<IEnumerable<UserTaskDTO>, List<UserTaskViewModel>>(_userTaskService.GetAllUserProfilesByTaskId(id));

            List<string> usersTaskList = new List<string>();
            foreach (var item in users)
            {
                usersTaskList.Add(item.UserId.ToString());
            }
            return usersTaskList;
        }
    }
}