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
    public class TasksManageController : AbstractController
    {
        public TasksManageController(ITaskService taskService, IvTaskService vTaskService, IUserTaskService userTaskService, 
            IvUserProfileService vUserProfileService, IvTaskStateService vTaskStateService, IvUserTaskService vUserTaskService) 
            : base (taskService, vTaskService, userTaskService, vUserProfileService, vTaskStateService, vUserTaskService)
        {
        }

        [Authorize(Roles = "admin, mentor")]
        [Route("tasks")]
        public ActionResult Index()
        {
            IEnumerable<vTaskDTO> taskDTOs = _vTaskService.GetAll();

            var taskListViewModel = Mapper.Map<IEnumerable<vTaskDTO>, IEnumerable<vTaskViewModel>>(taskDTOs);

            return View(taskListViewModel);
        }

        public ActionResult Create()
        {
            var userProfileDtos = _vUserProfileService.GetAll();
            var userProfileListViewModel = Mapper.Map<IEnumerable<vUserProfileDTO>, IEnumerable<vUserProfileViewModel>>(userProfileDtos);
            var taskStateDtos = _vTaskStateService.GetAll();
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
        public ActionResult Create(TaskViewModel taskViewModel, UserTaskViewModel userTaskViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var taskDto = Mapper.Map<TaskViewModel, TaskDTO>(taskViewModel);
                    var userProfileDtos = _vUserProfileService.GetAll();
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
                    _taskService.Save(taskDto);
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

        public ActionResult Edit(int id)
        {
            ViewBag.TaskUsers = GetUsersForTask(id);

            var taskDto = _taskService.GetById(id);
            var userProfileDtos = _vUserProfileService.GetAll();
            var taskStateDtos = _vTaskStateService.GetAll();

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaskManagePageViewModel taskManagePageViewModel, int id)
        {
            var taskDto = Mapper.Map<TaskViewModel, vTaskDTO>(taskManagePageViewModel.taskViewModel);

            taskDto.TaskId = id;

            var newUsers = new List<string>();
            var oldUsers = GetUsersForTask(id);

            IEnumerable<string> selectedUsers = Request.Form.GetValues("SelectedUsers");
            int selectedState = Convert.ToInt32(Request.Form.Get("SelectedState"));

            if (selectedUsers != null)
            {
                foreach (var item in selectedUsers)
                {
                    newUsers.Add(item);
                }
            }

            var UsersForDeleteTask = oldUsers.Except(newUsers);
            var UsersForAddTask = newUsers.Except(oldUsers);

            if (TryUpdateModel(taskDto))
            {
                try
                {
                    _vTaskService.Update(taskDto);
                    foreach (var item in UsersForAddTask)
                    {
                        var userTask = new UserTaskViewModel
                        {
                            UserId = Convert.ToInt32(item),
                            TaskId = taskDto.TaskId,
                            StateId = selectedState
                        };

                        var userTaskDto = Mapper.Map<UserTaskViewModel, vUserTaskDTO>(userTask);
                        _vUserTaskService.Save(userTaskDto);
                    }

                    foreach (var userId in UsersForDeleteTask)
                    {
                        _userTaskService.DeleteItemByTaskIdAndUserId(taskDto.TaskId, Convert.ToInt32(userId));
                    }

                    var userTaskDtos = _userTaskService.GetAllUserProfilesByTaskId(taskDto.TaskId);
                    var vUserTaskDtos = Mapper.Map<IEnumerable<UserTaskDTO>, IEnumerable<vUserTaskDTO>>(userTaskDtos);

                    foreach (var userTaskDto in vUserTaskDtos)
                    {
                        if (userTaskDto.StateId != selectedState)
                        {
                            userTaskDto.StateId = selectedState;
                            _vUserTaskService.Update(userTaskDto);
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
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var taskDto = _taskService.GetById(id);
            var userTaskDtos = _taskService.GetUserTasks(id);
            var userProfileDtos = _vUserProfileService.GetAll();
            var taskStateDtos = _vTaskStateService.GetAll();

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                _taskService.DeleteById(id);
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
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var taskDto = _taskService.GetById(id.Value);
            var userProfileDtos = _vUserProfileService.GetAll();
            var userTaskDtos = _userTaskService.GetAllUserProfilesByTaskId(id.Value);

            if (taskDto == null)
            {
                return HttpNotFound();
            }

            TaskManagePageViewModel taskManagePageViewModel = new TaskManagePageViewModel
            {
                taskViewModel = Mapper.Map<TaskDTO, TaskViewModel>(taskDto),
                userTaskListViewModel = Mapper.Map<IEnumerable<UserTaskDTO>, IEnumerable<UserTaskViewModel>>(userTaskDtos),
                userProfileListViewModel = Mapper.Map<IEnumerable<vUserProfileDTO>, IEnumerable<vUserProfileViewModel>>(userProfileDtos)
            };

            return PartialView(taskManagePageViewModel);
        }
    }
}