using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SisArrendamiento.Models;

public partial class Alquiler
{
    public int Codigo { get; set; }

    public decimal? AlquilerMensual { get; set; }

    public decimal? Agua { get; set; }

    public decimal? Cable { get; set; }
    [DataType(DataType.Date)]
    public DateTime FechaActual { get; set; }
    [DataType(DataType.Date)]
    public DateTime FechaVencimiento { get; set; }
    //[Editable(false)]

    public decimal? PagoTotal { get; set; }

    public int ArrendadorCodigo { get; set; }

    public int LuzCuartoCodigo { get; set; }

    public int LuzBañoCodigo { get; set; }

    public int LuzEscaleraCodigo { get; set; }

    public int ArrendatarioCodigo { get; set; }

    public int CuartoCodigo { get; set; }

    public virtual Arrendador ArrendadorCodigoNavigation { get; set; } = null!;

    public virtual Arrendatario ArrendatarioCodigoNavigation { get; set; } = null!;

    public virtual Cuarto CuartoCodigoNavigation { get; set; } = null!;

    public virtual LuzBaño LuzBañoCodigoNavigation { get; set; } = null!;

    public virtual LuzCuarto LuzCuartoCodigoNavigation { get; set; } = null!;

    public virtual LuzEscalera LuzEscaleraCodigoNavigation { get; set; } = null!;
}
