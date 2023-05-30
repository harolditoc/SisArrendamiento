using SisArrendamiento.Models;

namespace SisArrendamiento.ViewModel
{
    public partial class ConvivienteCuartoVM
    {
        public int Codigo { get; set; }

        public string? Nombres { get; set; }

        public string? Telefono { get; set; }

        public string? CedulaIdentidad { get; set; }

        public string? NombreArrendatario { get; set; }
        public virtual Cuarto? CuartoCodigoNavigation { get; set; }
    }
}
