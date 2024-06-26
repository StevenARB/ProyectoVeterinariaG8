﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ProyectoVeterinariaG8.DAL;
using ProyectoVeterinariaG8.Models;
using System.Security.Claims;
using System;

namespace ProyectoVeterinariaG8.Controllers
{
    public class AdminController : Controller
    {
        private readonly VeterinariaContext _veterinariaContext;
        private readonly AuthContext _authContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;

        public AdminController(VeterinariaContext veterinariaContext, AuthContext authContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IUserStore<ApplicationUser> userStore)
        {
            _veterinariaContext = veterinariaContext;
            _authContext = authContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _userStore = userStore;
            _emailStore = (IUserEmailStore<ApplicationUser>)_userStore;
        }

        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> HomeAdministrador()
        {
            var tipoMascotas = await _veterinariaContext.TiposMascotas.ToListAsync();
            ViewBag.tipoMascotas = tipoMascotas;

            var razasMascotas = await _veterinariaContext.RazasMascotas.ToListAsync();
            ViewBag.razasMascotas = razasMascotas;

            var medicamentos = await _veterinariaContext.Medicamentos.ToListAsync();
            ViewBag.medicamentos = medicamentos;

            var usuarios = await _userManager.Users.Where(u => u.EstadoUsuario.Descripcion == "Activo").ToListAsync();
            ViewBag._usuarios = usuarios;

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DetailsUser(string? id)
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

            var roles = await _userManager.GetRolesAsync(user);
            ViewBag.Roles = roles;

            return View(user);
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

                    return RedirectToAction("HomeAdministrador", "Admin");
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

            var rolesUser = await _userManager.GetRolesAsync(user);
            var roleId = _roleManager.Roles.FirstOrDefault(r => r.NormalizedName == rolesUser.FirstOrDefault())?.Id;

            var model = new AdminEditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Nombre = user.Nombre,
                PrimerApellido = user.PrimerApellido,
                SegundoApellido = user.SegundoApellido,
                Imagen = user.Imagen,
                EstadoUsuarioId = user.EstadoUsuarioId,
                IdRol = roleId
            };

            ViewData["Estados"] = new SelectList(_veterinariaContext.EstadosUsuario, "EstadoId", "Descripcion", model.EstadoUsuarioId);
            ViewData["Roles"] = new SelectList(_roleManager.Roles, "Id", "NormalizedName", model.IdRol);
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

                if (model.OldPassword != null && model.Password == model.ConfirmPassword)
                {
                    var changePassword = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);

                    if (changePassword.Succeeded)
                    {
                        TempData["UserPasswordUpdatedMessage"] = "La contraseña se ha actualizado correctamente.";
                    }
                    else 
                    {
                        TempData["UserPasswordUpdatedMessageError"] = "La contraseña no se pudo actualizar.";
                    }
                }

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    TempData["UserUpdatedMessage"] = "El usuario se ha actualizado correctamente.";
                    return RedirectToAction(nameof(HomeAdministrador));
                }
            }

            ViewData["Estados"] = new SelectList(_veterinariaContext.EstadosUsuario, "EstadoId", "Descripcion", model.EstadoUsuarioId);
            ViewData["Roles"] = new SelectList(_roleManager.Roles, "Id", "NormalizedName", model.IdRol);
            return View(model);
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteUser(string? id)
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

            var roles = await _userManager.GetRolesAsync(user);
            ViewBag.Roles = roles;

            return View(user);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserConfirmed(string? id)
        {
            if (id == null || _userManager.Users == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            var estadoUsuario = await _veterinariaContext.EstadosUsuario.Where(e => e.Descripcion == "Inactivo").FirstOrDefaultAsync();
            if (user == null || estadoUsuario == null)
            {
                return NotFound();
            }

            user.EstadoUsuarioId = estadoUsuario.EstadoId;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) 
            {
                TempData["UserDeleted"] = "El usuario se ha eliminado correctamente.";
            }

            return RedirectToAction(nameof(HomeAdministrador));
        }

        private bool UserExists(string id)
        {
            return (_authContext.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
