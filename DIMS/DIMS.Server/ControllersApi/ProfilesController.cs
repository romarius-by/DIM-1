using AutoMapper;
using HIMS.BL.DTO;
using HIMS.BL.Interfaces;
using HIMS.Server.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HIMS.Server.ControllersApi
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api")]
    public class ProfilesController : ApiController
    {
        private readonly IvUserProfileService _vUserProfileService;

        public ProfilesController(IvUserProfileService vUserProfileService)
        {
            _vUserProfileService = vUserProfileService ?? throw new ArgumentNullException(nameof(vUserProfileService));
        }

        [HttpGet]
        [Route("profiles")]
        public IHttpActionResult GetProfiles()
        {
            var vUserProfileDtos = _vUserProfileService.GetItems();
            var vUserProfiles = Mapper.Map<IEnumerable<vUserProfileDTO>, List<vUserProfileViewModel>>(vUserProfileDtos);
            
            return Json(vUserProfiles);
        }

    }
}