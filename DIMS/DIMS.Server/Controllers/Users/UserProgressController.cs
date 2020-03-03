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

        public UserProgressController(IvUserProgressService userProgressService)
        {
            _userProgressService = userProgressService;
        }

        public ActionResult Index(int? id)
        {
            var vUserProgresses = Mapper.Map<IEnumerable<vUserProgressDTO>, List<vUserProgressViewModel>>
                    (_userProgressService.GetVUserProgressesByUserId(id.Value));

            return View(vUserProgresses);
                
        }

    }
}