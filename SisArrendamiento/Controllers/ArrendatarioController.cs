using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using SisArrendamiento.Models;
using SisArrendamiento.ViewModels;

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
            var arrendamientoWebContext = _context.Alquilers.Include(a => a.ArrendadorCodigoNavigation).Include(a => a.ArrendatarioCodigoNavigation).Include(a => a.CuartoCodigoNavigation).
                Include(a => a.LuzBañoCodigoNavigation).Include(a => a.LuzCuartoCodigoNavigation).Include(a => a.LuzEscaleraCodigoNavigation);
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
            return View();
        }

        // POST: Arrendatario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Nombres,Apellidos,Telefono,CedulaIdentidad,FechaIngreso,FechaNacimiento")] Arrendatario arrendatario)
        {
            TempData["SuccessMessage"] = "Arrendatario " + arrendatario.Apellidos + " Guardado satisfactoriamente. Para culminar con el contrato, digite los precios para el cuarto";
            if (ModelState.IsValid)
            {
                _context.Add(arrendatario);
                await _context.SaveChangesAsync();
                return RedirectToAction("Crear", "Renta");
                //return RedirectToAction("Details", new {id=arrendatario.Codigo}); //Si deseas redireccionar al detalle directamente despues de crear.
            }
            return View(arrendatario);
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
        //[HttpGet]
        //public IActionResult ContratoPdf(int? id)
        //{
        //    var alquiler = _context.Alquilers
        //        .Include(a => a.ArrendadorCodigoNavigation)
        //        .Include(a => a.ArrendatarioCodigoNavigation)
        //        .Include(a => a.CuartoCodigoNavigation)
        //        .Include(a => a.LuzBañoCodigoNavigation)
        //        .Include(a => a.LuzCuartoCodigoNavigation)
        //        .Include(a => a.LuzEscaleraCodigoNavigation)
        //        .FirstOrDefault(m => m.Codigo == id);
        //    ViewBag.pdf = true;
        //    return new ViewAsPdf(alquiler)
        //    {
        //        PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
        //        PageSize = Rotativa.AspNetCore.Options.Size.A4,
        //    };
        //}
        [HttpGet]
        public IActionResult ContratoPdf(int? id)
        {
            if (id == null || _context.Alquilers == null)
            {
                return NotFound();
            }
            var alquiler = _context.Alquilers
                .Include(a => a.ArrendadorCodigoNavigation)
                .Include(a => a.ArrendatarioCodigoNavigation)
                .Include(a => a.CuartoCodigoNavigation)
                .Include(a => a.LuzBañoCodigoNavigation)
                .Include(a => a.LuzCuartoCodigoNavigation)
                .Include(a => a.LuzEscaleraCodigoNavigation)
                .FirstOrDefault(m => m.Codigo == id);
            var arrendatario = _context.Arrendatarios.FirstOrDefault(a => a.Codigo == id);
            var convivientes = _context.Convivientes.FirstOrDefault(c => c.Codigo == id);

            var model = new AlquilerVM
            {
                Alquiler = alquiler,
                Arrendatario = arrendatario,
                Convivientes = new List<Conviviente> { convivientes }
            };
            
            if (alquiler == null)
            {
                return NotFound();
            }
            ViewBag.pdf = true;
            return View(model);

        }
    }
}
