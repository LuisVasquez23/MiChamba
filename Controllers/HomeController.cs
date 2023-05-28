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


        // VISTAS
        #region INDEX - GET
        public IActionResult Index()
        {
            ViewBag.ofertas = ListarOfertas();
            ViewBag.recursos = ListarRecursos();


            return View();
        }
        #endregion

        #region LOGIN - GET
        public IActionResult Login()
        {
            return View();
        }
        #endregion

        #region REGISTRO - INDEX
        public IActionResult Registro() {     
            return View(); 
        }
        #endregion

        #region OFERTA - GET
        [HttpGet]
        public IActionResult Oferta(int idOferta)
        {  
            //ViewBag.buscar = BuscarOferta();

            ViewBag.oferta = ObtenerOferta(idOferta);

            return View();
        }
        #endregion

        [HttpPost]
        public IActionResult Oferta(IFormCollection form)
        {
            string valorBuscar = form["ofertaSearch"].ToString();

            ViewBag.ofertasBuscadas = BuscarOferta(valorBuscar) ?? Enumerable.Empty<OfertaViewModel>();

            return View();
        }

        // HELPERS
        #region LISTAR OFERTAS
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
        #endregion


        public List<OfertaViewModel> BuscarOferta(string valor)
        {
            List<OfertaViewModel> ofertaObtenida = (from oferta in _db.Ofertas
                                                    join empresa in _db.Empresas on oferta.IdEmpresa equals empresa.IdEmpresa
                                                    orderby oferta.FechaPublicacion descending
                                                    select new OfertaViewModel
                                                    {
                                                        IdOferta = oferta.IdOferta,
                                                        Titulo = oferta.Titulo + " - " + empresa.Nombre,
                                                        Descripcion = oferta.Descripcion.PadRight(10),
                                                        FechaPublicada = ObtenerTiempoPublicacion(oferta.FechaPublicacion),
                                                        Ciudad = oferta.Ubicacion
                                                    })
                .Take(6)
                .Where(o => o.Titulo.Contains(valor))
                .DefaultIfEmpty()
                .ToList();

            return ofertaObtenida;
        }

        #region BUSCAR OFERTA
        public OfertaViewModel ObtenerOferta(int idOfertaP)
        {
            OfertaViewModel? ofertaObtenida = _db.Ofertas
                                        .Include(o => o.Empresa)
                                        .OrderByDescending(i => i.FechaPublicacion)
                                        .Take(6)
                                        .Select(o => new OfertaViewModel
                                        {
                                            IdOferta = o.IdOferta,
                                            Titulo = o.Titulo + " - " + o.Empresa.Nombre,
                                            Descripcion = o.Descripcion.PadRight(10),
                                            FechaPublicada = ObtenerTiempoPublicacion(o.FechaPublicacion),
                                            Ciudad = o.Ubicacion
                                        })
                                        .Where( o => o.IdOferta == idOfertaP)
                                        .FirstOrDefault() ?? new OfertaViewModel();

            return ofertaObtenida;
        }
        #endregion

        #region LISTAR RECURSOS
        public List<Recurso> ListarRecursos()
        {
            List<Recurso> listadoRecursos = _db.Recursos.DefaultIfEmpty().ToList()
                                            ?? Enumerable.Empty<Recurso>().ToList();


            return listadoRecursos;
        }
        #endregion

        #region OBTENER EL TIEMPO DE  PUBLICACION
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
                return (mesesTranscurridos <= 1) ? $"Hace {mesesTranscurridos:N0} mes" : $"Hace {mesesTranscurridos:N0} meses";
            }
        }
        #endregion

    }
}