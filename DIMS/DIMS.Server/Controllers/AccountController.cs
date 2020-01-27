using HIMS.BL.Infrastructure;
using HIMS.BL.Interfaces;
using HIMS.BL.Models;
using HIMS.Server.Models;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HIMS.Server.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel viewModel)
        {
            await SetInitialDataAsync().ConfigureAwait(false);
            if (ModelState.IsValid)
            {
                var userDto = new UserDTO { Email = viewModel.Email, Password = viewModel.Password };
                ClaimsIdentity claim = await _userService.Authenticate(userDto).ConfigureAwait(false);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Invalid login or password.");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(viewModel);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Sample");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Register(RegisterViewModel viewModel)
        {
            await SetInitialDataAsync().ConfigureAwait(false);
            if (ModelState.IsValid)
            {
                var userDto = new UserDTO
                {
                    Email = viewModel.Email,
                    Password = viewModel.Password,
                    Address = viewModel.Address,
                    Name = viewModel.Name,
                    Role = "user"
                };
                OperationDetails operationDetails = await _userService.Create(userDto).ConfigureAwait(false);
                if (operationDetails.Succedeed)
                    return View("SuccessRegister");
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(viewModel);
        }

        private async Task SetInitialDataAsync()
        {
            await _userService.SetInitialData(new UserDTO
            {
                Email = "alex.meleschenko@gmail.com",
                UserName = "Alex",
                Password = "superadmin",
                Name = "Alex",
                Address = "Revoljucionnaja 11-301",
                Role = "admin",
            }, new List<string> { "user", "admin" }).ConfigureAwait(false);
        }
    }
}