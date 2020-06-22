using System.Web.Mvc;

namespace DIMS.Server.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (User.IsInRole("user"))
            {
                return View("UserIndex");
            }
            else if (User.IsInRole("mentor"))
            {
                return View("MentorIndex");
            }
            else if (User.IsInRole("admin"))
            {
                return View("AdminIndex");
            }
            else
            {
                return View();
            }
        }
    }
}