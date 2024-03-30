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
    public class TipoMascotasController : Controller
    {
        private readonly VeterinariaContext _context;

        public TipoMascotasController(VeterinariaContext context)
        {
            _context = context;
        }

        // GET: TipoMascotas
        public async Task<IActionResult> Index()
        {
              return _context.TiposMascotas != null ? 
                          View(await _context.TiposMascotas.ToListAsync()) :
                          Problem("Entity set 'VeterinariaContext.TiposMascotas'  is null.");
        }

        // GET: TipoMascotas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TiposMascotas == null)
            {
                return NotFound();
            }

            var tipoMascota = await _context.TiposMascotas
                .FirstOrDefaultAsync(m => m.TipoId == id);
            if (tipoMascota == null)
            {
                return NotFound();
            }

            return View(tipoMascota);
        }

        // GET: TipoMascotas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoMascotas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoId,Descripcion")] TipoMascota tipoMascota)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoMascota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoMascota);
        }

        // GET: TipoMascotas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TiposMascotas == null)
            {
                return NotFound();
            }

            var tipoMascota = await _context.TiposMascotas.FindAsync(id);
            if (tipoMascota == null)
            {
                return NotFound();
            }
            return View(tipoMascota);
        }

        // POST: TipoMascotas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoId,Descripcion")] TipoMascota tipoMascota)
        {
            if (id != tipoMascota.TipoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoMascota);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoMascotaExists(tipoMascota.TipoId))
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
            return View(tipoMascota);
        }

        // GET: TipoMascotas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TiposMascotas == null)
            {
                return NotFound();
            }

            var tipoMascota = await _context.TiposMascotas
                .FirstOrDefaultAsync(m => m.TipoId == id);
            if (tipoMascota == null)
            {
                return NotFound();
            }

            return View(tipoMascota);
        }

        // POST: TipoMascotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TiposMascotas == null)
            {
                return Problem("Entity set 'VeterinariaContext.TiposMascotas'  is null.");
            }
            var tipoMascota = await _context.TiposMascotas.FindAsync(id);
            if (tipoMascota != null)
            {
                _context.TiposMascotas.Remove(tipoMascota);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoMascotaExists(int id)
        {
          return (_context.TiposMascotas?.Any(e => e.TipoId == id)).GetValueOrDefault();
        }
    }
}
