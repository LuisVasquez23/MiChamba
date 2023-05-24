using MiChamba.Data;
using MiChamba.Models;
using MiChamba.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MiChamba.Controllers
{
    public class HomeController : BaseController<HomeController>
    {

        public HomeController(MiChambaDbContext db) {
            _db = db;
        }

        public IActionResult Index()
        {
            ViewBag.ofertas = ListarOfertas();

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        // HELPERS
        public List<OfertaViewModel> ListarOfertas()
        {
            List<OfertaViewModel> ListaOfertas = _db.Ofertas
                                        .Include(o => o.Empresa)
                                        .OrderByDescending( i => i.FechaPublicacion)
                                        .Take(6)
                                        .Select( o => new OfertaViewModel
                                        {
                                            IdOferta = o.IdOferta,
                                            Titulo = o.Titulo + " - " + o.Empresa.Nombre,
                                            Descripcion = o.Descripcion.PadRight(10),
                                            FechaPublicada = ObtenerTiempoPublicacion(o.FechaPublicacion)
                                        })
                                        .DefaultIfEmpty().ToList();

            return ListaOfertas;
        }

        public static string ObtenerTiempoPublicacion(DateTime fechaPublicacion)
        {
            TimeSpan tiempoTranscurrido = DateTime.Now - fechaPublicacion;

            if (tiempoTranscurrido.TotalMinutes < 60)
            {
                return $"Hace {tiempoTranscurrido.TotalMinutes:N0} minutos";
            }
            else if (tiempoTranscurrido.TotalHours < 24)
            {
                return $"Hace {tiempoTranscurrido.TotalHours:N0} horas";
            }
            else if (tiempoTranscurrido.TotalDays < 30)
            {
                return $"Hace {tiempoTranscurrido.TotalDays:N0} días";
            }
            else
            {
                int mesesTranscurridos = (int)(tiempoTranscurrido.TotalDays / 30);
                return $"Hace {mesesTranscurridos:N0} meses";
            }
        }


    }
}