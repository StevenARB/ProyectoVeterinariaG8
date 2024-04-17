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
    public class MascotasVacunasController : Controller
    {
        private readonly VeterinariaContext _context;

        public MascotasVacunasController(VeterinariaContext context)
        {
            _context = context;
        }

        // GET: MascotasVacunas
        public async Task<IActionResult> Index()
        {
            var veterinariaContext = _context.MascotasVacunas.Include(m => m.Mascota).ThenInclude(m => m.EstadoMascota).Where(m => m.Mascota.EstadoMascota.Descripcion == "Activo");
            return View(await veterinariaContext.ToListAsync());
        }

        // GET: MascotasVacunas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MascotasVacunas == null)
            {
                return NotFound();
            }

            var mascotaVacuna = await _context.MascotasVacunas
                .Include(m => m.Mascota)
                .FirstOrDefaultAsync(m => m.VacunaId == id);
            if (mascotaVacuna == null)
            {
                return NotFound();
            }

            return View(mascotaVacuna);
        }

        // GET: MascotasVacunas/Create
        public IActionResult Create()
        {
            ViewData["MascotaId"] = new SelectList(_context.Mascotas, "MascotaId", "Nombre");
            return View();
        }

        // POST: MascotasVacunas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VacunaId,MascotaId,Tipo,Fecha,Producto")] MascotaVacuna mascotaVacuna)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mascotaVacuna);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MascotaId"] = new SelectList(_context.Mascotas, "MascotaId", "Nombre", mascotaVacuna.MascotaId);
            return View(mascotaVacuna);
        }

        // GET: MascotasVacunas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MascotasVacunas == null)
            {
                return NotFound();
            }

            var mascotaVacuna = await _context.MascotasVacunas.FindAsync(id);
            if (mascotaVacuna == null)
            {
                return NotFound();
            }
            ViewData["MascotaId"] = new SelectList(_context.Mascotas, "MascotaId", "Nombre", mascotaVacuna.MascotaId);
            return View(mascotaVacuna);
        }

        // POST: MascotasVacunas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VacunaId,MascotaId,Tipo,Fecha,Producto")] MascotaVacuna mascotaVacuna)
        {
            if (id != mascotaVacuna.VacunaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mascotaVacuna);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MascotaVacunaExists(mascotaVacuna.VacunaId))
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
            ViewData["MascotaId"] = new SelectList(_context.Mascotas, "MascotaId", "Nombre", mascotaVacuna.MascotaId);
            return View(mascotaVacuna);
        }

        // GET: MascotasVacunas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MascotasVacunas == null)
            {
                return NotFound();
            }

            var mascotaVacuna = await _context.MascotasVacunas
                .Include(m => m.Mascota)
                .FirstOrDefaultAsync(m => m.VacunaId == id);
            if (mascotaVacuna == null)
            {
                return NotFound();
            }

            return View(mascotaVacuna);
        }

        // POST: MascotasVacunas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MascotasVacunas == null)
            {
                return Problem("Entity set 'VeterinariaContext.MascotasVacunas'  is null.");
            }
            var mascotaVacuna = await _context.MascotasVacunas.FindAsync(id);
            if (mascotaVacuna != null)
            {
                _context.MascotasVacunas.Remove(mascotaVacuna);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MascotaVacunaExists(int id)
        {
          return (_context.MascotasVacunas?.Any(e => e.VacunaId == id)).GetValueOrDefault();
        }
    }
}
