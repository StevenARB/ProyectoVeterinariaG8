using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoVeterinariaG8.DAL;

namespace ProyectoVeterinariaG8.Controllers
{
    public class RazaMascotasController : Controller
    {
        private readonly VeterinariaContext _context;

        public RazaMascotasController(VeterinariaContext context)
        {
            _context = context;
        }

        // GET: RazaMascotas
        public async Task<IActionResult> Index()
        {
            var veterinariaContext = _context.RazasMascotas.Include(r => r.TipoMascota);
            return View(await veterinariaContext.ToListAsync());
        }

        // GET: RazaMascotas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RazasMascotas == null)
            {
                return NotFound();
            }

            var razaMascota = await _context.RazasMascotas
                .Include(r => r.TipoMascota)
                .FirstOrDefaultAsync(m => m.RazaId == id);
            if (razaMascota == null)
            {
                return NotFound();
            }

            return View(razaMascota);
        }

        // GET: RazaMascotas/Create
        public IActionResult Create()
        {
            ViewData["TipoId"] = new SelectList(_context.TiposMascotas, "TipoId", "Descripcion");
            return View();
        }

        // POST: RazaMascotas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RazaId,TipoId,Descripcion")] RazaMascota razaMascota)
        {
            if (ModelState.IsValid)
            {
                _context.Add(razaMascota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoId"] = new SelectList(_context.TiposMascotas, "TipoId", "Descripcion", razaMascota.TipoId);
            return View(razaMascota);
        }

        // GET: RazaMascotas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RazasMascotas == null)
            {
                return NotFound();
            }

            var razaMascota = await _context.RazasMascotas.FindAsync(id);
            if (razaMascota == null)
            {
                return NotFound();
            }
            ViewData["TipoId"] = new SelectList(_context.TiposMascotas, "TipoId", "Descripcion", razaMascota.TipoId);
            return View(razaMascota);
        }

        // POST: RazaMascotas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RazaId,TipoId,Descripcion")] RazaMascota razaMascota)
        {
            if (id != razaMascota.RazaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(razaMascota);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RazaMascotaExists(razaMascota.RazaId))
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
            ViewData["TipoId"] = new SelectList(_context.TiposMascotas, "TipoId", "Descripcion", razaMascota.TipoId);
            return View(razaMascota);
        }

        // GET: RazaMascotas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RazasMascotas == null)
            {
                return NotFound();
            }

            var razaMascota = await _context.RazasMascotas
                .Include(r => r.TipoMascota)
                .FirstOrDefaultAsync(m => m.RazaId == id);
            if (razaMascota == null)
            {
                return NotFound();
            }

            return View(razaMascota);
        }

        // POST: RazaMascotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RazasMascotas == null)
            {
                return Problem("Entity set 'VeterinariaContext.RazasMascotas'  is null.");
            }
            var razaMascota = await _context.RazasMascotas.FindAsync(id);
            if (razaMascota != null)
            {
                _context.RazasMascotas.Remove(razaMascota);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RazaMascotaExists(int id)
        {
          return (_context.RazasMascotas?.Any(e => e.RazaId == id)).GetValueOrDefault();
        }
    }
}
