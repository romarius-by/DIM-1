using AutoMapper;
using HIMS.BL.DTO;
using HIMS.BL.Interfaces;
using HIMS.Server.Models.Users;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HIMS.Server.Controllers.Users
{
    public class UserProgressController : Controller
    {
        private readonly IvUserProgressService _userProgressService;
        private readonly IvUserProfileService _userProfileService;

        public UserProgressController(IvUserProgressService userProgressService, IvUserProfileService userProfileService)
        {
            _userProgressService = userProgressService;
            _userProfileService = userProfileService;
        }

        public ActionResult Index(int? id)
        {
            if (!id.HasValue)
            {
                // TODO: create my own badRequest method
                return HttpNotFound();
            }

            var _userProfile = _userProfileService.GetById(id.Value);

            var userProfile = Mapper.Map<vUserProfileDTO, vUserProfileViewModel>(_userProfile);

            var _userProgress = _userProgressService.GetByUserId(id.Value);

            var userProgress = Mapper.Map<IEnumerable<vUserProgressDTO>, IEnumerable<vUserProgressViewModel>>(_userProgress);

            var userProgresses = new vUserProgressesListViewModel(userProfile, userProgress);

            return View(userProgresses);
        }
    }
}