using System;
using System.Collections.Generic;

namespace SisArrendamiento.Models;

public partial class Conviviente
{
    public int Codigo { get; set; }

    public string? Nombres { get; set; }

    public string? Telefono { get; set; }

    public string? CedulaIdentidad { get; set; }

    public int? ArrendatarioCodigo { get; set; }

    public virtual Arrendatario? ArrendatarioCodigoNavigation { get; set; }
}
