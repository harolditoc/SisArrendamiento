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
    public class AlquilerController : Controller
    {
        private readonly ArrendamientoWebContext _context;

        public AlquilerController(ArrendamientoWebContext context)
        {
            _context = context;
        }

        // GET: Alquiler
        public async Task<IActionResult> Index()
        {
            var arrendamientoWebContext = _context.Alquilers.Include(a => a.ArrendadorCodigoNavigation).Include(a => a.ArrendatarioCodigoNavigation).Include(a => a.CuartoCodigoNavigation).Include(a => a.LuzBañoCodigoNavigation).Include(a => a.LuzCuartoCodigoNavigation).Include(a => a.LuzEscaleraCodigoNavigation);
            return View(await arrendamientoWebContext.ToListAsync());
        }

        // GET: Alquiler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Alquilers == null)
            {
                return NotFound();
            }

            var alquiler = await _context.Alquilers
                .Include(a => a.ArrendadorCodigoNavigation)
                .Include(a => a.ArrendatarioCodigoNavigation)
                .Include(a => a.CuartoCodigoNavigation)
                .Include(a => a.LuzBañoCodigoNavigation)
                .Include(a => a.LuzCuartoCodigoNavigation)
                .Include(a => a.LuzEscaleraCodigoNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (alquiler == null)
            {
                return NotFound();
            }

            return View(alquiler);
        }

        // GET: Alquiler/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ArrendadorCodigo"] = new SelectList(_context.Arrendadors, "Codigo", "Codigo");
            ViewData["ArrendatarioCodigo"] = new SelectList(_context.Arrendatarios, "Codigo", "Codigo");
            ViewData["CuartoCodigo"] = new SelectList(_context.Cuartos, "Codigo", "Codigo");
            ViewData["LuzBañoCodigo"] = new SelectList(_context.LuzBaños, "Codigo", "Codigo");
            ViewData["LuzCuartoCodigo"] = new SelectList(_context.LuzCuartos, "Codigo", "Codigo");
            ViewData["LuzEscaleraCodigo"] = new SelectList(_context.LuzEscaleras, "Codigo", "Codigo");
            return View();
        }

        // POST: Alquiler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,AlquilerMensual,Agua,Cable,FechaActual,FechaVencimiento,PagoTotal,ArrendadorCodigo,LuzCuartoCodigo,LuzBañoCodigo,LuzEscaleraCodigo,ArrendatarioCodigo,CuartoCodigo")] Alquiler alquiler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alquiler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArrendadorCodigo"] = new SelectList(_context.Arrendadors, "Codigo", "Codigo", alquiler.ArrendadorCodigo);
            ViewData["ArrendatarioCodigo"] = new SelectList(_context.Arrendatarios, "Codigo", "Codigo", alquiler.ArrendatarioCodigo);
            ViewData["CuartoCodigo"] = new SelectList(_context.Cuartos, "Codigo", "Codigo", alquiler.CuartoCodigo);
            ViewData["LuzBañoCodigo"] = new SelectList(_context.LuzBaños, "Codigo", "Codigo", alquiler.LuzBañoCodigo);
            ViewData["LuzCuartoCodigo"] = new SelectList(_context.LuzCuartos, "Codigo", "Codigo", alquiler.LuzCuartoCodigo);
            ViewData["LuzEscaleraCodigo"] = new SelectList(_context.LuzEscaleras, "Codigo", "Codigo", alquiler.LuzEscaleraCodigo);
            return View(alquiler);
        }

        // GET: Alquiler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Alquilers == null)
            {
                return NotFound();
            }

            var alquiler = await _context.Alquilers.FindAsync(id);
            if (alquiler == null)
            {
                return NotFound();
            }
            ViewData["ArrendadorCodigo"] = new SelectList(_context.Arrendadors, "Codigo", "Codigo", alquiler.ArrendadorCodigo);
            ViewData["ArrendatarioCodigo"] = new SelectList(_context.Arrendatarios, "Codigo", "Codigo", alquiler.ArrendatarioCodigo);
            ViewData["CuartoCodigo"] = new SelectList(_context.Cuartos, "Codigo", "Codigo", alquiler.CuartoCodigo);
            ViewData["LuzBañoCodigo"] = new SelectList(_context.LuzBaños, "Codigo", "Codigo", alquiler.LuzBañoCodigo);
            ViewData["LuzCuartoCodigo"] = new SelectList(_context.LuzCuartos, "Codigo", "Codigo", alquiler.LuzCuartoCodigo);
            ViewData["LuzEscaleraCodigo"] = new SelectList(_context.LuzEscaleras, "Codigo", "Codigo", alquiler.LuzEscaleraCodigo);
            return View(alquiler);
        }

        // POST: Alquiler/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,AlquilerMensual,Agua,Cable,FechaActual,FechaVencimiento,PagoTotal,ArrendadorCodigo,LuzCuartoCodigo,LuzBañoCodigo,LuzEscaleraCodigo,ArrendatarioCodigo,CuartoCodigo")] Alquiler alquiler)
        {
            if (id != alquiler.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alquiler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlquilerExists(alquiler.Codigo))
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
            ViewData["ArrendadorCodigo"] = new SelectList(_context.Arrendadors, "Codigo", "Codigo", alquiler.ArrendadorCodigo);
            ViewData["ArrendatarioCodigo"] = new SelectList(_context.Arrendatarios, "Codigo", "Codigo", alquiler.ArrendatarioCodigo);
            ViewData["CuartoCodigo"] = new SelectList(_context.Cuartos, "Codigo", "Codigo", alquiler.CuartoCodigo);
            ViewData["LuzBañoCodigo"] = new SelectList(_context.LuzBaños, "Codigo", "Codigo", alquiler.LuzBañoCodigo);
            ViewData["LuzCuartoCodigo"] = new SelectList(_context.LuzCuartos, "Codigo", "Codigo", alquiler.LuzCuartoCodigo);
            ViewData["LuzEscaleraCodigo"] = new SelectList(_context.LuzEscaleras, "Codigo", "Codigo", alquiler.LuzEscaleraCodigo);
            return View(alquiler);
        }

        // GET: Alquiler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Alquilers == null)
            {
                return NotFound();
            }

            var alquiler = await _context.Alquilers
                .Include(a => a.ArrendadorCodigoNavigation)
                .Include(a => a.ArrendatarioCodigoNavigation)
                .Include(a => a.CuartoCodigoNavigation)
                .Include(a => a.LuzBañoCodigoNavigation)
                .Include(a => a.LuzCuartoCodigoNavigation)
                .Include(a => a.LuzEscaleraCodigoNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (alquiler == null)
            {
                return NotFound();
            }

            return View(alquiler);
        }

        // POST: Alquiler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Alquilers == null)
            {
                return Problem("Entity set 'ArrendamientoWebContext.Alquilers'  is null.");
            }
            var alquiler = await _context.Alquilers.FindAsync(id);
            if (alquiler != null)
            {
                _context.Alquilers.Remove(alquiler);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlquilerExists(int id)
        {
          return (_context.Alquilers?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
