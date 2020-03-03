using AutoMapper;
using HIMS.BL.DTO;
using HIMS.BL.Interfaces;
using HIMS.Server.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

            var UserProgresses = new vUserProgressesListViewModel
            {
                vUserProgresses = Mapper.Map<IEnumerable<vUserProgressDTO>, List<vUserProgressViewModel>>(
                    _userProgressService.GetVUserProgressesByUserId(id.Value)),
                vUserProfile = Mapper.Map<vUserProfileDTO, vUserProfileViewModel>(
                    _userProfileService.GetVUserProfile(id.Value))
        };

            return View(UserProgresses);       
        }

    }
}