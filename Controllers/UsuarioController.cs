using MiChamba.Data;
using MiChamba.Models;
using MiChamba.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Client;
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
            ViewBag.postulaciones = PostulacionesUsuario();

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

            int idUsuario = int.Parse(HttpContext.Session.GetString("id_usuario"));

            Curriculum? curriculumExistente = (from c in _db.Curriculums
                                               where c.IdUsuario == idUsuario
                                               select c).FirstOrDefault();

            ViewBag.foto = HttpContext.Session.GetString("foto");
            ViewBag.curriculum = curriculumExistente;

            return View();
        }
        #endregion

        #region METODIFICACION PERFIL - GET
        public IActionResult ModificacionPerfil()
        {

            int id = int.Parse(HttpContext.Session.GetString("id_usuario"));

            Usuario usuario = _db.Usuarios.FirstOrDefault(u => u.IdUsuario == id);

            ViewBag.Vpais = usuario.Pais;
            ViewBag.Vestado = usuario.Estado;


            ViewBag.foto = HttpContext.Session.GetString("foto");

            return View(usuario);
        }
        #endregion

        #region MofificacionPerfil - POST
        [HttpPost]
        public IActionResult ModificacionPerfil(Usuario usuario)
        {

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


            int id = int.Parse(HttpContext.Session.GetString("id_usuario"));
            Usuario usuarioExistente = _db.Usuarios.FirstOrDefault(u => u.IdUsuario == id);

            if (usuarioExistente != null)
            {
                // Actualiza las propiedades del usuario 
                usuarioExistente.Nombre = usuario.Nombre;
                usuarioExistente.Apellido = usuario.Apellido;
                usuarioExistente.Email = usuario.Email;
                usuarioExistente.Pais = usuario.Pais;
                usuarioExistente.Estado = usuario.Estado;
                usuarioExistente.Imagen = usuario.Imagen;
                usuarioExistente.Password = usuario.Password;
                usuarioExistente.Telefono = usuario.Telefono;


                // Set la session
                HttpContext.Session.SetString("id_usuario", usuarioExistente.IdUsuario.ToString());
                HttpContext.Session.SetString("nombre_usuario", usuarioExistente.Nombre);
                HttpContext.Session.SetString("email", usuarioExistente.Email);
                HttpContext.Session.SetString("foto", usuarioExistente.Imagen);

                // Guarda los cambios en la base de datos
                _db.SaveChanges();
            }


            return RedirectToAction("Index", "Usuario");
        }
        #endregion

        #region POSTULACIONES - GET
        public IActionResult Postulaciones() {

            if (!VerifyUserLogin())
            {
                return RedirectToAction("Index");
            }

            ViewBag.postulaciones = PostulacionesUsuario();
            ViewBag.foto = HttpContext.Session.GetString("foto");

            return View();
        }
        #endregion

        #region POSTULARME - GET 
        [HttpGet]
        public IActionResult Postularme(int idOferta)
        {
            Postulacion postulacion = new Postulacion();

            try
            {
                postulacion.IdOferta = idOferta;
                postulacion.IdUsuario = int.Parse(HttpContext.Session.GetString("id_usuario"));
                postulacion.EstadoPostulacion = "Enviada";
                postulacion.FechaPostulacion = DateTime.Now;

                _db.Postulaciones.Add(postulacion);
                _db.SaveChanges();
                return RedirectToAction("Postulaciones", "Usuario");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region SUBIR CV
        [HttpPost]
        public IActionResult RegistrarCV(Curriculum cv)
        {
            int idUsuario = int.Parse(HttpContext.Session.GetString("id_usuario"));

            cv.IdUsuario = idUsuario ;

            if (cv.PdfFile != null && cv.PdfFile.Length > 0)
            {
                try
                {
                    // Generar un nombre único para el archivo PDF
                    string nombreArchivo = cv.IdUsuario + "-" + HttpContext.Session.GetString("nombre_usuario") + ".pdf";
                    nombreArchivo = nombreArchivo.Trim();

                    // Construir la ruta de destino
                    string rutaDestino = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" , "uploads", "pdf", nombreArchivo);

                    // Guardar el archivo en la ubicación especificada
                    using (var stream = new FileStream(rutaDestino, FileMode.Create))
                    {
                        cv.PdfFile.CopyTo(stream);
                    }

                    cv.NombreArchivo = nombreArchivo;

                    Curriculum? curriculumExistente = (from c in _db.Curriculums
                                                       where c.IdUsuario == idUsuario
                                                       select c ).FirstOrDefault();

                    if (curriculumExistente == null)
                    {
                        _db.Curriculums.Add(cv);
                        _db.SaveChanges();

                        return RedirectToAction("Index", "Usuario");
                    }

                    _db.Update(cv);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "Usuario");
                }
                catch (Exception ex)
                {
                    // Manejar cualquier error
                    ViewBag.Error = "Ocurrió un error al subir el archivo: " + ex.Message;
                }
            }


            return RedirectToAction("Ajustes");
        }
        #endregion

        // HELPERS
        #region POSTULACIONS DEL USUARIO
        public List<PostulacionViewModel> PostulacionesUsuario()
        {
            int idUsuario = int.Parse(HttpContext.Session.GetString("id_usuario"));

            var MisPostulaciones = (
                                    from postulacion in _db.Postulaciones
                                    join oferta in _db.Ofertas  on postulacion.IdOferta equals oferta.IdOferta
                                    where postulacion.IdUsuario == idUsuario
                                    select new PostulacionViewModel
                                    {
                                        titulo = oferta.Titulo,
                                        descripcion = oferta.Descripcion,
                                        estado = postulacion.EstadoPostulacion
                                    }).ToList();


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
                                                            Titulo = oferta.Titulo,
                                                            Descripcion = oferta.Descripcion.PadRight(10),
                                                            FechaPublicada = ObtenerTiempoPublicacion(oferta.FechaPublicacion),
                                                            Requisitos = JObject.Parse(oferta.Requisitos.Replace("'", "\""))
                                                        }
                                      ).ToList();

            return ofertasEncontradas;
        }
        #endregion

        #region  BUSCAR OFERTA INDIVIDUAL
        [HttpGet]
        public OfertaViewModel BuscarOferta(int idOferta){

            OfertaViewModel ofertaPostulada = null;
            List<OfertaViewModel>? ofertaObtenida = (from o in _db.Ofertas
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
                                              }).ToList();


            foreach (var oferta in ofertaObtenida)
            {
                if (oferta.EstasPostulado == "S")
                {
                    ofertaPostulada = oferta;
                    break;
                }
            }

            if (ofertaPostulada == null)
            {
                ofertaPostulada = ofertaObtenida.FirstOrDefault();
            }


            return ofertaPostulada;
        }
        #endregion

    }
}
