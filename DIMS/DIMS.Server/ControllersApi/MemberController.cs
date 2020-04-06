using AutoMapper;
using HIMS.BL.DTO;
using HIMS.BL.Interfaces;
using HIMS.BL.Services;
using HIMS.Server.Models;
using HIMS.Server.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HIMS.Server.ControllersApi
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api")]
    public class MemberController : ApiController
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IvUserProfileService _vUserProfileService;
        private readonly IDirectionService _directionService;

        public MemberController(IUserProfileService userProfileService, IvUserProfileService vUserProfileService, IDirectionService directionService)
        {
            _userProfileService = userProfileService ?? throw new ArgumentNullException(nameof(userProfileService));
            _vUserProfileService = vUserProfileService ?? throw new ArgumentNullException(nameof(vUserProfileService));
            _directionService = directionService ?? throw new ArgumentNullException(nameof(directionService));
        }


        private KeyValuePair<string, IEnumerable<string>>[] GetErrors()
        {
            return ModelState
                .ToDictionary(k => k.Key, kv => kv.Value.Errors.Select(e => e.ErrorMessage).Distinct())
                .ToArray();
        }

        [HttpGet]
        [Route("profile/{id?}")]
        public IHttpActionResult GetDetails([FromUri] int? id)
        {

            if (!id.HasValue)
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The id value was not set"));

            var vUserProfileDto = _vUserProfileService.GetItem(id.Value);

            if (vUserProfileDto == null)
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, $"The user with id = {id.Value} was not found!"));

            var userProfile = Mapper.Map<vUserProfileDTO, vUserProfileViewModel>(vUserProfileDto);

            return Json(userProfile);

        }

    }
}