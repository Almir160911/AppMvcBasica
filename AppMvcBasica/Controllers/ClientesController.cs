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

//... teste github 1

//... teste github 2



namespace AppMvcBasica.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clientes.ToListAsync());
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Cliente == null)
            {
                return NotFound();
            }

            return View(Cliente);
        }

        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClientId", "Logradouro");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente Cliente)
        {
            if (ModelState.IsValid)
            {
                Cliente.Id = Guid.NewGuid();
                _context.Add(Cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Cliente);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Cliente = await _context.Clientes.FindAsync(id);
            if (Cliente == null)
            {
                return NotFound();
            }
            return View(Cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Cliente Cliente)
        {
            if (id != Cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(Cliente.Id))
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
            return View(Cliente);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Cliente == null)
            {
                return NotFound();
            }

            return View(Cliente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var Cliente = await _context.Clientes.FindAsync(id);
            _context.Clientes.Remove(Cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(Guid id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
