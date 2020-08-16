using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FirstNetCore.Areas.User.Controllers
{
    [Area("user")]
    public class UserController : Controller
    {
        private SignInManager<IdentityUser> signInManager;

        public UserController(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public IActionResult Usuario()
        {
            return View();
        }

        public IActionResult Index()
        {
            return Content("tetss");
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Redirect("~/Home/Index");
        }

    }
}