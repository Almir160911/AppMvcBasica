using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppMvcBasica.Data;
using AppMvcBasica.Models;
using Microsoft.AspNetCore.Authorization;

namespace AppMvcBasica.Controllers
{
    [Authorize]
    public class EnderecosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnderecosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Enderecos.Include(p => p.Cliente);
            return View(await applicationDbContext.ToListAsync());
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Endereco = await _context.Enderecos
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Endereco == null)
            {
                return NotFound();
            }

            return View(Endereco);
        }

        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Endereco Endereco)
        {
            if (ModelState.IsValid)
            {
                Endereco.Id = Guid.NewGuid();
                _context.Add(Endereco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome", Endereco.ClienteId);
            return View(Endereco);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Endereco = await _context.Enderecos.FindAsync(id);
            if (Endereco == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome", Endereco.ClienteId);
            return View(Endereco);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Endereco Endereco)
        {
            if (id != Endereco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Endereco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnderecoExists(Endereco.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome", Endereco.ClienteId);
            return View(Endereco);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Endereco = await _context.Enderecos
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Endereco == null)
            {
                return NotFound();
            }

            return View(Endereco);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var Endereco = await _context.Enderecos.FindAsync(id);
            _context.Enderecos.Remove(Endereco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnderecoExists(Guid id)
        {
            return _context.Enderecos.Any(e => e.Id == id);
        }
    }
}
