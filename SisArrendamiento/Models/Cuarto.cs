using System;
using System.Collections.Generic;

namespace SisArrendamiento.Models;

public partial class Cuarto
{
    public int Codigo { get; set; }

    public string? Piso { get; set; }

    public string? Zona { get; set; }

    public virtual ICollection<Alquiler> Alquilers { get; } = new List<Alquiler>();
}
