using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SisArrendamiento.Models;
using SisArrendamiento.ViewModel;

namespace SisArrendamiento.Controllers
{
    public class ConvivientesController : Controller
    {
        private readonly ArrendamientoWebContext _context;

        public ConvivientesController(ArrendamientoWebContext context)
        {
            _context = context;
        }

        // GET: Convivientes
        public async Task<IActionResult> Index()
        {
            var arrendamientoWebContext = _context.Convivientes.Include(c => c.ArrendatarioCodigoNavigation)
                .ThenInclude(c => c.Alquilers).ThenInclude(c => c.CuartoCodigoNavigation)
                .Select(c => new ConvivienteCuartoVM
                {
                    Codigo = c.Codigo,
                    Nombres = c.Nombres,
                    Telefono = c.Telefono,
                    CedulaIdentidad = c.CedulaIdentidad,
                    NombreArrendatario = c.ArrendatarioCodigoNavigation.Nombres,
                    CuartoCodigoNavigation = new Cuarto
                    {
                        Codigo = c.ArrendatarioCodigoNavigation.Alquilers
                        .Select(a => a.CuartoCodigoNavigation.Codigo).FirstOrDefault(),
                        Piso = c.ArrendatarioCodigoNavigation.Alquilers
                        .Select(a => a.CuartoCodigoNavigation.Piso).FirstOrDefault(),
                        Zona = c.ArrendatarioCodigoNavigation.Alquilers
                        .Select(a => a.CuartoCodigoNavigation.Zona).FirstOrDefault(),
                    }
                });
            return View(await arrendamientoWebContext.ToListAsync());
        }

        // GET: Convivientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Convivientes == null)
            {
                return NotFound();
            }

            var conviviente = await _context.Convivientes
                .Include(c => c.ArrendatarioCodigoNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (conviviente == null)
            {
                return NotFound();
            }

            return View(conviviente);
        }

        // GET: Convivientes/Create
        public IActionResult Create()
        {
            //ViewData["ArrendatarioCodigo"] = new SelectList(_context.Arrendatarios, "Codigo", "Codigo");
            ViewData["ArrendatarioCodigo"] = _context.Arrendatarios.OrderByDescending(c => c.Codigo).Select(p => new SelectListItem
            {
                Value = p.Codigo.ToString(),
                Text = string.Format("{0} {1}", p.Nombres, p.Apellidos)
            });
            return View();
        }

        // POST: Convivientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Nombres,Telefono,CedulaIdentidad,ArrendatarioCodigo")] Conviviente conviviente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conviviente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArrendatarioCodigo"] = new SelectList(_context.Arrendatarios, "Codigo", "Codigo", conviviente.ArrendatarioCodigo);
            return View(conviviente);
        }

        // GET: Convivientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Convivientes == null)
            {
                return NotFound();
            }

            var conviviente = await _context.Convivientes.FindAsync(id);
            if (conviviente == null)
            {
                return NotFound();
            }
            ViewData["ArrendatarioCodigo"] = new SelectList(_context.Arrendatarios, "Codigo", "Codigo", conviviente.ArrendatarioCodigo);
            return View(conviviente);
        }

        // POST: Convivientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Nombres,Telefono,CedulaIdentidad,ArrendatarioCodigo")] Conviviente conviviente)
        {
            if (id != conviviente.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conviviente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConvivienteExists(conviviente.Codigo))
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
            ViewData["ArrendatarioCodigo"] = new SelectList(_context.Arrendatarios, "Codigo", "Codigo", conviviente.ArrendatarioCodigo);
            return View(conviviente);
        }

        // GET: Convivientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Convivientes == null)
            {
                return NotFound();
            }

            var conviviente = await _context.Convivientes
                .Include(c => c.ArrendatarioCodigoNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (conviviente == null)
            {
                return NotFound();
            }

            return View(conviviente);
        }

        // POST: Convivientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Convivientes == null)
            {
                return Problem("Entity set 'ArrendamientoWebContext.Convivientes'  is null.");
            }
            var conviviente = await _context.Convivientes.FindAsync(id);
            if (conviviente != null)
            {
                _context.Convivientes.Remove(conviviente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConvivienteExists(int id)
        {
          return _context.Convivientes.Any(e => e.Codigo == id);
        }
    }
}
