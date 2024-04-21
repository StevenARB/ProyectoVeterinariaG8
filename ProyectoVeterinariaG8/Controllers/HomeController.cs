using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoVeterinariaG8.DAL;
using ProyectoVeterinariaG8.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace ProyectoVeterinariaG8.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly VeterinariaContext _context;

        public HomeController(ILogger<HomeController> logger, VeterinariaContext context, UserManager <ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Veterinario,Administrador")]
        public async Task<ActionResult> HomeVeterinarioAsync()
        {
            var fechaActual = DateTime.Now;

            String usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (usuarioId != null)
            {

                var citas = await _context.Citas
                    .Include(c => c.EstadoCita)
                    .Include(c => c.Mascota)
                    .Include(c => c.Medicamento)
                    .Include(c => c.PrimerVeterinario)
                    .Include(c => c.SegundoVeterinario)
                    .ToListAsync();


                var citasPasadasPrimario = citas.Where(c => c.FechayHora < fechaActual && c.PrimerVeterinarioId == usuarioId).ToList();
                var citasFuturasPrimario = citas.Where(c => c.FechayHora > fechaActual && c.PrimerVeterinarioId == usuarioId).ToList();

                ViewBag.HistorialPrimero = citasPasadasPrimario;
                ViewBag.ProximasPrimero = citasFuturasPrimario;

                var citasPasadasSecundario = citas.Where(c => c.FechayHora < fechaActual && c.SegundoVeterinarioId == usuarioId).ToList();
                var citasFuturasSecundario = citas.Where(c => c.FechayHora > fechaActual && c.SegundoVeterinarioId == usuarioId).ToList();

                ViewBag.HistorialSecundario = citasPasadasSecundario;
                ViewBag.ProximasSecundario = citasFuturasSecundario;
            }
            return View();
        }

        [Authorize(Roles = "Cliente,Administrador")]
        public async Task<ActionResult> HomeClienteAsync()
        {

            String usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (usuarioId != null)
            {
                // Obtiene las citas relacionadas con el usuario logueado
                var citas = await _context.Citas
                    .Include(c => c.EstadoCita)
                    .Include(c => c.Mascota)
                    .Include(c => c.Medicamento)
                    .Include(c => c.PrimerVeterinario)
                    .Include(c => c.SegundoVeterinario)
                    .Where(c => c.Mascota.UsuarioPropietarioId == usuarioId)
                    .ToListAsync();

                // Separa las citas pasadas y futuras
                var fechaActual = DateTime.Now;
                var citasPasadasMascota = citas.Where(c => c.FechayHora < fechaActual).ToList();
                var citasProximasMascota = citas.Where(c => c.FechayHora > fechaActual).ToList();

                // Pasa las citas a la vista
                ViewBag.Historial = citasPasadasMascota;
                ViewBag.Proximas = citasProximasMascota;
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
