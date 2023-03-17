using SisArrendamiento.Models;

namespace SisArrendamiento.ViewModels
{
    public class AlquilerVM
    {
        public Alquiler Alquiler { get; set; }
        public Arrendatario Arrendatario { get; set; }
        public List<Conviviente> Convivientes { get; set; }
    }
}
