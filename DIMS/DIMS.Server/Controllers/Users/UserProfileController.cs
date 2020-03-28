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



        public ActionResult Index()
        {
            var userProfileDtos = _vUserProfileService.GetItems();

            var userProfileListViewModel = Mapper.Map<IEnumerable<vUserProfileDTO>, IEnumerable<vUserProfileViewModel>>(userProfileDtos);

            return View(userProfileListViewModel);
        }

        public ActionResult Create()
        {
            ViewBag.DirectionId = GetDirections();
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, LastName, Email, MobilePhone, DirectionId, Education, UniversityAverageScore, MathScore, BirthDate, Address, Skype, StartDate, Sex")]UserProfileViewModel userProfileViewModel)
        {
            try
            {
                var UserProfileDTO = Mapper.Map<UserProfileViewModel, UserProfileDTO>(UserProfilesPageViewModel.UserProfileViewModel);
                if (ModelState.IsValid)
                {
                    var userProfileDTO = Mapper.Map<UserProfileViewModel, UserProfileDTO>(userProfileViewModel);
                    _userProfileService.SaveItem(userProfileDTO);

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

        public ActionResult Edit(UserProfileViewModel userProfileViewModel, int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var userProfileDto = _userProfileService.GetItem(id.Value);

            if (userProfileDto == null)
                return HttpNotFound();

            var UserProfileViewModel =
               Mapper.Map<UserProfileDTO, UserProfileViewModel>(userProfileDto);
            ViewBag.DirectionId = GetDirections();
            
            return PartialView(UserProfileViewModel);
        }


        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(UserProfileViewModel userProfileViewModel, int? id)
        {
            if (userProfileViewModel == null || !id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var userProfileDto = _userProfileService.GetItem(id.Value);

            try
            {
                Mapper.Map(userProfileViewModel, userProfileDto);
                userProfileDto.UserId = id.Value;
                _userProfileService.UpdateItem(userProfileDto);
                return RedirectToAction("Index");
            }                

            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            

            var UserProfileViewModel = Mapper.Map<UserProfileDTO, UserProfileViewModel>(userProfileDto);

            return PartialView(UserProfileViewModel);
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var vuserProfileDto = _vUserProfileService.GetItem(id.Value);

            if (vuserProfileDto == null)
                return HttpNotFound();

            var vuserProfile = Mapper.Map<vUserProfileDTO, vUserProfileViewModel>(vuserProfileDto);

            return PartialView(vuserProfile);
        }

        public ActionResult Progress(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var userProgressDto = _vUserProgressService.GetVUserProgressesByUserId(id.Value);

            var user = _vUserProfileService.GetItem(id.Value);

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


            var vuserProfileDto = _vUserProfileService.GetItem(id.Value);

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
                _userProfileService.DeleteItem(id);
            }
            catch (RetryLimitExceededException)
            {
                return RedirectToAction("DeleteById", new { id, saveChangesError = true });
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteByEmail(string email)
        {
            if (email == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var userProfile = _vUserProfileService.GetVUserProfileByEmail(email);

            var vuserProfileDto = Mapper.Map<vUserProfileDTO, vUserProfileViewModel>(_vUserProfileService.GetVUserProfileByEmail(email));

            if (vuserProfileDto == null)
                return HttpNotFound();

            return PartialView(vuserProfileDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteByEmail(string email, int id)
        {
            try
            {
                var operationDetails = await _userProfileService.DeleteUserProfileByEmail(email);
            }
            catch (RetryLimitExceededException)
            {
                return RedirectToAction("DeleteByEmail", new { email, saveChangesError = true });
            }

            return RedirectToAction("Index");
        }

        private List<SelectListItem> GetDirections()
        {
            var directions = Mapper.Map<IEnumerable<DirectionDTO>, List<DirectionViewModel>>(_directionService.GetItems());
            List<SelectListItem> selectItems = new List<SelectListItem>();

            foreach (var item in directions)
            {
                selectItems.Add(new SelectListItem { Text = item.Name, Value = item.DirectionId.ToString() });
            }

            return selectItems;
        }

    }

}