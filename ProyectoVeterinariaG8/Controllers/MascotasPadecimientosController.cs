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
    public class MascotasPadecimientosController : Controller
    {
        private readonly VeterinariaContext _context;

        public MascotasPadecimientosController(VeterinariaContext context)
        {
            _context = context;
        }

        // GET: MascotasPadecimientos
        public async Task<IActionResult> Index()
        {
            var veterinariaContext = _context.MascotasPadecimientos.Include(m => m.Mascota);
            return View(await veterinariaContext.ToListAsync());
        }

        // GET: MascotasPadecimientos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MascotasPadecimientos == null)
            {
                return NotFound();
            }

            var mascotaPadecimiento = await _context.MascotasPadecimientos
                .Include(m => m.Mascota)
                .FirstOrDefaultAsync(m => m.PadecimientoId == id);
            if (mascotaPadecimiento == null)
            {
                return NotFound();
            }

            return View(mascotaPadecimiento);
        }

        // GET: MascotasPadecimientos/Create
        public IActionResult Create()
        {
            ViewData["MascotaId"] = new SelectList(_context.Mascotas, "MascotaId", "MascotaId");
            return View();
        }

        // POST: MascotasPadecimientos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PadecimientoId,MascotaId,Descripcion")] MascotaPadecimiento mascotaPadecimiento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mascotaPadecimiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MascotaId"] = new SelectList(_context.Mascotas, "MascotaId", "MascotaId", mascotaPadecimiento.MascotaId);
            return View(mascotaPadecimiento);
        }

        // GET: MascotasPadecimientos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MascotasPadecimientos == null)
            {
                return NotFound();
            }

            var mascotaPadecimiento = await _context.MascotasPadecimientos.FindAsync(id);
            if (mascotaPadecimiento == null)
            {
                return NotFound();
            }
            ViewData["MascotaId"] = new SelectList(_context.Mascotas, "MascotaId", "MascotaId", mascotaPadecimiento.MascotaId);
            return View(mascotaPadecimiento);
        }

        // POST: MascotasPadecimientos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PadecimientoId,MascotaId,Descripcion")] MascotaPadecimiento mascotaPadecimiento)
        {
            if (id != mascotaPadecimiento.PadecimientoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mascotaPadecimiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MascotaPadecimientoExists(mascotaPadecimiento.PadecimientoId))
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
            ViewData["MascotaId"] = new SelectList(_context.Mascotas, "MascotaId", "MascotaId", mascotaPadecimiento.MascotaId);
            return View(mascotaPadecimiento);
        }

        // GET: MascotasPadecimientos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MascotasPadecimientos == null)
            {
                return NotFound();
            }

            var mascotaPadecimiento = await _context.MascotasPadecimientos
                .Include(m => m.Mascota)
                .FirstOrDefaultAsync(m => m.PadecimientoId == id);
            if (mascotaPadecimiento == null)
            {
                return NotFound();
            }

            return View(mascotaPadecimiento);
        }

        // POST: MascotasPadecimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MascotasPadecimientos == null)
            {
                return Problem("Entity set 'VeterinariaContext.MascotasPadecimientos'  is null.");
            }
            var mascotaPadecimiento = await _context.MascotasPadecimientos.FindAsync(id);
            if (mascotaPadecimiento != null)
            {
                _context.MascotasPadecimientos.Remove(mascotaPadecimiento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MascotaPadecimientoExists(int id)
        {
          return (_context.MascotasPadecimientos?.Any(e => e.PadecimientoId == id)).GetValueOrDefault();
        }
    }
}
