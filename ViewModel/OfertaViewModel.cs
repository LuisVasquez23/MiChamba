using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MiChamba.ViewModel
{
    public class OfertaViewModel
    {
        public int IdOferta { get; set; }
        public String? Titulo { get; set; }
        public String? Ciudad { get; set; }
        public String? Descripcion { get; set; }
        public String? FechaPublicada { get; set; }

        public String? EstadoPostulacion { get; set; }

        public String? EstasPostulado { get; set; }

        public JObject? Requisitos { get; set; }

    }
}
