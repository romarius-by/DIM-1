using HIMS.BL.Interfaces;
using HIMS.BL.Services;
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

    }
}