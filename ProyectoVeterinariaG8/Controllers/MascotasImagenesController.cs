using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoVeterinariaG8.DAL;
using ProyectoVeterinariaG8.Models;

namespace ProyectoVeterinariaG8.Controllers
{
    public class MascotasImagenesController : Controller
    {
        private readonly VeterinariaContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MascotasImagenesController(VeterinariaContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: MascotasImagenes
        public async Task<IActionResult> Index()
        {
            var usuarioAutenticado = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(usuarioAutenticado);

            if (roles == null)
            {
                return NotFound();
            }

            if (roles.Contains("Cliente"))
            {
                var veterinariaContext = _context.MascotasImagenes
                    .Include(m => m.Mascota)
                    .ThenInclude(m => m.EstadoMascota)
                    .Where(m => m.Mascota.UsuarioPropietarioId == usuarioAutenticado.Id && m.Mascota.EstadoMascota.Descripcion == "Activo");

                return View(await veterinariaContext.ToListAsync());

            }

            if (roles.Contains("Veterinario") || roles.Contains("Administrador"))
            {
                var veterinariaContext = _context.MascotasImagenes
                    .Include(m => m.Mascota)
                    .ThenInclude(m => m.EstadoMascota)
                    .Where(m => m.Mascota.UsuarioPropietario.EstadoUsuario.Descripcion == "Activo" && m.Mascota.EstadoMascota.Descripcion == "Activo");

                return View(await veterinariaContext.ToListAsync());
            }

            return View();
        }

        // GET: MascotasImagenes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MascotasImagenes == null)
            {
                return NotFound();
            }

            var mascotaImagen = await _context.MascotasImagenes
                .Include(m => m.Mascota)
                .FirstOrDefaultAsync(m => m.ImagenId == id);
            if (mascotaImagen == null)
            {
                return NotFound();
            }

            return View(mascotaImagen);
        }

        // GET: MascotasImagenes/Create
        public async Task<IActionResult> Create()
        {
            var usuarioAutenticado = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(usuarioAutenticado);

            if (roles == null)
            {
                return NotFound();
            }

            if (roles.Contains("Cliente"))
            {
                ViewData["MascotaId"] = new SelectList(_context.Mascotas.Where(m => m.UsuarioPropietarioId == usuarioAutenticado.Id), "MascotaId", "Nombre");
            }

            if (roles.Contains("Veterinario") || roles.Contains("Administrador"))
            {
                ViewData["MascotaId"] = new SelectList(_context.Mascotas, "MascotaId", "Nombre");
            }

            return View();
        }

        // POST: MascotasImagenes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImagenId,MascotaId")] MascotaImagenCreateViewModel mascotaImagenView, IFormFile imagen)
        {
            var usuarioAutenticado = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(usuarioAutenticado);

            if (roles == null)
            {
                return NotFound();
            }

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

                MascotaImagen mascotaImagen = new MascotaImagen
                {
                    MascotaId = mascotaImagenView.MascotaId,
                    Imagen = imagenVariable
                };

                _context.Add(mascotaImagen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            if (roles.Contains("Cliente"))
            {
                ViewData["MascotaId"] = new SelectList(_context.Mascotas.Where(m => m.UsuarioPropietarioId == usuarioAutenticado.Id), "MascotaId", "Nombre", mascotaImagenView.MascotaId);
            }

            if (roles.Contains("Veterinario") || roles.Contains("Administrador"))
            {
                ViewData["MascotaId"] = new SelectList(_context.Mascotas, "MascotaId", "Nombre", mascotaImagenView.MascotaId);
            }
            return View(mascotaImagenView);
        }

        // GET: MascotasImagenes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var usuarioAutenticado = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(usuarioAutenticado);

            if (roles == null)
            {
                return NotFound();
            }

            if (id == null || _context.MascotasImagenes == null)
            {
                return NotFound();
            }

            var mascotaImagen = await _context.MascotasImagenes.FindAsync(id);
            if (mascotaImagen == null)
            {
                return NotFound();
            }

            if (roles.Contains("Cliente"))
            {
                ViewData["MascotaId"] = new SelectList(_context.Mascotas.Where(m => m.UsuarioPropietarioId == usuarioAutenticado.Id), "MascotaId", "Nombre", mascotaImagen.MascotaId);
            }

            if (roles.Contains("Veterinario") || roles.Contains("Administrador"))
            {
                ViewData["MascotaId"] = new SelectList(_context.Mascotas, "MascotaId", "Nombre", mascotaImagen.MascotaId);
            }

            return View(mascotaImagen);
        }

        // POST: MascotasImagenes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImagenId,MascotaId")] MascotaImagenCreateViewModel mascotaImagenView, IFormFile imagen)
        {
            var usuarioAutenticado = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(usuarioAutenticado);

            if (roles == null)
            {
                return NotFound();
            }

            if (id != mascotaImagenView.ImagenId)
            {
                return NotFound();
            }

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
                try
                {
                    MascotaImagen mascotaImagen = new MascotaImagen
                    {
                        ImagenId = id,
                        MascotaId = mascotaImagenView.MascotaId,
                        Imagen = imagenVariable
                    };
                    _context.Update(mascotaImagen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MascotaImagenExists(mascotaImagenView.ImagenId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            if (roles.Contains("Cliente"))
            {
                ViewData["MascotaId"] = new SelectList(_context.Mascotas.Where(m => m.UsuarioPropietarioId == usuarioAutenticado.Id), "MascotaId", "Nombre", mascotaImagenView.MascotaId);
            }

            if (roles.Contains("Veterinario") || roles.Contains("Administrador"))
            {
                ViewData["MascotaId"] = new SelectList(_context.Mascotas, "MascotaId", "Nombre", mascotaImagenView.MascotaId);
            }

            return View(mascotaImagenView);
        }

        // GET: MascotasImagenes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MascotasImagenes == null)
            {
                return NotFound();
            }

            var mascotaImagen = await _context.MascotasImagenes
                .Include(m => m.Mascota)
                .FirstOrDefaultAsync(m => m.ImagenId == id);
            if (mascotaImagen == null)
            {
                return NotFound();
            }

            return View(mascotaImagen);
        }

        // POST: MascotasImagenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MascotasImagenes == null)
            {
                return Problem("Entity set 'VeterinariaContext.MascotasImagenes'  is null.");
            }
            var mascotaImagen = await _context.MascotasImagenes.FindAsync(id);
            if (mascotaImagen != null)
            {
                _context.MascotasImagenes.Remove(mascotaImagen);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MascotaImagenExists(int id)
        {
          return (_context.MascotasImagenes?.Any(e => e.ImagenId == id)).GetValueOrDefault();
        }
    }
}
