using AutoMapper;
using HIMS.BL.DTO;
using HIMS.BL.Interfaces;
using HIMS.Server.Models;
using HIMS.Server.Models.Directions;
using HIMS.Server.Models.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HIMS.Server.Controllers
{
    [Authorize]
    [RoutePrefix("users")]
    public class UserProfileController : Controller
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IvUserProfileService _vUserProfileService;
        private readonly IDirectionService _directionService;
        private readonly IvUserProgressService _vUserProgressService;

        private UserProfilePageViewModel UserProfilesPageViewModel;
        public UserProfileController(IUserProfileService userProfileService, 
            IvUserProfileService vuserProfileService, 
            IDirectionService directionService, 
            IvUserProgressService vUserProgressService)
        {
            _userProfileService = userProfileService;
            _vUserProfileService = vuserProfileService;
            _directionService = directionService;
            _vUserProgressService = vUserProgressService;

            UserProfilesPageViewModel = new UserProfilePageViewModel();
           
        }


        [HttpGet]
        [Route("profiles")]
        public ActionResult Index()
        {
            var userProfileDtos = _vUserProfileService.GetAll();

            var userProfileListViewModel = Mapper.Map<IEnumerable<vUserProfileDTO>, IEnumerable<vUserProfileViewModel>>(userProfileDtos);

            return View(userProfileListViewModel);
        }

        [HttpGet]
        [Route("create")]
        public ActionResult Create()
        {
            ViewBag.DirectionId = GetDirections();
            return PartialView();
        }

        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, LastName, Email, MobilePhone, DirectionId, Education, UniversityAverageScore, MathScore, BirthDate, Address, Skype, StartDate, Sex")]UserProfileViewModel userProfileViewModel)
        {
            try
            {
                var UserProfileDTO = Mapper.Map<UserProfileViewModel, UserProfileDTO>(UserProfilesPageViewModel.UserProfileViewModel);
                if (ModelState.IsValid)
                {
                    var userProfileDTO = Mapper.Map<UserProfileViewModel, UserProfileDTO>(userProfileViewModel);
                    _userProfileService.Save(userProfileDTO);

                    UserProfilesPageViewModel.UserProfileViewModel = userProfileViewModel;
                    return RedirectToAction("Index");
                }
            }

            
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }


            return View(UserProfilesPageViewModel);
        }

        [HttpGet]
        [Route("edit/{id?}")]
        public ActionResult Edit(UserProfileViewModel userProfileViewModel, int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var userProfileDto = _userProfileService.GetById(id.Value);

            if (userProfileDto == null)
                return HttpNotFound();

            var UserProfileViewModel =
               Mapper.Map<UserProfileDTO, UserProfileViewModel>(userProfileDto);
            ViewBag.DirectionId = GetDirections();
            
            return PartialView(UserProfileViewModel);
        }


        [HttpPost, ActionName("Edit")]
        [Route("edit/{id?}")]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(UserProfileViewModel userProfileViewModel, int? id)
        {
            if (userProfileViewModel == null || !id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var userProfileDto = _userProfileService.GetById(id.Value);

            try
            {
                Mapper.Map(userProfileViewModel, userProfileDto);
                userProfileDto.UserId = id.Value;
                _userProfileService.Update(userProfileDto);
                return RedirectToAction("Index");
            }                

            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            

            var UserProfileViewModel = Mapper.Map<UserProfileDTO, UserProfileViewModel>(userProfileDto);

            return PartialView(UserProfileViewModel);
        }

        [HttpGet]
        [Route("profile/{id?}")]
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var vuserProfileDto = _vUserProfileService.GetById(id.Value);

            if (vuserProfileDto == null)
                return HttpNotFound();

            var vuserProfile = Mapper.Map<vUserProfileDTO, vUserProfileViewModel>(vuserProfileDto);

            return PartialView(vuserProfile);
        }

        [HttpGet]
        [Route("profile/progress/{id?}")]
        public ActionResult Progress(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var userProgressDto = _vUserProgressService.GetByUserId(id.Value);

            var user = _vUserProfileService.GetById(id.Value);

            var vUserProgressesListViewModel = new vUserProgressesListViewModel(
                Mapper.Map<vUserProfileDTO, vUserProfileViewModel>(user),
                Mapper.Map<IEnumerable<vUserProgressDTO>, IEnumerable<vUserProgressViewModel>>(userProgressDto)
                );

            return PartialView(vUserProgressesListViewModel);
        }


        public ActionResult DeleteById(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);


            var vuserProfileDto = _vUserProfileService.GetById(id.Value);

            if (vuserProfileDto == null)
                return HttpNotFound();


            var vuserProfile = Mapper.Map<vUserProfileDTO, vUserProfileViewModel>(vuserProfileDto);

            return View(vuserProfile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteById(int id)
        {
            try
            {
                _userProfileService.DeleteById(id);
            }
            catch (RetryLimitExceededException)
            {
                return RedirectToAction("DeleteById", new { id, saveChangesError = true });
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("delete/{email?}")]
        public async Task<ActionResult> DeleteByEmail(string email)
        {
            if (email == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var userProfile = await _vUserProfileService.GetByEmailAsync(email);

            if (userProfile == null)
            {
                return HttpNotFound();
            } 

            var vuserProfileDto = Mapper.Map<vUserProfileDTO, vUserProfileViewModel>(userProfile);

            return PartialView(vuserProfileDto);
        }

        [HttpDelete]
        [Route("delete/{email?}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteByEmail(string email, int id)
        {
            try
            {
                var operationDetails = await _userProfileService.DeleteByEmailAsync(email);
            }
            catch (RetryLimitExceededException)
            {
                return RedirectToAction("DeleteByEmail", new { email, saveChangesError = true });
            }

            return RedirectToAction("Index");
        }

        [NonAction]
        private List<SelectListItem> GetDirections()
        {
            var directions = Mapper.Map<IEnumerable<DirectionDTO>, List<DirectionViewModel>>(_directionService.GetAll());
            List<SelectListItem> selectedItems = new List<SelectListItem>();

            foreach (var direction in directions)
            {
                selectedItems.Add(new SelectListItem { Text = direction.Name, Value = direction.DirectionId.ToString() });
            }

            return selectedItems;
        }

    }

}