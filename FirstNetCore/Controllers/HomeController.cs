using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FirstNetCore.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System.Net.NetworkInformation;
using FirstNetCore.Areas.User.Models;
using FirstNetCore.Areas.Principal.Controllers;

namespace FirstNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private SignInManager<IdentityUser> _signInManager;
        //private IServiceProvider serviceProvider;
        private static LoginModels _model = null;
        private LUsuario LUsuario;
        public HomeController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<HomeController> logger)
        {
            _logger = logger;
            _signInManager = signInManager;
            this.LUsuario = new LUsuario(userManager, signInManager, roleManager);
    
        }

        public async Task<IActionResult> Index()
        {
            //await CreateRolesAsync(this.serviceProvider);
            //Verifica si ya inició sesión
            if (_signInManager.IsSignedIn(User))
            {
                return Redirect("/Principal/Principal/Principal");
            }


            if (_model == null)
            {
                return View();
            } else
            {
                return View(_model);
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginModels model)
        {
            //await CreateRolesAsync(this.serviceProvider);
            if (ModelState.IsValid)
            {
                var result = await LUsuario.userLoginAsync(model);
                if (result.Succeeded)
                {
                    return Redirect("/Principal/Principal/Principal");
                } else if (result.IsLockedOut) 
                {
                    model.Error = "Cuenta de usuario bloqueada";
                    _model = model;
                    return Redirect("/");
                } else
                {
                    model.Error = "Correo o Contraseña Inválidos";
                    _model = model;
                    return Redirect("/");
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task CreateRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            String[] rolesName = { "Admin", "User" };
            foreach(var item in rolesName)
            {
                var roleExist = await roleManager.RoleExistsAsync(item);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(item));
                }
            }
        }
    }
}
