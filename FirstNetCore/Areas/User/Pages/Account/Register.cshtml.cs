using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FirstNetCore.Areas.User.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;
        private static InputModel _input = null;
        public RegisterModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public void OnGet()
        {
            this.Input = _input;
        }
        public InputModel Input { get; set; }
        public class InputModel
        {
            [Required(ErrorMessage = "El campo Email es requerido")]
            [EmailAddress(ErrorMessage = "El correo no es un formato de correo válido")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "El campo Password es requerido")]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña")]
            [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
            public string Password { get; set; }

            [Required(ErrorMessage = "El campo ConfirmPassword es requerido")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Las constraseñas no son iguales")]
            public string ConfirmPassword { get; set; }

            public string Error { get; set; }
        }

        public async Task<IActionResult> OnPostAsync(InputModel input)
        {
            if (await RegisterUser(input))
            {
                return Redirect("/Principal/Principal?area=Principal");
            }
            return Redirect("/user/Register");
        }

        private async Task<bool> RegisterUser(InputModel input) 
        {
            bool isSucces = false;

            if (ModelState.IsValid)
            {
                var userList = this.userManager.Users.Where(u => u.Email.Equals(input.Email)).ToList();

                if (userList.Count > 0)
                {
                    _input = new InputModel
                    {
                        Email = input.Email,
                        Error = "Correo electrónico no disponible"
                    };
                }
                else
                {
                    var user = new IdentityUser
                    {
                        UserName = input.Email,
                        Email = input.Email
                    };
                    var result = await this.userManager.CreateAsync(user, input.Password);

                    if (!result.Succeeded)
                    {
                        foreach (var item in result.Errors)
                        {
                            _input = new InputModel
                            {
                                Email = input.Email,
                                Error = item.Description
                            };
                        }
                    }
                    else
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        isSucces = true;
                    }
                }

            }
            else
            {
                _input = new InputModel
                {
                    Email = input.Email,
                    Error = "Todos los campos son requeridos"
                };
            }
            return isSucces;
        }
    }
}
