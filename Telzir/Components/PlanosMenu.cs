using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Telzir.Models;

namespace Telzir.Components
{
    public class PlanosMenu : ViewComponent
    {
        private readonly TelzirContext _context;

        public PlanosMenu(TelzirContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await GetItemsAsync();
            return View(items);
        }

        private Task<List<Plano>> GetItemsAsync() => _context.Plano.ToListAsync();

        // private Task<List<Plano>> GetItemsAsync()
        // {
        //     return _context.Plano.ToListAsync();
        // }

    }
}