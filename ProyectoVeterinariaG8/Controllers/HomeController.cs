using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoVeterinariaG8.DAL;
using ProyectoVeterinariaG8.Models;
using System.Diagnostics;

namespace ProyectoVeterinariaG8.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly VeterinariaContext _context;

        public HomeController(ILogger<HomeController> logger, VeterinariaContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Veterinario,Administrador")]
        public ActionResult HomeVeterinario()
        {
            var fechaActual = DateTime.Now;
            var citas = _context.Citas
                .Include(c => c.EstadoCita)
                .Include(c => c.Mascota)
                .Include(c => c.Medicamento)
                .Include(c => c.PrimerVeterinario)
                .Include(c => c.SegundoVeterinario);

            var citasPasadas = citas.Where(c => c.FechayHora < fechaActual).ToList();
            var citasFuturas = citas.Where(c => c.FechayHora > fechaActual).ToList();

            ViewBag.Historial = citasPasadas;
            ViewBag.Proximas = citasFuturas;

            return View();
        }

        [Authorize(Roles = "Cliente,Administrador")]
        public ActionResult HomeCliente()
        {
            var fechaActual = DateTime.Now;
            var citas = _context.Citas
                .Include(c => c.EstadoCita)
                .Include(c => c.Mascota)
                .Include(c => c.Medicamento)
                .Include(c => c.PrimerVeterinario)
                .Include(c => c.SegundoVeterinario);

            var citasPasadas = citas.Where(c => c.FechayHora < fechaActual).ToList();
            var citasFuturas = citas.Where(c => c.FechayHora > fechaActual).ToList();

            ViewBag.Historial = citasPasadas;
            ViewBag.Proximas = citasFuturas;

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
