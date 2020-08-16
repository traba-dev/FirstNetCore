using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirstNetCore.Areas.Principal.Controllers
{
    
    [Area("Principal")]
    public class PrincipalController : Controller
    {

        
        public PrincipalController()
        {
        }
        //[Authorize(Roles = "User,Admin")] //Autorización basada en roles
        [Authorize(Policy = "Authorization")] //Autorización basada en políticas
        public IActionResult Principal()
        {
            return View();
        }
    }
}
