using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using SisArrendamiento.Models;
using Rotativa.AspNetCore;
using Microsoft.Build.Execution;
using SisArrendamiento.Paginar;
using System.Diagnostics;

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
        /*public async Task<IActionResult> Index()
        {
            var arrendamientoWebContext = _context.Alquilers.Include(a => a.ArrendadorCodigoNavigation).Include(a => a.ArrendatarioCodigoNavigation).Include(a => a.CuartoCodigoNavigation).Include(a => a.LuzBañoCodigoNavigation).Include(a => a.LuzCuartoCodigoNavigation).Include(a => a.LuzEscaleraCodigoNavigation);
            return View(await arrendamientoWebContext.ToListAsync());
        }*/
        public async Task<IActionResult> Index(DateTime? fechaInicio, DateTime? fechaFin,
            string ordenActual, string filtroActual, string busca, int? numPag)
        {
            fechaInicio ??= DateTime.MinValue;
            fechaFin ??= DateTime.MaxValue;

            ViewData["FechaInicio"] = fechaInicio.Value.ToString("2000-MM-dd");
            ViewData["FechaFin"] = fechaFin.Value.ToString("2030-MM-dd");

            var arrendamientoWebContext = from Alquiler in _context.Alquilers.Include(a => a.ArrendadorCodigoNavigation)
                                          .Include(a => a.ArrendatarioCodigoNavigation).Include(a => a.CuartoCodigoNavigation)
                                          .Include(a => a.LuzBañoCodigoNavigation).Include(a => a.LuzCuartoCodigoNavigation)
                                          .Include(a => a.LuzEscaleraCodigoNavigation)
                                          where Alquiler.FechaActual >= fechaInicio && Alquiler.FechaActual <= fechaFin
                                          select Alquiler;

            var pagoTotal = await arrendamientoWebContext.SumAsync(a => a.PagoTotal);
            ViewData["totalPagos"] = pagoTotal;

            ViewData["ordenActual"] = ordenActual;
            ViewData["filtroNombre"] = String.IsNullOrEmpty(ordenActual) ? "nombre_desc" : "";
            ViewData["filtroFecha"] = ordenActual == "fecha_asc" ? "fecha_desc" : "fecha_asc";


            if (busca != null)
            {
                numPag = 1;
            }
            else
            {
                busca = filtroActual;
            }
            ViewData["filtroActual"] = busca;

            if (!string.IsNullOrEmpty(busca))
            {
                arrendamientoWebContext = arrendamientoWebContext.Where(a => a.ArrendatarioCodigoNavigation.Apellidos.Contains(busca)
                || a.ArrendatarioCodigoNavigation.Nombres.Contains(busca));
            }
            switch (ordenActual)
            {
                case "nombre_desc":
                    arrendamientoWebContext = arrendamientoWebContext.OrderByDescending(a => a.ArrendatarioCodigoNavigation.Apellidos);
                    break;
                case "fecha_asc":
                    arrendamientoWebContext = arrendamientoWebContext.OrderBy(a => a.FechaActual);
                    break;
                case "fecha_desc":
                    arrendamientoWebContext = arrendamientoWebContext.OrderByDescending(a => a.FechaActual);
                    break;

                default:
                    arrendamientoWebContext = arrendamientoWebContext.OrderBy(a => a.ArrendatarioCodigoNavigation.Apellidos);
                    break;
            }
            int pageSize = 8;
            return View(await PaginatedList<Alquiler>.CreateAsync(arrendamientoWebContext.AsNoTracking(), numPag ?? 1, pageSize));
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
            ViewData["CuartoCodigo"] = _context.Cuartos.OrderByDescending(c => c.Codigo).Select(p => new SelectListItem
            {
                Value = p.Codigo.ToString(),
                Text = string.Format("{0} {1}", p.Piso, p.Zona)
            });
            ViewData["LuzBañoCodigo"] = new SelectList(_context.LuzBaños, "Codigo", "Codigo");
            ViewData["LuzCuartoCodigo"] = new SelectList(_context.LuzCuartos, "Codigo", "Codigo");
            ViewData["LuzEscaleraCodigo"] = new SelectList(_context.LuzEscaleras, "Codigo", "Codigo");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Crear(Alquiler alquiler)
        {
            decimal numluz = 1;
            decimal arrendatarios = 10;

            string fa1 = Convert.ToString(alquiler.FechaVencimiento);
            DateTime fa = DateTime.Parse(fa1);
            alquiler.FechaActual = DateTime.Now;
            alquiler.FechaVencimiento = alquiler.FechaActual.AddDays(2);
            alquiler.LuzCuartoCodigoNavigation.AnteriorLuzLectura = null;
            alquiler.LuzBañoCodigoNavigation.AnteriorLuzLectura = null;
            alquiler.LuzEscaleraCodigoNavigation.AnteriorLuzLectura = null;
            alquiler.PagoTotal = alquiler.AlquilerMensual;

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
        public async Task<IActionResult> ObtenerUltimoRegistro(int id)
        {
            // Obtener el último registro de luz del arrendatario
            var ultimoRegistro = await _context.Alquilers
                .Where(r => r.ArrendatarioCodigo == id)
                .OrderByDescending(r => r.FechaActual)
                //.Skip(1)
                .FirstOrDefaultAsync();

            return View(ultimoRegistro);
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
        public IActionResult NewMes(int? id)
        {
            if (id == null)
            {
                return View();
            }
            var alquiler = _context.Alquilers.Include(lc => lc.LuzCuartoCodigoNavigation).Include(lb => lb.LuzBañoCodigoNavigation).Include(le => le.LuzEscaleraCodigoNavigation).Include(cc => cc.CuartoCodigoNavigation).
                FirstOrDefault(c => c.Codigo == id);

            //string fv1 = Convert.ToString(alquiler.FechaVencimiento);
            //DateTime fv = DateTime.Parse(fv1);
            //string fa1 = Convert.ToString(alquiler.FechaActual);
            //DateTime fa = DateTime.Parse(fa1);
            //alquiler.FechaActual.AddMonths(1);
            //alquiler.FechaActual = fa.AddDays(2);
            //alquiler.FechaActual = fa.AddMonths(1);
            //alquiler.FechaVencimiento = fv.AddMonths(1);
            alquiler.FechaVencimiento = alquiler.FechaVencimiento.AddMonths(1);
            alquiler.FechaActual = alquiler.FechaActual.AddMonths(1);

            alquiler.LuzCuartoCodigoNavigation.AnteriorLuzLectura = alquiler.LuzCuartoCodigoNavigation.ActualLuzLectura;
            alquiler.LuzCuartoCodigoNavigation.ActualLuzLectura = null;

            alquiler.LuzBañoCodigoNavigation.AnteriorLuzLectura = alquiler.LuzBañoCodigoNavigation.ActualLuzLectura;
            alquiler.LuzBañoCodigoNavigation.ActualLuzLectura = null;

            alquiler.LuzEscaleraCodigoNavigation.AnteriorLuzLectura = alquiler.LuzEscaleraCodigoNavigation.ActualLuzLectura;
            alquiler.LuzEscaleraCodigoNavigation.ActualLuzLectura = null;
            ViewData["ArrendadorCodigo"] = new SelectList(_context.Arrendadors, "Codigo", "Codigo", alquiler.ArrendadorCodigo);
            //ViewData["ArrendatarioCodigo"] = new SelectList(_context.Arrendatarios, "Codigo", "Nombres", alquiler.ArrendatarioCodigo);
            ViewData["ArrendatarioCodigo"] = _context.Arrendatarios.OrderByDescending(c => c.Codigo).Select(p => new SelectListItem
            {
                Value = p.Codigo.ToString(),
                Text = string.Format("{0} {1}", p.Nombres, p.Apellidos)
            });
            //ViewData["CuartoCodigo"] = _context.Cuartos.OrderByDescending(c => c.Codigo).Select(p => new SelectListItem
            //{
            //    Value = p.Codigo.ToString(),
            //    Text = string.Format("{0} {1}", p.Piso, p.Zona)
            //});
            ViewData["CuartoCodigo"] = new SelectList(_context.Cuartos, "Codigo", "Codigo", alquiler.CuartoCodigo);
            return View(alquiler);
        }
        [HttpPost]

        public async Task<IActionResult> NewMes(Alquiler alquiler)
        {
            decimal numluz = 1;
            decimal arrendatarios = 10;
            //decimal? alc = alquiler.LuzCuartoCodigoNavigation.ActualLuzLectura;
            //decimal? anlc = alquiler.LuzCuartoCodigoNavigation.AnteriorLuzLectura;

            alquiler.LuzCuartoCodigoNavigation.KwConsumido = alquiler.LuzCuartoCodigoNavigation.ActualLuzLectura - alquiler.LuzCuartoCodigoNavigation.AnteriorLuzLectura;
            alquiler.LuzCuartoCodigoNavigation.Monto = (alquiler.LuzCuartoCodigoNavigation.ActualLuzLectura - alquiler.LuzCuartoCodigoNavigation.AnteriorLuzLectura) * numluz;

            alquiler.LuzEscaleraCodigoNavigation.KwConsumido = alquiler.LuzEscaleraCodigoNavigation.ActualLuzLectura - alquiler.LuzEscaleraCodigoNavigation.AnteriorLuzLectura;
            alquiler.LuzEscaleraCodigoNavigation.Monto = (alquiler.LuzEscaleraCodigoNavigation.ActualLuzLectura - alquiler.LuzEscaleraCodigoNavigation.AnteriorLuzLectura) * numluz / arrendatarios;

            alquiler.LuzBañoCodigoNavigation.KwConsumido = alquiler.LuzBañoCodigoNavigation.ActualLuzLectura - alquiler.LuzBañoCodigoNavigation.AnteriorLuzLectura;
            alquiler.LuzBañoCodigoNavigation.Monto = (alquiler.LuzBañoCodigoNavigation.ActualLuzLectura - alquiler.LuzBañoCodigoNavigation.AnteriorLuzLectura) * numluz;

            decimal? montoALC = (alquiler.LuzCuartoCodigoNavigation.ActualLuzLectura - alquiler.LuzCuartoCodigoNavigation.AnteriorLuzLectura) * numluz;
            decimal? montoALE = alquiler.LuzEscaleraCodigoNavigation.Monto = (alquiler.LuzEscaleraCodigoNavigation.ActualLuzLectura - alquiler.LuzEscaleraCodigoNavigation.AnteriorLuzLectura) * numluz / arrendatarios;
            decimal? montoALB = (alquiler.LuzBañoCodigoNavigation.ActualLuzLectura - alquiler.LuzBañoCodigoNavigation.AnteriorLuzLectura) * numluz;

            decimal? calc = (alquiler.AlquilerMensual.Value +
                montoALC +
                montoALE +
                montoALC +
                alquiler.Agua.Value + alquiler.Cable.Value);

            alquiler.PagoTotal = Math.Round((decimal)calc, 1);

            //alquiler.PagoTotal = Math.Round((decimal)(alquiler.AlquilerMensual.Value +
            //        alquiler.LuzCuartoCodigoNavigation.Monto.Value +
            //        alquiler.LuzEscaleraCodigoNavigation.Monto.Value +
            //        alquiler.LuzBañoCodigoNavigation.Monto.Value +
            //        alquiler.Agua.Value + alquiler.Cable.Value), 1);
            if (ModelState.IsValid)
            {
                _context.Add(alquiler);
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
            var alquiler = _context.Alquilers           
                .FirstOrDefault(c => c.Codigo == id);

            var luzBaño = _context.LuzBaños
                .FirstOrDefault(c => c.Codigo == id);

            var luzEscalera = _context.LuzEscaleras
                .FirstOrDefault(c => c.Codigo == id);

            var luzCuarto = _context.LuzCuartos
                .FirstOrDefault(c => c.Codigo == id);

            _context.LuzBaños.Remove(luzBaño);
            _context.LuzEscaleras.Remove(luzEscalera);
            _context.LuzCuartos.Remove(luzCuarto);
            _context.Alquilers.Remove(alquiler);

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
