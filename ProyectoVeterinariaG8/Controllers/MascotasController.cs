using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoVeterinariaG8.DAL;

namespace ProyectoVeterinariaG8.Controllers
{
    public class MascotasController : Controller
    {
        private readonly VeterinariaContext _context;

        public MascotasController(VeterinariaContext context)
        {
            _context = context;
        }

        // GET: Mascotas
        public async Task<IActionResult> Index()
        {
            var veterinariaContext = _context.Mascotas.Include(m => m.RazaMascota).Include(m => m.TipoMascota).Include(m => m.EstadoMascota);
            return View(await veterinariaContext.ToListAsync());
        }

        // GET: Mascotas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mascotas == null || _context.MascotasImagenes == null)
            {
                return NotFound();
            }

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

        // GET: Mascotas/Create
        public IActionResult Create()
        {
            ViewData["RazaId"] = new SelectList(_context.RazasMascotas, "RazaId", "Descripcion");
            ViewData["TipoId"] = new SelectList(_context.TiposMascotas, "TipoId", "Descripcion");
            ViewData["EstadoId"] = new SelectList(_context.EstadosMascotas, "EstadoId", "Descripcion");
            return View();
        }

        // POST: Mascotas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            ViewData["RazaId"] = new SelectList(_context.RazasMascotas, "RazaId", "Descripcion", mascota.RazaId);
            ViewData["TipoId"] = new SelectList(_context.TiposMascotas, "TipoId", "Descripcion", mascota.TipoId);
            ViewData["EstadoId"] = new SelectList(_context.EstadosMascotas, "EstadoId", "Descripcion", mascota.EstadoId);
            return View(mascota);
        }

        // GET: Mascotas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mascotas == null)
            {
                return NotFound();
            }

            var mascota = await _context.Mascotas.FindAsync(id);
            if (mascota == null)
            {
                return NotFound();
            }
            ViewData["RazaId"] = new SelectList(_context.RazasMascotas, "RazaId", "Descripcion");
            ViewData["TipoId"] = new SelectList(_context.TiposMascotas, "TipoId", "Descripcion");
            ViewData["EstadoId"] = new SelectList(_context.EstadosMascotas, "EstadoId", "Descripcion");
            return View(mascota);
        }

        // POST: Mascotas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MascotaId,Nombre,TipoId,RazaId,Genero,Edad,Peso,EstadoId,UsuarioPropietarioId,UsuarioCreacionId,UsuarioModificacionId,FechaCreacion")] Mascota mascota)
        {
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
            ViewData["RazaId"] = new SelectList(_context.RazasMascotas, "RazaId", "Descripcion", mascota.RazaId);
            ViewData["TipoId"] = new SelectList(_context.TiposMascotas, "TipoId", "Descripcion", mascota.TipoId);
            ViewData["EstadoId"] = new SelectList(_context.EstadosMascotas, "EstadoId", "Descripcion", mascota.EstadoId);
            return View(mascota);
        }

        // GET: Mascotas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mascotas == null)
            {
                return NotFound();
            }

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

        // POST: Mascotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mascotas == null)
            {
                return Problem("Entity set 'VeterinariaContext.Mascotas'  is null.");
            }
            var mascota = await _context.Mascotas.FindAsync(id);
            var estado = await _context.EstadosMascotas.FirstOrDefaultAsync(e => e.Descripcion == "Inactiva");
            if (mascota != null && estado != null)
            {
                mascota.EstadoId = estado.EstadoId;
                _context.Update(mascota);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MascotaExists(int id)
        {
          return (_context.Mascotas?.Any(e => e.MascotaId == id)).GetValueOrDefault();
        }
    }
}
