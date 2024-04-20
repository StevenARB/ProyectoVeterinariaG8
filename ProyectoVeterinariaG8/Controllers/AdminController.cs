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
        private readonly VeterinariaContext _veterinariaContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;

        public AdminController(AuthContext authContext, VeterinariaContext veterinariaContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IUserStore<ApplicationUser> userStore)
        {
            _authContext = authContext;
            _veterinariaContext = veterinariaContext;
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
            ViewData["Estados"] = new SelectList(_veterinariaContext.EstadosUsuario, "EstadoId", "Descripcion");
            ViewData["Roles"] = new SelectList(_roleManager.Roles, "Id", "NormalizedName");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> CreateUser(AdminCreateUserViewModel userModel, IFormFile imagen)
        {
            if (ModelState.IsValid)
            {
                byte[]? imagenVariable = null;

                if (imagen != null && imagen.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await imagen.CopyToAsync(memoryStream);
                        imagenVariable = memoryStream.ToArray();
                    }
                }

                var user = new ApplicationUser();

                await _userStore.SetUserNameAsync(user, userModel.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, userModel.Email, CancellationToken.None);
                user.Nombre = userModel.Nombre;
                user.PrimerApellido = userModel.PrimerApellido;
                user.SegundoApellido = userModel.SegundoApellido;
                user.Imagen = imagenVariable;
                user.FechaUltimaConexion = userModel.FechaUltimaConexion;
                user.EstadoUsuarioId = userModel.EstadoUsuarioId;
                var result = await _userManager.CreateAsync(user, userModel.Password);

                if (result.Succeeded)
                {
                    string normalizedNameRole = _roleManager.Roles.FirstOrDefault(r => r.Id == userModel.IdRol).NormalizedName;
                    var roleResult = await _userManager.AddToRoleAsync(user, normalizedNameRole);
                    var userId = await _userManager.GetUserIdAsync(user);

                    return RedirectToAction("Index", "Home");
                }
            }
            ViewData["Estados"] = new SelectList(_veterinariaContext.EstadosUsuario, "EstadoId", "Descripcion");
            ViewData["Roles"] = new SelectList(_roleManager.Roles, "Id", "NormalizedName");
            return View(userModel);
        }

        public async Task<IActionResult> EditUser(string? id)
        {
            if (id == null || _userManager.Users == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new AdminEditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Nombre = user.Nombre,
                PrimerApellido = user.PrimerApellido,
                SegundoApellido = user.SegundoApellido,
                Imagen = user.Imagen,
                EstadoUsuarioId = user.EstadoUsuarioId
            };

            ViewData["Estados"] = new SelectList(_veterinariaContext.EstadosUsuario, "EstadoId", "Descripcion");
            ViewData["Roles"] = new SelectList(_roleManager.Roles, "Id", "NormalizedName");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(string? id, AdminEditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (id != model.Id || user == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                user.Email = model.Email;
                user.Nombre = model.Nombre;
                user.PrimerApellido = model.PrimerApellido;
                user.SegundoApellido = model.SegundoApellido;
                user.Imagen = model.Imagen;
                user.EstadoUsuarioId = model.EstadoUsuarioId;

                if (model.Password != null && model.Password == model.ConfirmPassword)
                {
                    var changePassword = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);

                    if (changePassword.Succeeded)
                    {
                        TempData["UserPasswordUpdatedMessage"] = "La contraseña se ha actualizado correctamente.";
                    }
                }

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded) 
                {
                    TempData["UserUpdatedMessage"] = "El usuario se ha actualizado correctamente.";
                    //return RedirectToAction(nameof(Index));
                }
                ViewData["Estados"] = new SelectList(_veterinariaContext.EstadosUsuario, "EstadoId", "Descripcion", model.EstadoUsuarioId);
                ViewData["Roles"] = new SelectList(_roleManager.Roles, "Id", "NormalizedName");
            }

            ViewData["Estados"] = new SelectList(_veterinariaContext.EstadosUsuario, "EstadoId", "Descripcion");
            ViewData["Roles"] = new SelectList(_roleManager.Roles, "Id", "NormalizedName");
            return View(model);
        }

        private bool UserExists(string id)
        {
            return (_authContext.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
