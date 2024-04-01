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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public ActionResult HomeVeterinario()
        {
            var fechaActual = DateTime.Now;
            List<Cita> citas;

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
