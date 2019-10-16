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
    public class PacotesController : Controller
    {
        private readonly TelzirContext _context;

        public PacotesController(TelzirContext context)
        {
            _context = context;
        }

        // GET: Pacotes
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Planos");
            // return View(await _context.Pacote.ToListAsync());
        }

        // GET: Pacotes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacote = await _context.Pacote
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pacote == null)
            {
                return NotFound();
            }

            return View(pacote);
        }

        // GET: Pacotes/Create
        public IActionResult Create(int? PlanoId)
        {
            if(PlanoId == null){
                TempData["codeError"] = 404;
                TempData["Mensagem"] = "Um pacote só pode ser cadastrado em um plano"; 
                return RedirectToAction("Index", "Planos");
            
            }

            var plano = _context.Plano.FirstOrDefault(p => p.Id == PlanoId);

            if (plano == null)
            {
                TempData["codeError"] = 404;
                TempData["Mensagem"] = "Um pacote só pode ser cadastrado em um plano"; 
                return RedirectToAction("Index", "Planos");
            }

            ViewData["PlanoId"] = PlanoId;
            return View();
        }

        // POST: Pacotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? PlanoId, [Bind("Id,Descricao,Minutos,Tipo")] Pacote pacote)
        {
            if (ModelState.IsValid)
            {
                var plano = await _context.Plano.FirstOrDefaultAsync(p => p.Id == PlanoId);

                if (plano == null)
                {
                    TempData["codeError"] = 404;
                    TempData["Mensagem"] = "Um pacote só pode ser cadastrado em um plano"; 
                    return RedirectToAction("Index", "Planos");
                }

                pacote.Plano = plano;
                _context.Add(pacote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pacote);
        }

        // GET: Pacotes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacote = await _context.Pacote
                                        .FirstOrDefaultAsync(pac => pac.Id == id);
            if (pacote == null)
            {
                return NotFound();
            }
            return View(pacote);
        }

        // POST: Pacotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao,Minutos,Tipo")] Pacote pacote)
        {
            if (id != pacote.Id)
            {
                return NotFound();
            }

            
            var planoPacote = await _context.Pacote.AsNoTracking()
                                .Include(pac => pac.Plano)
                                .FirstOrDefaultAsync(pac => pac.Id == id);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pacote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacoteExists(pacote.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                

                return RedirectToAction("Details", "Planos", new {@id=planoPacote.Plano.Id});
                // return RedirectToAction(nameof(Index));
            }
            return View(pacote);
        }

        // GET: Pacotes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacote = await _context.Pacote
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pacote == null)
            {
                return NotFound();
            }

            return View(pacote);
        }

        // POST: Pacotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pacote = await _context.Pacote.FindAsync(id);
            _context.Pacote.Remove(pacote);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacoteExists(int id)
        {
            return _context.Pacote.Any(e => e.Id == id);
        }
    }
}
