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
    public class CitasController : Controller
    {
        private readonly VeterinariaContext _context;

        public CitasController(VeterinariaContext context)
        {
            _context = context;
        }

        // GET: Citas
        public async Task<IActionResult> Index()
        {
            var veterinariaContext = _context.Citas.Include(c => c.EstadoCita).Include(c => c.Mascota).Include(c => c.Medicamento);
            return View(await veterinariaContext.ToListAsync());
        }

        // GET: Citas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Citas == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas
                .Include(c => c.EstadoCita)
                .Include(c => c.Mascota)
                .Include(c => c.Medicamento)
                .FirstOrDefaultAsync(m => m.CitaId == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        // GET: Citas/Create
        public IActionResult Create()
        {
            ViewData["EstadoCitaId"] = new SelectList(_context.EstadosCita, "EstadoCitaId", "DescripcionCita");
            ViewData["MascotaId"] = new SelectList(_context.Mascotas, "MascotaId", "Nombre");
            ViewData["PrimerVeterinarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "Nombre");
            ViewData["SegundoVeterinarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "Nombre");
            ViewData["MedicamentoId"] = new SelectList(_context.Medicamentos, "MedicamentoId", "Nombre");
            return View();
        }

        // POST: Citas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CitaId,MascotaId,FechayHora,PrimerVeterinarioId,SegundoVeterinarioId,DescripcionCita,DiagnosticoCita,MedicamentoId,EstadoCitaId")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstadoCitaId"] = new SelectList(_context.EstadosCita, "EstadoCitaId", "DescripcionCita", cita.EstadoCitaId);
            ViewData["MascotaId"] = new SelectList(_context.Mascotas, "MascotaId", "Nombre", cita.MascotaId);
            ViewData["PrimerVeterinario"] = new SelectList(_context.Usuarios, "UsuarioId", "Nombre", cita.PrimerVeterinarioId);
            ViewData["SegundoVeterinario"] = new SelectList(_context.Usuarios, "UsuarioId", "Nombre", cita.SegundoVeterinarioId);
            ViewData["MedicamentoId"] = new SelectList(_context.Medicamentos, "MedicamentoId", "Nombre", cita.MedicamentoId);
            return View(cita);
        }

        // GET: Citas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Citas == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas.FindAsync(id);
            if (cita == null)
            {
                return NotFound();
            }
            ViewData["EstadoCitaId"] = new SelectList(_context.EstadosCita, "EstadoCitaId", "DescripcionCita", cita.EstadoCitaId);
            ViewData["MascotaId"] = new SelectList(_context.Mascotas, "MascotaId", "Nombre", cita.MascotaId);
            ViewData["PrimerVeterinario"] = new SelectList(_context.Usuarios, "UsuarioId", "Nombre", cita.PrimerVeterinarioId);
            ViewData["SegundoVeterinario"] = new SelectList(_context.Usuarios, "UsuarioId", "Nombre", cita.SegundoVeterinarioId);
            ViewData["MedicamentoId"] = new SelectList(_context.Medicamentos, "MedicamentoId", "Nombre", cita.MedicamentoId);
            return View(cita);
        }

        // POST: Citas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CitaId,MascotaId,FechayHora,PrimerVeterinarioId,SegundoVeterinario,DescripcionCita,DiagnosticoCita,MedicamentoId,EstadoCitaId")] Cita cita)
        {
            if (id != cita.CitaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitaExists(cita.CitaId))
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
            ViewData["EstadoCitaId"] = new SelectList(_context.EstadosCita, "EstadoCitaId", "DescripcionCita", cita.EstadoCitaId);
            ViewData["MascotaId"] = new SelectList(_context.Mascotas, "MascotaId", "Nombre", cita.MascotaId);
            ViewData["PrimerVeterinario"] = new SelectList(_context.Usuarios, "UsuarioId", "Nombre", cita.PrimerVeterinarioId);
            ViewData["SegundoVeterinario"] = new SelectList(_context.Usuarios, "UsuarioId", "Nombre", cita.SegundoVeterinarioId);
            ViewData["MedicamentoId"] = new SelectList(_context.Medicamentos, "MedicamentoId", "Nombre", cita.MedicamentoId);
            return View(cita);
        }

        // GET: Citas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Citas == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas
                .Include(c => c.EstadoCita)
                .Include(c => c.Mascota)
                .Include(c => c.Medicamento)
                .FirstOrDefaultAsync(m => m.CitaId == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Citas == null)
            {
                return Problem("Entity set 'VeterinariaContext.Citas'  is null.");
            }
            var cita = await _context.Citas.FindAsync(id);
            if (cita != null)
            {
                _context.Citas.Remove(cita);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitaExists(int id)
        {
            return (_context.Citas?.Any(e => e.CitaId == id)).GetValueOrDefault();
        }
    }
}
