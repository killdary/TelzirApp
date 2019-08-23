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

        public JsonResult CalcularValor(int DdOrigem, int DdDestino, int Tempo, int PacoteId){

            CalculoTarifa calculo = new CalculoTarifa();
            calculo.Status = 0;
            
            var tarifa = _context.Tarifa.FirstOrDefault(t => t.DdOrigem == DdOrigem &&
                                                                    t.DdDestino == DdDestino);

            if(tarifa != null)
            {
                var pacote = _context.Pacote
                                     .Include(pac => pac.Plano)
                                     .FirstOrDefault(pac => pac.Id == PacoteId);

                calculo.Status = 2;
                calculo.MinutosConsumidos = Tempo;
                calculo.MinutosPacote = pacote.Minutos;
                calculo.Porcentagem = pacote.Plano.Porcentagem;
                calculo.Valor = tarifa.Valor;
                var x = calculo.ValorTarifa() ;
                            
            }else{
                calculo.Status = 1;
            }

            return Json(calculo.ValorTarifa());
        }
    }
}
