using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Telzir.Models;
using Microsoft.AspNetCore.Authorization;

namespace Telzir.Controllers
{
    [Authorize]
    public class PlanosController : Controller
    {
        private readonly TelzirContext _context;

        public PlanosController(TelzirContext context)
        {
            _context = context;
        }

        // GET: Planos
        
        public async Task<IActionResult> Index()
        {
            return View(await _context.Plano.ToListAsync());
        }

        // GET: Planos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["codeError"] = 404;
                TempData["Mensagem"] = "O plano Desejado não foi encontrado. Por favor tente novamente mais tarde.";
                return RedirectToAction("Index", "Planos");
            }

            var plano = await _context.Plano
                .FirstOrDefaultAsync(m => m.Id == id);
            
            plano.Pacotes = await _context.Pacote.Where(p => p.Plano.Id == plano.Id).ToListAsync();

            if (plano == null)
            {
                TempData["codeError"] = 404;
                TempData["Mensagem"] = "O plano Desejado não foi encontrado. Por favor tente novamente mais tarde.";
                return RedirectToAction("Index", "Planos");
            }
            
            TempData["PlanoId"] = plano.Id;
            TempData.Keep("PlanoId");

            

            return View(plano);
        }

        // GET: Planos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Planos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao,Porcentagem,Criado,Editado")] Plano plano)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plano);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(plano);
        }

        // GET: Planos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plano = await _context.Plano.FindAsync(id);
            if (plano == null)
            {
                return NotFound();
            }
            return View(plano);
        }

        // POST: Planos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,Porcentagem,Criado,Editado")] Plano plano)
        {
            if (id != plano.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plano);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanoExists(plano.Id))
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
            return View(plano);
        }

        // GET: Planos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plano = await _context.Plano
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plano == null)
            {
                return NotFound();
            }

            return View(plano);
        }

        // POST: Planos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plano = await _context.Plano.FindAsync(id);
            _context.Plano.Remove(plano);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanoExists(int id)
        {
            return _context.Plano.Any(e => e.Id == id);
        }

        [AllowAnonymous]
        public IActionResult Oferta(int? id)
        {
            PlanoPacotes planoComPacotes = new PlanoPacotes();
            if (id == null)
            {
                TempData["codeError"] = 404;
                TempData["Mensagem"] = "O plano Desejado não foi encontrado. Por favor tente novamente mais tarde.";
                return RedirectToAction("Index", "Home");
            }

            var plano = _context.Plano
                .FirstOrDefault(m => m.Id == id);

            var pacotes = _context.Pacote.Where(p => p.Plano.Id == plano.Id).ToList();
            var tarifas = _context.Tarifa.ToList();

            planoComPacotes.Plano = plano;
            planoComPacotes.Pacotes = pacotes;
            planoComPacotes.Tarifas = tarifas;

            if (plano == null)
            {
                TempData["codeError"] = 404;
                TempData["Mensagem"] = "O plano Desejado não foi encontrado. Por favor tente novamente mais tarde.";
                return RedirectToAction("Index", "Home");
            }
            
            return View(planoComPacotes);
        }
    }
}
