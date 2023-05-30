using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SisArrendamiento.Models;

namespace SisArrendamiento.Controllers
{
    public class CuartoController : Controller
    {
        private readonly ArrendamientoWebContext _context;

        public CuartoController(ArrendamientoWebContext context)
        {
            _context = context;
        }

        // GET: Cuarto
        public async Task<IActionResult> Index()
        {
              return View(await _context.Cuartos.ToListAsync());
        }

        // GET: Cuarto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cuartos == null)
            {
                return NotFound();
            }

            var cuarto = await _context.Cuartos
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (cuarto == null)
            {
                return NotFound();
            }

            return View(cuarto);
        }

        // GET: Cuarto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cuarto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Piso,Zona")] Cuarto cuarto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cuarto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cuarto);
        }

        // GET: Cuarto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cuartos == null)
            {
                return NotFound();
            }

            var cuarto = await _context.Cuartos.FindAsync(id);
            if (cuarto == null)
            {
                return NotFound();
            }
            return View(cuarto);
        }

        // POST: Cuarto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Piso,Zona")] Cuarto cuarto)
        {
            if (id != cuarto.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cuarto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CuartoExists(cuarto.Codigo))
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
            return View(cuarto);
        }

        // GET: Cuarto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cuartos == null)
            {
                return NotFound();
            }

            var cuarto = await _context.Cuartos
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (cuarto == null)
            {
                return NotFound();
            }

            return View(cuarto);
        }

        // POST: Cuarto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cuartos == null)
            {
                return Problem("Entity set 'ArrendamientoWebContext.Cuartos'  is null.");
            }
            var cuarto = await _context.Cuartos.FindAsync(id);
            if (cuarto != null)
            {
                _context.Cuartos.Remove(cuarto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CuartoExists(int id)
        {
          return _context.Cuartos.Any(e => e.Codigo == id);
        }
    }
}
