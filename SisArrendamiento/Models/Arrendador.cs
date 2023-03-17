using System;
using System.Collections.Generic;

namespace SisArrendamiento.Models;

public partial class Arrendador
{
    public int Codigo { get; set; }

    public string? Nombres { get; set; }

    public string? Apellidos { get; set; }

    public string? Telefono { get; set; }

    public string? CedulaIdentidad { get; set; }

    public virtual ICollection<Alquiler> Alquilers { get; } = new List<Alquiler>();
}
