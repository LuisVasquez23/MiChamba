using MiChamba.Data;
using MiChamba.Models;
using MiChamba.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            ViewBag.totalOfertas = ObtenerTotalOfertas(); // Obtener el total de ofertas
            ViewBag.totalEmpresas = ObtenerTotalEmpresas(); // Obtener el total de empresas
            ViewBag.totalUsuarios = ObtenerTotalUsuarios(); // Obtener el total de usuarios



            return View();
        }
        #endregion

        #region OFERTA - GET
        [HttpGet]
        public IActionResult Oferta(int idOferta)
        {  

            ViewBag.oferta = ObtenerOferta(idOferta);

            return View();
        }
        #endregion


        #region OFERTA - DINAMICA 
        public ActionResult OfertaParcial(int idOferta)
        {
            // Lógica para obtener el contenido dinámico actualizado
            OfertaViewModel oferta = BuscarOfertaIndividual(idOferta);

            // Devuelve la vista parcial actualizada
            return PartialView("_OfertaParcial", oferta);
        }
        #endregion

        [HttpGet]
        public OfertaViewModel BuscarOfertaIndividual(int idOferta)
        {

            OfertaViewModel? ofertaPostulada = (from o in _db.Ofertas
                                                     join p in _db.Postulaciones on o.IdOferta equals p.IdOferta into op
                                                     from p in op.DefaultIfEmpty()
                                                     join u in _db.Usuarios on p.IdUsuario equals u.IdUsuario into pu
                                                     from u in pu.DefaultIfEmpty()
                                                     where o.IdOferta == idOferta
                                                     select new OfertaViewModel
                                                     {
                                                         IdOferta = o.IdOferta,
                                                         Titulo = o.Titulo ,
                                                         Descripcion = o.Descripcion.PadRight(10),
                                                         FechaPublicada = ObtenerTiempoPublicacion(o.FechaPublicacion),
                                                         Ciudad = o.Ubicacion,
                                                         Requisitos = JObject.Parse(o.Requisitos.Replace("'", "\"")),
                                                         EstasPostulado = (u.IdUsuario.ToString() == HttpContext.Session.GetString("id_usuario")) ? "S" : "N"
                                                     }).FirstOrDefault();


            return ofertaPostulada;
        }

        [HttpPost]
        public IActionResult Ofertas(IFormCollection formBusqueda)
        {

            string valorBuscado = formBusqueda["ofertaSearch"].ToString();

            ViewBag.foto = HttpContext.Session.GetString("foto");
            ViewBag.ofertaBuscada = valorBuscado;
            ViewBag.ofertas = BuscarOferta(valorBuscado);

            return View();
        }

        // HELPERS
        #region LISTAR OFERTAS
        public List<OfertaViewModel> ListarOfertas()
        {
            List<OfertaViewModel> ListaOfertas = _db.Ofertas
                                        .OrderByDescending( i => i.FechaPublicacion)
                                        .Take(6)
                                        .Select( o => new OfertaViewModel
                                        {
                                            IdOferta = o.IdOferta,
                                            Titulo = o.Titulo,
                                            Descripcion = o.Descripcion.PadRight(10),
                                            FechaPublicada = ObtenerTiempoPublicacion(o.FechaPublicacion),
                                            Requisitos = JObject.Parse(o.Requisitos.Replace("'", "\""))
                                        })
                                        .DefaultIfEmpty().ToList();

            return ListaOfertas;
        }
        #endregion

        #region BUSCAR OFERTA
        public List<OfertaViewModel> BuscarOferta(string valor)
        {
            List<OfertaViewModel> ofertaObtenida = new List<OfertaViewModel>();

            try
            {

                ofertaObtenida = (from oferta in _db.Ofertas
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
            catch(Exception e)
            {
                return new List<OfertaViewModel>();
            }

            return ofertaObtenida;
        }
        #endregion

        #region BUSCAR OFERTA
        public OfertaViewModel ObtenerOferta(int idOfertaP)
        {
            OfertaViewModel? ofertaObtenida = _db.Ofertas
                                        .OrderByDescending(i => i.FechaPublicacion)
                                        .Take(6)
                                        .Select(o => new OfertaViewModel
                                        {
                                            IdOferta = o.IdOferta,
                                            Titulo = o.Titulo,
                                            Descripcion = o.Descripcion.PadRight(10),
                                            FechaPublicada = ObtenerTiempoPublicacion(o.FechaPublicacion),
                                            Ciudad = o.Ubicacion,
                                            Requisitos = JObject.Parse(o.Requisitos.Replace("'", "\""))
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


        #region OBTENER TOTAL DE OFERTAS
        public int ObtenerTotalOfertas()
        {
            int totalOfertas = _db.Ofertas.Count();
            return totalOfertas;
        }
        #endregion

        #region OBTENER TOTAL DE EMPRESAS
        public int ObtenerTotalEmpresas()
        {
            int totalEmpresas = _db.Empresas.Count();
            return totalEmpresas;
        }
        #endregion

        #region OBTENER TOTAL DE USUARIOS
        public int ObtenerTotalUsuarios()
        {
            int totalUsuarios = _db.Usuarios.Count();
            return totalUsuarios;
        }
        #endregion 

    }

}