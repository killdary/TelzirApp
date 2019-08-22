using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Telzir.Models;
using Telzir.Negocio;
using Microsoft.EntityFrameworkCore;

namespace Telzir.Controllers
{
    public class HomeController : Controller
    {

        private readonly TelzirContext _context;

        public HomeController(TelzirContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var listaPlanos = _context.Plano.ToList();
            ViewBag.Planos = listaPlanos;
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

        public JsonResult CalcularValor(CalculoTarifa calculo){

            return Json(calculo.ValorTarifa());
        }
    }
}
