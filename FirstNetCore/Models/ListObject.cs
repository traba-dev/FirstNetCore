using FirstNetCore.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstNetCore.Models
{
    public class ListObject
    {
        public IdentityError identityError;
        public ApplicationDbContext context;
        public IWebHostEnvironment environment;

        public RoleManager<IdentityRole> roleManager;
        public UserManager<IdentityUser> userManager;
        public SignInManager<IdentityUser> signInManager;
    }
}
