using AutoMapper;
using HIMS.BL.DTO;
using HIMS.BL.Interfaces;
using HIMS.Server.Models;
using HIMS.Server.Models.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HIMS.Server.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IvUserProfileService _vUserProfileService;
        public UserProfileController(IUserProfileService userProfileService, IvUserProfileService vuserProfileService)
        {
            _userProfileService = userProfileService;
            _vUserProfileService = vuserProfileService;
        }

        

        public ActionResult Index()
        {
            IEnumerable<UserProfileDTO> userProfileDTOs = _userProfileService.GetUserProfiles();
            IEnumerable<vUserProfileDTO> vUserProfileDTOs = _vUserProfileService.GetVUserProfiles();
            var userProfiles = new UserProfilesListViewModel
            {
                UserProfiles = Mapper.Map<IEnumerable<UserProfileDTO>, List<UserProfileViewModel>>(userProfileDTOs)
            };

            var vuserProfiles = new vUserProfilesListViewModel
            {
                vUserProfiles = Mapper.Map<IEnumerable<vUserProfileDTO>, List<vUserProfileViewModel>>(vUserProfileDTOs)
            };

            return View(vuserProfiles);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, LastName, Email, MobilePhone")]UserProfileViewModel userProfileViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userProfileDTO = Mapper.Map<UserProfileViewModel, UserProfileDTO>(userProfileViewModel);
                    _userProfileService.SaveUserProfile(userProfileDTO);
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return PartialView(userProfileViewModel);
        }

        public ActionResult DeleteById(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);


            var vuserProfileDto = _vUserProfileService.GetVUserProfile(id.Value);

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
                _userProfileService.DeleteUserProfileById(id);
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


            var vuserProfileDto = Mapper.Map<vUserProfileDTO, vUserProfileViewModel>(_vUserProfileService.GetVUserProfileByEmail(email));

            if (vuserProfileDto == null)
                return HttpNotFound();

            return PartialView(vuserProfileDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteByEmail(string email, int id)
        {
            try
            {
                _userProfileService.DeleteUserProfileByEmail(email);
            }
            catch (RetryLimitExceededException)
            {
                return RedirectToAction("DeleteByEmail", new { email, saveChangesError = true });
            }

            return RedirectToAction("Index");
        }
    }
}