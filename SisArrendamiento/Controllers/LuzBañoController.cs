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
    public class LuzBañoController : Controller
    {
        private readonly ArrendamientoWebContext _context;

        public LuzBañoController(ArrendamientoWebContext context)
        {
            _context = context;
        }

        // GET: LuzBaño
        public async Task<IActionResult> Index()
        {
              return _context.LuzBaños != null ? 
                          View(await _context.LuzBaños.ToListAsync()) :
                          Problem("Entity set 'ArrendamientoWebContext.LuzBaños'  is null.");
        }

        // GET: LuzBaño/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LuzBaños == null)
            {
                return NotFound();
            }

            var luzBaño = await _context.LuzBaños
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (luzBaño == null)
            {
                return NotFound();
            }

            return View(luzBaño);
        }

        // GET: LuzBaño/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LuzBaño/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,AnteriorLuzLectura,ActualLuzLectura,KwConsumido,Monto")] LuzBaño luzBaño)
        {
            if (ModelState.IsValid)
            {
                _context.Add(luzBaño);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(luzBaño);
        }

        // GET: LuzBaño/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LuzBaños == null)
            {
                return NotFound();
            }

            var luzBaño = await _context.LuzBaños.FindAsync(id);
            if (luzBaño == null)
            {
                return NotFound();
            }
            return View(luzBaño);
        }

        // POST: LuzBaño/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,AnteriorLuzLectura,ActualLuzLectura,KwConsumido,Monto")] LuzBaño luzBaño)
        {
            if (id != luzBaño.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(luzBaño);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LuzBañoExists(luzBaño.Codigo))
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
            return View(luzBaño);
        }

        // GET: LuzBaño/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LuzBaños == null)
            {
                return NotFound();
            }

            var luzBaño = await _context.LuzBaños
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (luzBaño == null)
            {
                return NotFound();
            }

            return View(luzBaño);
        }

        // POST: LuzBaño/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LuzBaños == null)
            {
                return Problem("Entity set 'ArrendamientoWebContext.LuzBaños'  is null.");
            }
            var luzBaño = await _context.LuzBaños.FindAsync(id);
            if (luzBaño != null)
            {
                _context.LuzBaños.Remove(luzBaño);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LuzBañoExists(int id)
        {
          return (_context.LuzBaños?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
