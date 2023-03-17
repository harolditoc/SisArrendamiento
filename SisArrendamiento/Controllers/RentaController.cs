using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using SisArrendamiento.Models;
using Rotativa.AspNetCore;

namespace SisArrendamiento.Controllers
{
    public class RentaController : Controller
    {
        public decimal calcLuz = 1;
        public decimal numArrendatarios = 10;

        private readonly ArrendamientoWebContext _context;

        public RentaController(ArrendamientoWebContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var arrendamientoWebContext = _context.Alquilers.Include(a => a.ArrendadorCodigoNavigation).Include(a => a.ArrendatarioCodigoNavigation).Include(a => a.CuartoCodigoNavigation).Include(a => a.LuzBañoCodigoNavigation).Include(a => a.LuzCuartoCodigoNavigation).Include(a => a.LuzEscaleraCodigoNavigation);
            return View(await arrendamientoWebContext.ToListAsync());
        }
        // GET: Alquiler/Details/5
        [HttpGet]
        public async Task<IActionResult> Detalles(int? id)
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
            ViewBag.pdf = true;
            return new ViewAsPdf(alquiler)
            {
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
            };
        }
        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            Arrendatario ar = new Arrendatario();
            ViewData["Nombres"] = string.Format("{0} {1}", ar.Nombres, ar.Apellidos);
            ViewData["ArrendadorCodigo"] = new SelectList(_context.Arrendadors, "Codigo", "Codigo");
            //ViewData["ArrendatarioCodigo"] = new SelectList(_context.Arrendatarios.OrderByDescending(c => c.Codigo), "Codigo", "Nombres");
            ViewData["ArrendatarioCodigo"] = _context.Arrendatarios.OrderByDescending(c => c.Codigo).Select(p => new SelectListItem
            {
                Value = p.Codigo.ToString(),
                Text = string.Format("{0} {1}", p.Nombres, p.Apellidos)
            });
            ViewData["CuartoCodigo"] = new SelectList(_context.Cuartos, "Codigo", "Codigo");
            ViewData["LuzBañoCodigo"] = new SelectList(_context.LuzBaños, "Codigo", "Codigo");
            ViewData["LuzCuartoCodigo"] = new SelectList(_context.LuzCuartos, "Codigo", "Codigo");
            ViewData["LuzEscaleraCodigo"] = new SelectList(_context.LuzEscaleras, "Codigo", "Codigo");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Crear(Alquiler alquiler)
        {
            alquiler.FechaActual = DateTime.Today;
            alquiler.FechaVencimiento = DateTime.Today;
            alquiler.PagoTotal= null;
            alquiler.LuzCuartoCodigoNavigation.ActualLuzLectura = null;
            alquiler.LuzCuartoCodigoNavigation.AnteriorLuzLectura = null;
            alquiler.LuzCuartoCodigoNavigation.KwConsumido = null;
            alquiler.LuzCuartoCodigoNavigation.Monto = null;
            alquiler.LuzEscaleraCodigoNavigation.ActualLuzLectura = null;
            alquiler.LuzEscaleraCodigoNavigation.AnteriorLuzLectura = null;
            alquiler.LuzEscaleraCodigoNavigation.KwConsumido = null;
            alquiler.LuzEscaleraCodigoNavigation.Monto = null;
            alquiler.LuzBañoCodigoNavigation.ActualLuzLectura = null;
            alquiler.LuzBañoCodigoNavigation.AnteriorLuzLectura = null;
            alquiler.LuzBañoCodigoNavigation.KwConsumido = null;
            alquiler.LuzBañoCodigoNavigation.Monto = null;
            alquiler.CuartoCodigoNavigation.Piso = null;
            alquiler.CuartoCodigoNavigation.Zona = null;

            if (!ModelState.IsValid)
            {
                _context.Add(alquiler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArrendadorCodigo"] = new SelectList(_context.Arrendadors, "Codigo", "Codigo", alquiler.ArrendadorCodigo);
            ViewData["ArrendatarioCodigo"] = new SelectList(_context.Arrendatarios, "Codigo", "Codigo", alquiler.ArrendatarioCodigo);
            //ViewData["CuartoCodigo"] = new SelectList(_context.Cuartos, "Codigo", "Codigo", alquiler.CuartoCodigo);
            //ViewData["LuzBañoCodigo"] = new SelectList(_context.LuzBaños, "Codigo", "Codigo", alquiler.LuzBañoCodigo);
            //ViewData["LuzCuartoCodigo"] = new SelectList(_context.LuzCuartos, "Codigo", "Codigo", alquiler.LuzCuartoCodigo);
            //ViewData["LuzEscaleraCodigo"] = new SelectList(_context.LuzEscaleras, "Codigo", "Codigo", alquiler.LuzEscaleraCodigo);
            return View(alquiler);
        }
        [HttpGet]
        public IActionResult CrearExistente(int id)
        {
            var alquiler = _context.Alquilers.Include(lc => lc.LuzCuartoCodigoNavigation).Include(lb => lb.LuzBañoCodigoNavigation).Include(le => le.LuzEscaleraCodigoNavigation).
                Where(a => a.Codigo == id).FirstOrDefault(c => c.Codigo == id);
            ViewData["ArrendadorCodigo"] = new SelectList(_context.Arrendadors, "Codigo", "Codigo", alquiler.ArrendadorCodigo);
            ViewData["ArrendatarioCodigo"] = new SelectList(_context.Arrendatarios, "Codigo", "Nombres", alquiler.ArrendatarioCodigo);
            ViewData["CuartoCodigo"] = new SelectList(_context.Cuartos, "Codigo", "Codigo", alquiler.CuartoCodigo);
            
            return View(alquiler);
        }

        [HttpPost]
        public IActionResult CrearExistente(Alquiler alquiler)
        {
            //Se creara un nuevo boton de crear en el index, que a base de un combobox de los datos del ultimo registro del arrendatario.
            //Se veran los arrendatarios que tienen que pagar esa misma fecha.
            return View();
        }
        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null)
            {
                return View();
            }
            var alquiler = _context.Alquilers.Include(lc => lc.LuzCuartoCodigoNavigation).Include(lb => lb.LuzBañoCodigoNavigation).Include(le => le.LuzEscaleraCodigoNavigation).
                FirstOrDefault(c => c.Codigo == id);
            ViewData["ArrendadorCodigo"] = new SelectList(_context.Arrendadors, "Codigo", "Codigo", alquiler.ArrendadorCodigo);
            ViewData["ArrendatarioCodigo"] = new SelectList(_context.Arrendatarios, "Codigo", "Nombres", alquiler.ArrendatarioCodigo);
            ViewData["CuartoCodigo"] = new SelectList(_context.Cuartos, "Codigo", "Codigo", alquiler.CuartoCodigo);
            //ViewData["LuzBañoCodigo"] = new SelectList(_context.LuzBaños, "Codigo", "Monto", alquiler.LuzBañoCodigo);
            //ViewData["LuzCuartoCodigo"] = new SelectList(_context.LuzCuartos, "Codigo", "Monto", alquiler.LuzCuartoCodigo);
            //ViewData["LuzEscaleraCodigo"] = new SelectList(_context.LuzEscaleras, "Codigo", "Monto", alquiler.LuzEscaleraCodigo);
            return View(alquiler);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Alquiler alquiler)
        {
            decimal numluz = 1;
            decimal arrendatarios = 10;

            string fa1 = Convert.ToString(alquiler.FechaVencimiento);
            DateTime fa = DateTime.Parse(fa1);
            alquiler.FechaActual = fa.AddDays(-2);

            alquiler.LuzCuartoCodigoNavigation.KwConsumido = alquiler.LuzCuartoCodigoNavigation.ActualLuzLectura - alquiler.LuzCuartoCodigoNavigation.AnteriorLuzLectura;
            alquiler.LuzCuartoCodigoNavigation.Monto = (alquiler.LuzCuartoCodigoNavigation.ActualLuzLectura - alquiler.LuzCuartoCodigoNavigation.AnteriorLuzLectura) * numluz;

            alquiler.LuzEscaleraCodigoNavigation.KwConsumido = alquiler.LuzEscaleraCodigoNavigation.ActualLuzLectura - alquiler.LuzEscaleraCodigoNavigation.AnteriorLuzLectura;
            alquiler.LuzEscaleraCodigoNavigation.Monto = (alquiler.LuzEscaleraCodigoNavigation.ActualLuzLectura - alquiler.LuzEscaleraCodigoNavigation.AnteriorLuzLectura) * numluz / arrendatarios;

            alquiler.LuzBañoCodigoNavigation.KwConsumido = alquiler.LuzBañoCodigoNavigation.ActualLuzLectura - alquiler.LuzBañoCodigoNavigation.AnteriorLuzLectura;
            alquiler.LuzBañoCodigoNavigation.Monto = (alquiler.LuzBañoCodigoNavigation.ActualLuzLectura - alquiler.LuzBañoCodigoNavigation.AnteriorLuzLectura) * numluz;

            alquiler.PagoTotal = Math.Round((decimal)(alquiler.AlquilerMensual + alquiler.LuzCuartoCodigoNavigation.Monto + 
                alquiler.LuzEscaleraCodigoNavigation.Monto +
                alquiler.LuzBañoCodigoNavigation.Monto +
                alquiler.Agua + alquiler.Cable), 1);   

            if (ModelState.IsValid)
            {
                _context.Alquilers.Update(alquiler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArrendadorCodigo"] = new SelectList(_context.Arrendadors, "Codigo", "Codigo", alquiler.ArrendadorCodigo);
            ViewData["ArrendatarioCodigo"] = new SelectList(_context.Arrendatarios, "Codigo", "Codigo", alquiler.ArrendatarioCodigo);
            ViewData["CuartoCodigo"] = new SelectList(_context.Cuartos, "Codigo", "Codigo", alquiler.CuartoCodigo);
            //ViewData["LuzBañoCodigo"] = new SelectList(_context.LuzBaños, "Codigo", "Monto", alquiler.LuzBañoCodigo);
            //ViewData["LuzCuartoCodigo"] = new SelectList(_context.LuzCuartos, "Codigo", "Monto", alquiler.LuzCuartoCodigo);
            //ViewData["LuzEscaleraCodigo"] = new SelectList(_context.LuzEscaleras, "Codigo", "Monto", alquiler.LuzEscaleraCodigo);
            return View(alquiler);
        }
        [HttpGet]
        public IActionResult Eliminar(int? id)
        {
            var alquiler = _context.Alquilers.FirstOrDefault(c => c.Codigo == id);
            _context.Alquilers.Remove(alquiler);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
