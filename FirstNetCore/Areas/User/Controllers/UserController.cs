using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FirstNetCore.Areas.User.Controllers
{
    [Area("user")]
    public class UserController : Controller
    {
        public IActionResult Usuario()
        {
            return View();
        }

        public IActionResult Index()
        {
            return Content("tetss");
        }

    }
}