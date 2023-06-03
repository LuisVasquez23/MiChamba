using MiChamba.Data;
using MiChamba.Models;
using MiChamba.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;


namespace MiChamba.Controllers
{
    public class UsuarioController : BaseController<UsuarioController>
    {
        public UsuarioController(MiChambaDbContext db)
        {
            _db = db;

        }

        // VISTAS

        #region INDEX - GET
        [HttpGet]
        public IActionResult Index()
        {
            if (!VerifyUserLogin())
            {
                return RedirectToAction("Login");
            }

            ViewBag.foto = HttpContext.Session.GetString("foto");

            return View();
        }
        #endregion

        #region REGISTRO - GET
        public IActionResult Registro()
        {
            if (VerifyUserLogin())
            {
                return RedirectToAction("Index");
            }

            Usuario usuario = new Usuario();

            return View(usuario);
        }
        #endregion

        #region REGISTRO - POST
        [HttpPost]
        public IActionResult Registro(Usuario usuario)
        {
            usuario.Imagen = "default";

            // Guardar la imagen en el servidor
            if (usuario.ImagenFile != null && usuario.ImagenFile.Length > 0)
            {

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "img");
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(usuario.ImagenFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    usuario.ImagenFile.CopyTo(stream);
                }

                // Actualizar la propiedad Imagen con la ruta relativa del archivo guardado
                usuario.Imagen = fileName;
            }

            // Registrar el usuario en el contexto de la base de datos
            _db.Usuarios.Add(usuario);
            _db.SaveChanges();

            return RedirectToAction("Index", "Usuario");
        }
        #endregion

        #region LOGIN - GET
        [HttpGet]
        public IActionResult Login()
        {
            if (VerifyUserLogin()) {
                return RedirectToAction("Index");
            }

            return View();
        }
        #endregion

        #region LOGIN - POST
        [HttpPost]
        public IActionResult Login(Usuario usuarioP)
        {

            string email = usuarioP.Email;
            string password = usuarioP.Password;

            var usuarioGuardado = (from usuario in _db.Usuarios
                                   where usuario.Email == email & usuario.Password == password
                                   select new
                                   {
                                       usuario.IdUsuario,
                                       usuario.Nombre,
                                       usuario.Email,
                                       usuario.Imagen
                                   }).FirstOrDefault();

            // Si no existe enviar al login

            if (usuarioGuardado == null)
            {
                return RedirectToAction("Login");
            }

            // Set la session
            HttpContext.Session.SetString("id_usuario", usuarioGuardado.IdUsuario.ToString());
            HttpContext.Session.SetString("nombre_usuario", usuarioGuardado.Nombre);
            HttpContext.Session.SetString("email", usuarioGuardado.Email);
            HttpContext.Session.SetString("foto", usuarioGuardado.Imagen);

            TempData["nombre_usuario"] = usuarioGuardado.Nombre;
            ViewBag.foto = usuarioGuardado.Imagen;

            return RedirectToAction("Index", "Usuario");
        }
        #endregion

        #region LOG-OUT - GET    
        [HttpGet]
        public IActionResult LogOut()
        {

            if (!VerifyUserLogin())
            {
                return RedirectToAction("Index");
            }

            HttpContext.Session.Remove("id_usuario");

            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region OFERTAS - POST
        [HttpPost]
        public IActionResult Ofertas(IFormCollection formBusqueda) {

            if (!VerifyUserLogin())
            {
                return RedirectToAction("Index");
            }

            string valorBuscado = formBusqueda["empleoBuscar"].ToString();

            ViewBag.foto = HttpContext.Session.GetString("foto");
            ViewBag.ofertaBuscada = valorBuscado;
            ViewBag.ofertas = BuscarOfertas(valorBuscado);


            return View();
        }
        #endregion

        #region AJUSTES - GET
        public IActionResult Ajustes() {

            if (!VerifyUserLogin())
            {
                return RedirectToAction("Index");
            }

            ViewBag.foto = HttpContext.Session.GetString("foto");
            return View();
        }
        #endregion

        #region METODIFICACION PERFIL - GET
        public IActionResult ModificacionPerfil()
        {

            ViewBag.foto = HttpContext.Session.GetString("foto");
            return View();
        }
        #endregion

        public IActionResult Postulaciones() {

            if (!VerifyUserLogin())
            {
                return RedirectToAction("Index");
            }

            ViewBag.postulaciones = PostulacionesUsuario();
            ViewBag.foto = HttpContext.Session.GetString("foto");

            return View();
        }

        // HELPERS
        #region POSTULACIONS DEL USUARIO
        public List<object> PostulacionesUsuario()
        {
            int idUsuario = int.Parse(HttpContext.Session.GetString("id_usuario"));

            var MisPostulaciones = (
                                                    from postulacion in _db.Postulaciones
                                                    join oferta in _db.Ofertas  on postulacion.IdOferta equals oferta.IdOferta
                                                    where postulacion.IdUsuario == idUsuario
                                                    select new
                                                    {
                                                        titulo = oferta.Titulo,
                                                        descripcion = oferta.Descripcion,
                                                        estado = postulacion.EstadoPostulacion
                                                    }
                                                ).DefaultIfEmpty().ToList<object>();


            return MisPostulaciones;

        }
        #endregion

        #region OFERTA - DINAMICA 
        public ActionResult Oferta(int idOferta)
        {
            // Lógica para obtener el contenido dinámico actualizado
            OfertaViewModel oferta = BuscarOferta(idOferta);

            // Devuelve la vista parcial actualizada
            return PartialView("_OfertaParcial", oferta);
        }
        #endregion

        // HELPERS
        #region BUSCAR OFERTAS - LISTAR 
        public List<OfertaViewModel> BuscarOfertas(string valorBuscar)
        {
            List<OfertaViewModel> ofertasEncontradas = (from oferta in _db.Ofertas
                                                        where oferta.Titulo.ToLower().Contains(valorBuscar)
                                                        select new OfertaViewModel
                                                        {
                                                            IdOferta = oferta.IdOferta,
                                                            Titulo = oferta.Titulo + " - " + oferta.Empresa.Nombre,
                                                            Descripcion = oferta.Descripcion.PadRight(10),
                                                            FechaPublicada = ObtenerTiempoPublicacion(oferta.FechaPublicacion),
                                                            Requisitos = JObject.Parse(oferta.Requisitos)
                                                        }
                                      ).ToList();

            return ofertasEncontradas;
        }
        #endregion

        #region  BUSCAR OFERTA INDIVIDUAL
        [HttpGet]
        public OfertaViewModel BuscarOferta(int idOferta){
                
            
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
                                                Ciudad = o.Ubicacion,
                                                Requisitos = JObject.Parse(o.Requisitos)
                                            })
                                            .Where(o => o.IdOferta == idOferta)
                                            .FirstOrDefault() ?? new OfertaViewModel();

                return ofertaObtenida;
        }
        #endregion


        
    }
}
