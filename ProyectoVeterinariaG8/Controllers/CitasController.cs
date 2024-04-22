using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoVeterinariaG8.DAL;

namespace ProyectoVeterinariaG8.Controllers
{
    public class CitasController : Controller
    {
        private readonly VeterinariaContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CitasController(VeterinariaContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Citas
        public async Task<IActionResult> Index()
        {
            var veterinariaContext = _context.Citas.Include(c => c.EstadoCita).Include(c => c.Mascota).Include(c => c.Medicamento).Include(c => c.PrimerVeterinario).Include(c => c.SegundoVeterinario);

            var fechaActual = DateTime.Now;
            var fechaLimite = fechaActual.AddHours(1);

            var citasEnCurso = await _context.Citas
                .Where(c => c.FechayHora <= fechaActual && c.FechayHora <= fechaLimite && c.EstadoCita.DescripcionCita == "Agendada")
                .ToListAsync();

            var estadoEnCurso = await _context.EstadosCita
                .Where(e => e.DescripcionCita == "En Curso")
                .FirstOrDefaultAsync();

            foreach (var cita in citasEnCurso)
            {
                cita.EstadoCitaId = estadoEnCurso.EstadoCitaId;
            }

            await _context.SaveChangesAsync();


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
                .Include(c => c.PrimerVeterinario)
                .Include(c => c.SegundoVeterinario)
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
            ViewData["PrimerVeterinarioId"] = new SelectList(_userManager.Users.Where(u => u.EstadoUsuario.Descripcion == "Activo"), "Id", "Nombre");
            ViewData["SegundoVeterinarioId"] = new SelectList(_userManager.Users.Where(u => u.EstadoUsuario.Descripcion == "Activo"), "Id", "Nombre");
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
            //Validar que no se aparte una cita Sabado o Domingo
            if (cita.FechayHora.DayOfWeek == DayOfWeek.Sunday)
            {
                ModelState.AddModelError("FechayHora", "La fecha seleccionada no puede ser");
            }
            //Validar que no se aparte una cita entre las 7am y las 6pm
            int horaSeleccionada = cita.FechayHora.Hour;
            if (horaSeleccionada < 7 || horaSeleccionada >= 18)
            {
                ModelState.AddModelError("FechayHora", "La hora seleccionada debe estar entre las 7am y las 6pm");
            }

            //Restriccion Veterinario 1 cita fecha
            if (_context.Citas.Any(c => c.PrimerVeterinarioId == cita.PrimerVeterinarioId && c.FechayHora == cita.FechayHora))
            {
                ModelState.AddModelError("PrimerVeterinarioId", "El veterinario ya tiene una cita asignada en la misma fecha y hora");
            }

            //Restriccion Veterinario 2 cita fecha
            if (_context.Citas.Any(c => c.SegundoVeterinarioId == cita.SegundoVeterinarioId && c.FechayHora == cita.FechayHora))
            {
                ModelState.AddModelError("SegundoVeterinarioId", "El veterinario ya tiene una cita asignada en la misma fecha y hora");
            }

            //Verificar si el veterinario 1 esta activo
            var estadoUsuario = await _context.EstadosUsuario.Where(e => e.Descripcion == "Inactivo").FirstOrDefaultAsync();
            var primerVeterinario = await _userManager.FindByIdAsync(cita.PrimerVeterinarioId);
            if (primerVeterinario == null || primerVeterinario.EstadoUsuarioId == estadoUsuario.EstadoId)
            {
                ModelState.AddModelError("PrimerVeterinarioId", "El primer veterinario seleccionado está inactivo");
            }

            //Verificar si el veterinario 2 esta activo
            var segundoVeterinario = await _userManager.FindByIdAsync(cita.SegundoVeterinarioId);
            if (segundoVeterinario == null || segundoVeterinario.EstadoUsuarioId == estadoUsuario.EstadoId)
            {
                ModelState.AddModelError("SegundoVeterinarioId", "El Segundo veterinario seleccionado está inactivo");
            }

            //Verificar que el veterinario 1 y veterinario 2 son iguales
            if (cita.PrimerVeterinarioId == cita.SegundoVeterinarioId)
            {
                ModelState.AddModelError("SegundoVeterinarioId", "El segundo veterinario debe ser diferente al primer veterinario");
            }
            if (ModelState.IsValid)
            {
                _context.Add(cita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstadoCitaId"] = new SelectList(_context.EstadosCita, "EstadoCitaId", "DescripcionCita", cita.EstadoCitaId);
            ViewData["MascotaId"] = new SelectList(_context.Mascotas, "MascotaId", "Nombre", cita.MascotaId);
            ViewData["PrimerVeterinarioId"] = new SelectList(_userManager.Users, "Id", "Nombre");
            ViewData["SegundoVeterinarioId"] = new SelectList(_userManager.Users, "Id", "Nombre");
            ViewData["PrimerVeterinario"] = new SelectList(_userManager.Users, "Id", "Nombre", cita.PrimerVeterinarioId);
            ViewData["SegundoVeterinario"] = new SelectList(_userManager.Users, "Id", "Nombre", cita.SegundoVeterinarioId);
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
            ViewData["PrimerVeterinario"] = new SelectList(_userManager.Users, "Id", "Nombre", cita.PrimerVeterinarioId);
            ViewData["SegundoVeterinario"] = new SelectList(_userManager.Users, "Id", "Nombre", cita.SegundoVeterinarioId);
            ViewData["MedicamentoId"] = new SelectList(_context.Medicamentos, "MedicamentoId", "Nombre", cita.MedicamentoId);
            return View(cita);
        }

        // POST: Citas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CitaId,MascotaId,FechayHora,PrimerVeterinarioId,SegundoVeterinarioId,DescripcionCita,DiagnosticoCita,MedicamentoId,EstadoCitaId")] Cita cita)
        {
            if (id != cita.CitaId)
            {
                return NotFound();
            }

            //Validar que no se aparte una cita Sabado o Domingo
            if (cita.FechayHora.DayOfWeek == DayOfWeek.Sunday)
            {
                ModelState.AddModelError("FechayHora", "La fecha seleccionada no puede ser");
            }
            //Validar que no se aparte una cita entre las 7am y las 6pm
            int horaSeleccionada = cita.FechayHora.Hour;
            if (horaSeleccionada < 7 || horaSeleccionada >= 18)
            {
                ModelState.AddModelError("FechayHora", "La hora seleccionada debe estar entre las 7am y las 6pm");
            }

            //Restriccion Veterinario 1 cita fecha
            if (_context.Citas.Any(c => c.PrimerVeterinarioId == cita.PrimerVeterinarioId && c.FechayHora == cita.FechayHora))
            {
                ModelState.AddModelError("PrimerVeterinarioId", "El veterinario ya tiene una cita asignada en la misma fecha y hora");
            }


            //Restriccion Veterinario 2 cita fecha
            if (_context.Citas.Any(c => c.SegundoVeterinarioId == cita.SegundoVeterinarioId && c.FechayHora == cita.FechayHora))
            {
                ModelState.AddModelError("SegundoVeterinarioId", "El veterinario ya tiene una cita asignada en la misma fecha y hora");
            }

            //Verificar si el veterinario 1 esta activo
            var estadoUsuario = await _context.EstadosUsuario.Where(e => e.Descripcion == "Inactivo").FirstOrDefaultAsync();
            var primerVeterinario = await _userManager.FindByIdAsync(cita.PrimerVeterinarioId);
            if (primerVeterinario == null || primerVeterinario.EstadoUsuarioId == estadoUsuario.EstadoId)
            {
                ModelState.AddModelError("PrimerVeterinarioId", "El primer veterinario seleccionado está inactivo");
            }

            //Verificar si el veterinario 2 esta activo
            var segundoVeterinario = await _userManager.FindByIdAsync(cita.SegundoVeterinarioId);
            if (segundoVeterinario == null || segundoVeterinario.EstadoUsuarioId == estadoUsuario.EstadoId)
            {
                ModelState.AddModelError("SegundoVeterinarioId", "El Segundo veterinario seleccionado está inactivo");
            }

            //Verificar que el veterinario 1 y veterinario 2 son iguales
            if (cita.PrimerVeterinarioId == cita.SegundoVeterinarioId)
            {
                ModelState.AddModelError("SegundoVeterinarioId", "El segundo veterinario debe ser diferente al primer veterinario");
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
            ViewData["PrimerVeterinarioId"] = new SelectList(_userManager.Users, "Id", "Nombre");
            ViewData["SegundoVeterinarioId"] = new SelectList(_userManager.Users, "Id", "Nombre");
            ViewData["PrimerVeterinario"] = new SelectList(_userManager.Users, "Id", "Nombre", cita.PrimerVeterinarioId);
            ViewData["SegundoVeterinario"] = new SelectList(_userManager.Users, "Id", "Nombre", cita.SegundoVeterinarioId);
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
