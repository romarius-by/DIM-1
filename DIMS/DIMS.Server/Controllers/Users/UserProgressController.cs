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

            var UserProgresses = new vUserProgressesListViewModel(
                Mapper.Map<vUserProfileDTO, vUserProfileViewModel>(
                    _userProfileService.GetItem(id.Value)),
                Mapper.Map<IEnumerable<vUserProgressDTO>, IEnumerable<vUserProgressViewModel>>(
                    _userProgressService.GetVUserProgressesByUserId(id.Value)));

            return View(UserProgresses);       
        }

    }
}