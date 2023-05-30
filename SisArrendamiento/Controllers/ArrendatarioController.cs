using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using SisArrendamiento.Models;

namespace SisArrendamiento.Controllers
{
    public class ArrendatarioController : Controller
    {
        private readonly ArrendamientoWebContext _context;

        public ArrendatarioController(ArrendamientoWebContext context)
        {
            _context = context;
        }

        // GET: Arrendatario
        public async Task<IActionResult> Index()
        {     
            var arrendamientoWebContext = _context.Arrendatarios
        .Include(a => a.Alquilers).ThenInclude(a => a.CuartoCodigoNavigation)
        .Include(a => a.Alquilers).ThenInclude(a => a.LuzBañoCodigoNavigation)
        .Include(a => a.Alquilers).ThenInclude(a => a.LuzCuartoCodigoNavigation)
        .Include(a => a.Alquilers).ThenInclude(a => a.LuzEscaleraCodigoNavigation)
        .Include(a => a.Convivientes);
            return View(arrendamientoWebContext.ToList());
        }

        // GET: Arrendatario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Arrendatarios == null)
            {
                return NotFound();
            }

            var arrendatario = await _context.Arrendatarios
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (arrendatario == null)
            {
                return NotFound();
            }

            return View(arrendatario);
        }

        // GET: Arrendatario/Create
        public IActionResult Create()
        {
            ViewData["ArrendadorCodigo"] = _context.Arrendadors.OrderByDescending(c => c.Codigo).Select(p => new SelectListItem
            {
                Value = p.Codigo.ToString(),
                Text = string.Format("{0} {1}", p.Nombres, p.Apellidos)
            });
            ViewData["CuartoCodigo"] = _context.Cuartos.OrderByDescending(c => c.Codigo).Select(p => new SelectListItem
            {
                Value = p.Codigo.ToString(),
                Text = string.Format("{0} {1}", p.Piso, p.Zona)
            });
            return View();
        }

        // POST: Arrendatario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Codigo,Nombres,Apellidos,Telefono,CedulaIdentidad,FechaIngreso,FechaNacimiento")] Arrendatario arrendatario)
        //{
        //    TempData["SuccessMessage"] = "Arrendatario " + arrendatario.Apellidos + " Guardado satisfactoriamente. Para culminar con el contrato, digite los precios para el cuarto";
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(arrendatario);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Crear", "Renta");
        //        //return RedirectToAction("Details", new {id=arrendatario.Codigo}); //Si deseas redireccionar al detalle directamente despues de crear.
        //    }
        //    return View(arrendatario);
        //}
        public async Task<IActionResult> Create(Alquiler alquiler)
        {
            //alquiler = null;
            //alquiler.AlquilerMensual = null;
            //alquiler.Agua = null;
            //alquiler.Cable = null;
            //alquiler.FechaActual = DateTime.Now;
            //alquiler.FechaVencimiento = DateTime.Now;
            //alquiler.LuzBañoCodigoNavigation = null;
            //alquiler.LuzEscaleraCodigoNavigation = null;
            //alquiler.LuzCuartoCodigoNavigation = null;
            //alquiler.CuartoCodigoNavigation = null;
            //alquiler.ArrendadorCodigoNavigation = null;
            if(!ModelState.IsValid)
            {
                _context.Add(alquiler);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("CompletaContrato", "Renta", new { id = alquiler.Codigo });
            }
            ViewData["CuartoCodigo"] = new SelectList(_context.Cuartos, "Codigo", "Codigo", alquiler.CuartoCodigo);
            ViewData["ArrendadorCodigo"] = new SelectList(_context.Arrendadors, "Codigo", "Codigo", alquiler.ArrendadorCodigo);
            return View(alquiler);
        }

        // GET: Arrendatario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Arrendatarios == null)
            {
                return NotFound();
            }

            var arrendatario = await _context.Arrendatarios.FindAsync(id);
            if (arrendatario == null)
            {
                return NotFound();
            }
            return View(arrendatario);
        }

        // POST: Arrendatario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Nombres,Apellidos,Telefono,CedulaIdentidad,FechaIngreso,FechaNacimiento")] Arrendatario arrendatario)
        {
            if (id != arrendatario.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(arrendatario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArrendatarioExists(arrendatario.Codigo))
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
            return View(arrendatario);
        }

        // GET: Arrendatario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Arrendatarios == null)
            {
                return NotFound();
            }

            var arrendatario = await _context.Arrendatarios
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (arrendatario == null)
            {
                return NotFound();
            }

            return View(arrendatario);
        }

        // POST: Arrendatario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Arrendatarios == null)
            {
                return Problem("Entity set 'ArrendamientoWebContext.Arrendatarios'  is null.");
            }
            var arrendatario = await _context.Arrendatarios.FindAsync(id);
            if (arrendatario != null)
            {
                _context.Arrendatarios.Remove(arrendatario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArrendatarioExists(int id)
        {
            return _context.Arrendatarios.Any(e => e.Codigo == id);
        }
        [HttpGet]
        public IActionResult ContratoPdf(int? id)
        {
            var arrendatario = _context.Arrendatarios
        .Include(a => a.Alquilers).ThenInclude(a => a.CuartoCodigoNavigation)
        .Include(a => a.Alquilers).ThenInclude(a => a.LuzBañoCodigoNavigation)
        .Include(a => a.Alquilers).ThenInclude(a => a.LuzCuartoCodigoNavigation)
        .Include(a => a.Alquilers).ThenInclude(a => a.LuzEscaleraCodigoNavigation)
        .Include(a => a.Convivientes)
        .FirstOrDefault(m => m.Codigo == id);
            ViewBag.pdf = true;

            return new ViewAsPdf(arrendatario)
            {
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins =
                {
                    Top = 20,
                    Bottom = 20,
                    Left = 31,
                    Right = 31,
                }
            };
        }
    }
}
