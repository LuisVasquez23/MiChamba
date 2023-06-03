using MiChamba.Data;
using MiChamba.Models;
using MiChamba.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MiChamba.Controllers
{
    public class EmpresaController : BaseController<EmpresaController>
    {
        public EmpresaController(MiChambaDbContext db)
        {
            _db = db;

        }
        public IActionResult Index()
        {
            return View();
        }

       public IActionResult AdministrarPostulaciones()
        {
            return View();
        }
        public List<OfertaViewModel> OfertasDisponibles(int idEmpresa)
        {
            var ofertas = (from oferta in _db.Ofertas
                           where oferta.IdEmpresa == idEmpresa
                           select new OfertaViewModel
                           {
                               IdOferta = oferta.IdOferta,
                               Titulo = oferta.Titulo + " - " + oferta.Empresa.Nombre,
                               Descripcion = oferta.Descripcion.PadRight(10),
                               FechaPublicada = ObtenerTiempoPublicacion(oferta.FechaPublicacion),
                               Ciudad = oferta.Ubicacion,
                               Requisitos = JObject.Parse(oferta.Requisitos)

                           }).ToList();

            return ofertas;
        }
    }
}
