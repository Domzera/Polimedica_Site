using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Polimedica.Data;
using Polimedica.Models;
using Polimedica.ViewModel;

namespace Polimedica.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsuarioController(
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()//   ==> Index
        {
            return View();
        }
        public IActionResult Login()//   ==> Login
        {
            var response = new LoginViewModel();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)//   ==> Login
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user != null)
            {
                var passWordCheck = await _userManager.CheckPasswordAsync(user, login.Password);
                if (passWordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
                    if (result.Succeeded)
                    {
                        var type = await _userManager.GetRolesAsync(user);
                        switch (type[0])
                        {
                            case "Admin":
                                return RedirectToAction("Dashboard", "Usuario");
                                //break;
                            case "Gerente":
                                return RedirectToAction("Dashboard", "Usuario");
                                //break;
                            case "Vendedor":
                                return RedirectToAction("Index", "Vendedor");
                                //break;
                            case "Cliente_Pj":
                                return RedirectToAction("Index", "Cliente");
                                //break;
                            case "Cliente_Pf":
                                return RedirectToAction("Index", "Cliente");
                                //break;
                        }
                    }
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult RegistroUsuario()//   ==> Registro de Usuário
        {
            var response = new UserRegisterViewModel();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> RegistroUsuario(UserRegisterViewModel userRegister)//   ==> Registro de Usuário
        {
            if (!ModelState.IsValid)
            {
                return View(userRegister);
            }
            var user = await _userManager.FindByEmailAsync(userRegister.Email);

            if (user == null)
            {
                var newUser = new Usuario()
                {
                    UserName = userRegister.Name + "-" + userRegister.SobreNome + "-" + userRegister.Email,
                    PrimeiroNome = userRegister.Name,
                    SobreNome = userRegister.SobreNome,
                    Email = userRegister.Email,
                    DataDeCadastro = DateOnly.FromDateTime(DateTime.Now)
                };
                var result = await _userManager.CreateAsync(newUser, userRegister.Password);

                if (result.Succeeded)
                {
                    if(!await _roleManager.RoleExistsAsync("Gerente"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Gerente"));
                        await _userManager.AddToRoleAsync(newUser, UserRoles.Gerente);
                        await _signInManager.SignInAsync(newUser, isPersistent: true);
                        return RedirectToAction("Dashboard", "Usuario");
                    }
                    else
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Cliente_Pf"));
                        await _userManager.AddToRoleAsync(newUser, UserRoles.ClientePf);
                        await _signInManager.SignInAsync(newUser, isPersistent: true);
                        return RedirectToAction("Index", "Cliente");
                    }
                }   
            }
            return View(userRegister);

        }
        [Authorize]
        public IActionResult Dashboard()//   ==> Dashboard
        {
            IEnumerable<Usuario> usuarios = _userManager.Users.AsEnumerable();
            if (usuarios == null) return View("Error");

            return View(usuarios);
        }
        public async Task<IActionResult> Editar(string id)//   ==>  Editar
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return View("error");

            var regra = await _userManager.GetRolesAsync(user);

            var userVm = new EditarUsuarioViewModel
            {
                PrimeiroNome = user.PrimeiroNome,
                SobreNome = user.SobreNome,
                Email = user.Email,
                Hierarquia = regra[0]
            };

            return View(userVm);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Editar(EditarUsuarioViewModel editarUser)//   ==>  Editar
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Fail to edit");
                return RedirectToAction("Dashboard");
            }

            var user = await _userManager.FindByEmailAsync(editarUser.Email);

            if (user != null)
            {
                user.PrimeiroNome = editarUser.PrimeiroNome;
                user.SobreNome = editarUser.SobreNome;
                user.Email = editarUser.Email;
                user.UserName = editarUser.PrimeiroNome + "-" + editarUser.SobreNome + "-" + editarUser.Email;

                var userUpdated = await _userManager.UpdateAsync(user);

                if (userUpdated.Succeeded)
                {
                    var regra = await _userManager.GetRolesAsync(user);

                    if (!editarUser.Hierarquia.Equals(regra[0]))
                    {
                        switch (regra[0])
                        {
                            case "Adimn":
                                await _userManager.RemoveFromRoleAsync(user, UserRoles.Admin);
                                break;
                            case "Gerente":
                                await _userManager.RemoveFromRoleAsync(user, UserRoles.Gerente);
                                break;
                            case "Vendedor":
                                await _userManager.RemoveFromRoleAsync(user, UserRoles.Vendedor);
                                break;
                            case "Cliente_Pf":
                                await _userManager.RemoveFromRoleAsync(user, UserRoles.ClientePf);
                                break;
                            case "Cliente_Pj":
                                await _userManager.RemoveFromRoleAsync(user, UserRoles.ClientePj);
                                break;
                        }

                        switch (editarUser.Hierarquia.ToString())
                        {
                            case "Admin":
                                if (!await _roleManager.RoleExistsAsync("Admin"))
                                {
                                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                                }
                                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
                                break;
                            case "Gerente":
                                if (!await _roleManager.RoleExistsAsync("Gerente"))
                                {
                                    await _roleManager.CreateAsync(new IdentityRole("Gerente"));
                                }
                                await _userManager.AddToRoleAsync(user, UserRoles.Gerente);
                                break;
                            case "Vendedor":
                                if (!await _roleManager.RoleExistsAsync("Vendedor"))
                                {
                                    await _roleManager.CreateAsync(new IdentityRole("Vendedor"));
                                }
                                await _userManager.AddToRoleAsync(user, UserRoles.Vendedor);
                                break;
                            case "Cliente_Pf":
                                if (!await _roleManager.RoleExistsAsync("Cliente_Pf"))
                                {
                                    await _roleManager.CreateAsync(new IdentityRole("Cliente_Pf"));
                                }
                                await _userManager.AddToRoleAsync(user, UserRoles.ClientePf);
                                break;
                            case "Cliente_Pj":
                                if (!await _roleManager.RoleExistsAsync("Cliente_Pj"))
                                {
                                    await _roleManager.CreateAsync(new IdentityRole("Cliente_Pj"));
                                }
                                await _userManager.AddToRoleAsync(user, UserRoles.ClientePj);
                                break;
                        }
                    }
                }

                //return RedirectToAction("Dashboard");
                return RedirectToAction("Dashboard", "Usuario");

            }
            else
            {
                return View("Index");
            }

        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
