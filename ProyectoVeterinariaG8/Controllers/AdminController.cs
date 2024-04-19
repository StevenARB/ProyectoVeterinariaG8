using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoVeterinariaG8.DAL;
using ProyectoVeterinariaG8.Models;

namespace ProyectoVeterinariaG8.Controllers
{
    public class AdminController : Controller
    {
        private readonly AuthContext _authContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;

        public AdminController(AuthContext authContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IUserStore<ApplicationUser> userStore)
        {
            _authContext = authContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _userStore = userStore;
            _emailStore = (IUserEmailStore<ApplicationUser>)_userStore;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult CreateUser()
        {
            ViewData["Roles"] = new SelectList(_roleManager.Roles, "Id", "NormalizedName");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> CreateUser(AdminCreateUserViewModel userModel) 
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();

                await _userStore.SetUserNameAsync(user, userModel.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, userModel.Email, CancellationToken.None);
                user.Nombre = userModel.Nombre;
                user.PrimerApellido = userModel.PrimerApellido;
                user.SegundoApellido = userModel.SegundoApellido;
                user.Imagen = userModel.Imagen;
                user.FechaUltimaConexion = userModel.FechaUltimaConexion;
                user.EstadoUsuarioId = 1;
                var result = await _userManager.CreateAsync(user, userModel.Password);

                if (result.Succeeded)
                {
                    string normalizedNameRole = _roleManager.Roles.FirstOrDefault(r => r.Id == userModel.IdRol).NormalizedName;
                    var roleResult = await _userManager.AddToRoleAsync(user, normalizedNameRole);
                    var userId = await _userManager.GetUserIdAsync(user);

                    return RedirectToAction("Index", "Home");
                }
                }

            ViewData["Roles"] = new SelectList(_roleManager.Roles, "Id", "NormalizedName");
            return View(userModel);
        }
    }
}
