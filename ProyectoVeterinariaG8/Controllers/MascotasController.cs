using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoVeterinariaG8.DAL;

namespace ProyectoVeterinariaG8.Controllers
{
    public class MascotasController : Controller
    {
        private readonly VeterinariaContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MascotasController(VeterinariaContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Mascotas
        [Authorize]
        public async Task<IActionResult> Index()
        {
            //var usuarioAutenticado = User.Identity;
            //String usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usuarioAutenticado = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(usuarioAutenticado);

            if (roles == null)
            {
                return NotFound();
            }

            if (roles.Contains("Cliente"))
            {
                var veterinariaContext = _context.Mascotas.Include(m => m.UsuarioPropietario).Include(m => m.RazaMascota).Include(m => m.TipoMascota).Include(m => m.EstadoMascota).Where(m => m.UsuarioPropietario.Id == usuarioAutenticado.Id && m.EstadoMascota.Descripcion == "Activo");
                return View(await veterinariaContext.ToListAsync());
            }

            if (roles.Contains("Veterinario") || roles.Contains("Administrador"))
            {
                var veterinariaContext = _context.Mascotas.Include(m => m.UsuarioPropietario).Include(m => m.RazaMascota).Include(m => m.TipoMascota).Include(m => m.EstadoMascota).Where(m => m.UsuarioPropietario.EstadoUsuario.Descripcion == "Activo" && m.EstadoMascota.Descripcion == "Activo");
                return View(await veterinariaContext.ToListAsync());
            }

            return View();
        }

        // GET: Mascotas/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mascotas == null || _context.MascotasImagenes == null)
            {
                return NotFound();
            }

            var usuarioAutenticado = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(usuarioAutenticado);

            if (roles == null)
            {
                return NotFound();
            }

            if (roles.Contains("Cliente"))
            {
                var mascota = await _context.Mascotas
                    .Include(m => m.RazaMascota)
                    .Include(m => m.TipoMascota)
                    .Include(m => m.EstadoMascota)
                    .Include(m => m.MascotaImagenes)
                    .Where(m => m.UsuarioPropietarioId == usuarioAutenticado.Id)
                    .FirstOrDefaultAsync(m => m.MascotaId == id);

                if (mascota == null)
                {
                    return NotFound();
                }

                return View(mascota);
            }

            if (roles.Contains("Veterinario") || roles.Contains("Administrador"))
            {
                var mascota = await _context.Mascotas
                    .Include(m => m.RazaMascota)
                    .Include(m => m.TipoMascota)
                    .Include(m => m.EstadoMascota)
                    .Include(m => m.MascotaImagenes)
                    .FirstOrDefaultAsync(m => m.MascotaId == id);

                if (mascota == null)
                {
                    return NotFound();
                }

                return View(mascota);
            }

            return View();
        }

        // GET: Mascotas/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var usuarioAutenticado = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(usuarioAutenticado);

            if (roles == null)
            {
                return NotFound();
            }

            ViewData["TiposRazas"] = await _context.TiposMascotas.Include(tipo => tipo.RazasMascota).ToListAsync();
            ViewData["EstadoId"] = new SelectList(_context.EstadosMascotas, "EstadoId", "Descripcion");

            if (roles.Contains("Cliente")) 
            {
                ViewData["UsuariosPropietario"] = new SelectList(_userManager.Users.Where(u => u.Id == usuarioAutenticado.Id), "Id", "Email");
                ViewData["UsuariosCreacion"] = new SelectList(_userManager.Users.Where(u => u.Id == usuarioAutenticado.Id), "Id", "Email");
                ViewData["UsuariosModificacion"] = new SelectList(_userManager.Users.Where(u => u.Id == usuarioAutenticado.Id), "Id", "Email");
            }

            if (roles.Contains("Veterinario") || roles.Contains("Administrador"))
            {
                ViewData["UsuariosPropietario"] = new SelectList(_userManager.Users, "Id", "Email");
                ViewData["UsuariosCreacion"] = new SelectList(_userManager.Users, "Id", "Email");
                ViewData["UsuariosModificacion"] = new SelectList(_userManager.Users, "Id", "Email");
            }

            return View();
        }

        // POST: Mascotas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MascotaId,Nombre,TipoId,RazaId,Genero,Edad,Peso,EstadoId,UsuarioPropietarioId,UsuarioCreacionId,UsuarioModificacionId")] Mascota mascota, IFormFile imagen)
        {
            if (ModelState.IsValid)
            {
                mascota.FechaCreacion = DateTime.Now;
                byte[]? imagenVariable = null;

                if (imagen != null && imagen.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await imagen.CopyToAsync(memoryStream);
                        imagenVariable = memoryStream.ToArray();
                    }
                }

                _context.Add(mascota);
                await _context.SaveChangesAsync();

                MascotaImagen mascotaImagen = new MascotaImagen
                {
                    MascotaId = mascota.MascotaId,
                    Imagen = imagenVariable
                };

                _context.Add(mascotaImagen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstadoId"] = new SelectList(_context.EstadosMascotas, "EstadoId", "Descripcion", mascota.EstadoId);
            ViewData["UsuariosPropietario"] = new SelectList(_userManager.Users, "Id", "Email", mascota.UsuarioPropietarioId);
            ViewData["UsuariosCreacion"] = new SelectList(_userManager.Users, "Id", "Email", mascota.UsuarioCreacionId);
            ViewData["UsuariosModificacion"] = new SelectList(_userManager.Users, "Id", "Email", mascota.UsuarioModificacionId);
            var usuarioAutenticado = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(usuarioAutenticado);

            if (roles == null)
            {
                return NotFound();
            }

            ViewData["EstadoId"] = new SelectList(_context.EstadosMascotas, "EstadoId", "Descripcion", mascota.EstadoId);

            if (roles.Contains("Cliente"))
            {
                ViewData["UsuariosPropietario"] = new SelectList(_userManager.Users.Where(u => u.Id == usuarioAutenticado.Id), "Id", "Email", mascota.UsuarioPropietarioId);
                ViewData["UsuariosCreacion"] = new SelectList(_userManager.Users.Where(u => u.Id == usuarioAutenticado.Id), "Id", "Email", mascota.UsuarioCreacionId);
                ViewData["UsuariosModificacion"] = new SelectList(_userManager.Users.Where(u => u.Id == usuarioAutenticado.Id), "Id", "Email", mascota.UsuarioModificacionId);
            }

            if (roles.Contains("Veterinario") || roles.Contains("Administrador"))
            {
                ViewData["UsuariosPropietario"] = new SelectList(_userManager.Users, "Id", "Email", mascota.UsuarioPropietarioId);
                ViewData["UsuariosCreacion"] = new SelectList(_userManager.Users, "Id", "Email", mascota.UsuarioCreacionId);
                ViewData["UsuariosModificacion"] = new SelectList(_userManager.Users, "Id", "Email", mascota.UsuarioModificacionId);
            }
            return View(mascota);
        }

        // GET: Mascotas/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            var usuarioAutenticado = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(usuarioAutenticado);

            if (roles == null)
            {
                return NotFound();
            }

            if (id == null || _context.Mascotas == null)
            {
                return NotFound();
            }

            var mascota = await _context.Mascotas.FindAsync(id);
            if (mascota == null)
            {
                return NotFound();
            }

            ViewData["TiposRazas"] = await _context.TiposMascotas.Include(tipo => tipo.RazasMascota).ToListAsync();
            ViewData["EstadoId"] = new SelectList(_context.EstadosMascotas, "EstadoId", "Descripcion", mascota.EstadoId);
            if (roles.Contains("Cliente"))
            {
                ViewData["UsuariosPropietario"] = new SelectList(_userManager.Users.Where(u => u.Id == usuarioAutenticado.Id), "Id", "Email", mascota.UsuarioPropietarioId);
                ViewData["UsuariosCreacion"] = new SelectList(_userManager.Users.Where(u => u.Id == usuarioAutenticado.Id), "Id", "Email", mascota.UsuarioCreacionId);
                ViewData["UsuariosModificacion"] = new SelectList(_userManager.Users.Where(u => u.Id == usuarioAutenticado.Id), "Id", "Email", mascota.UsuarioModificacionId);
            }

            if (roles.Contains("Veterinario") || roles.Contains("Administrador"))
            {
                ViewData["UsuariosPropietario"] = new SelectList(_userManager.Users, "Id", "Email", mascota.UsuarioPropietarioId);
                ViewData["UsuariosCreacion"] = new SelectList(_userManager.Users, "Id", "Email", mascota.UsuarioCreacionId);
                ViewData["UsuariosModificacion"] = new SelectList(_userManager.Users, "Id", "Email", mascota.UsuarioModificacionId);
            }
            return View(mascota);
        }

        // POST: Mascotas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MascotaId,Nombre,TipoId,RazaId,Genero,Edad,Peso,EstadoId,UsuarioPropietarioId,UsuarioCreacionId,UsuarioModificacionId,FechaCreacion")] Mascota mascota)
        {
            var usuarioAutenticado = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(usuarioAutenticado);

            if (roles == null)
            {
                return NotFound();
            }

            if (id != mascota.MascotaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    mascota.FechaModificacion = DateTime.Now;
                    _context.Update(mascota);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MascotaExists(mascota.MascotaId))
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
            ViewData["TiposRazas"] = _context.TiposMascotas.Include(tipo => tipo.RazasMascota).ToList();
            ViewData["EstadoId"] = new SelectList(_context.EstadosMascotas, "EstadoId", "Descripcion", mascota.EstadoId);
            if (roles.Contains("Cliente"))
            {
                ViewData["UsuariosPropietario"] = new SelectList(_userManager.Users.Where(u => u.Id == usuarioAutenticado.Id), "Id", "Email", mascota.UsuarioPropietarioId);
                ViewData["UsuariosCreacion"] = new SelectList(_userManager.Users.Where(u => u.Id == usuarioAutenticado.Id), "Id", "Email", mascota.UsuarioCreacionId);
                ViewData["UsuariosModificacion"] = new SelectList(_userManager.Users.Where(u => u.Id == usuarioAutenticado.Id), "Id", "Email", mascota.UsuarioModificacionId);
            }

            if (roles.Contains("Veterinario") || roles.Contains("Administrador"))
            {
                ViewData["UsuariosPropietario"] = new SelectList(_userManager.Users, "Id", "Email", mascota.UsuarioPropietarioId);
                ViewData["UsuariosCreacion"] = new SelectList(_userManager.Users, "Id", "Email", mascota.UsuarioCreacionId);
                ViewData["UsuariosModificacion"] = new SelectList(_userManager.Users, "Id", "Email", mascota.UsuarioModificacionId);
            }
            return View(mascota);
        }

        // GET: Mascotas/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            var usuarioAutenticado = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(usuarioAutenticado);

            if (roles == null)
            {
                return NotFound();
            }

            if (id == null || _context.Mascotas == null)
            {
                return NotFound();
            }

            if (roles.Contains("Cliente"))
            {
                var mascota = await _context.Mascotas
                    .Include(m => m.RazaMascota)
                    .Include(m => m.TipoMascota)
                    .Include(m => m.EstadoMascota)
                    .Where(m=> m.UsuarioPropietarioId == usuarioAutenticado.Id)
                    .FirstOrDefaultAsync(m => m.MascotaId == id);

                if (mascota == null)
                {
                    return NotFound();
                }

                return View(mascota);
            }

            if (roles.Contains("Veterinario") || roles.Contains("Administrador"))
            {
                var mascota = await _context.Mascotas
                    .Include(m => m.RazaMascota)
                    .Include(m => m.TipoMascota)
                    .Include(m => m.EstadoMascota)
                    .FirstOrDefaultAsync(m => m.MascotaId == id);

                if (mascota == null)
                {
                    return NotFound();
                }

                return View(mascota);
            }

            return NotFound();
        }

        // POST: Mascotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuarioAutenticado = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(usuarioAutenticado);

            if (roles == null)
            {
                return NotFound();
            }

            if (_context.Mascotas == null)
            {
                return Problem("Entity set 'VeterinariaContext.Mascotas'  is null.");
            }

            var estado = await _context.EstadosMascotas.FirstOrDefaultAsync(e => e.Descripcion == "Inactivo");

            if (roles.Contains("Cliente"))
            {
                var mascota = await _context.Mascotas.Where(m => m.UsuarioPropietarioId == usuarioAutenticado.Id && m.MascotaId == id).FirstOrDefaultAsync();

                if (mascota != null && estado != null)
                {
                    mascota.EstadoId = estado.EstadoId;
                    _context.Update(mascota);
                }
            }

            if (roles.Contains("Veterinario") || roles.Contains("Administrador"))
            {
                var mascota = await _context.Mascotas.FindAsync(id);

                if (mascota != null && estado != null)
                {
                    mascota.EstadoId = estado.EstadoId;
                    _context.Update(mascota);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        private bool MascotaExists(int id)
        {
            return (_context.Mascotas?.Any(e => e.MascotaId == id)).GetValueOrDefault();
        }
    }
}
