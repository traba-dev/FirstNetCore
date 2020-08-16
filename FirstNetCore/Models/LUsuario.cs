using FirstNetCore.Areas.User.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstNetCore.Models
{
    public class LUsuario: ListObject
    {
        public LUsuario(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        internal async Task<SignInResult> userLoginAsync(LoginModels loginModels)
        {
            var result = await this.signInManager.PasswordSignInAsync(loginModels.Email, loginModels.Password, false, lockoutOnFailure: false);
            return result;
        }
    }
}
