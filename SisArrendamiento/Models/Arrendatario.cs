using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SisArrendamiento.Models;

public partial class Arrendatario
{
    public int Codigo { get; set; }

    public string? Nombres { get; set; }

    public string? Apellidos { get; set; }

    public string? Telefono { get; set; }

    public string? CedulaIdentidad { get; set; }
    [DataType(DataType.Date)]
    public DateTime FechaIngreso { get; set; }
    [DataType(DataType.Date)]
    public DateTime? FechaNacimiento { get; set; }

    public virtual ICollection<Alquiler> Alquilers { get; } = new List<Alquiler>();

    public virtual ICollection<Conviviente> Convivientes { get; } = new List<Conviviente>();
}
