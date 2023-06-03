using MiChamba.Data;
using MiChamba.Models;
using MiChamba.ViewModel;
using Microsoft.AspNetCore.Http;
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

        // VISTAS
        #region INDEX - GET 
        public IActionResult Index()
        {
            if (!VerifyEmpresaLogin())
            {
                return RedirectToAction("Login");
            }

            ViewBag.foto = HttpContext.Session.GetString("foto");

            return View();
        }
        #endregion

        #region LOGIN - GET
        [HttpGet]
        public IActionResult Login()
        {
            if (VerifyEmpresaLogin())
            {
                return RedirectToAction("Index");
            }

            return View();
        }
        #endregion

        #region LOGIN - POST
        [HttpPost]
        public IActionResult Login(Empresa empresa)
        {

            string email = empresa.Email;
            string password = empresa.Password;

            var empresaGuardada = (from emp in _db.Empresas
                                   where emp.Email == email & emp.Password == password
                                   select new
                                   {
                                       emp.IdEmpresa,
                                       emp.Imagen,
                                       emp.Nombre
                                   }).FirstOrDefault();

            if (empresaGuardada == null)
            {
                return RedirectToAction("Login");
            }

            HttpContext.Session.SetString("id_empresa", empresaGuardada.IdEmpresa.ToString());
            HttpContext.Session.SetString("nombre_empresa", empresaGuardada.Nombre );
            HttpContext.Session.SetString("foto", empresaGuardada.Imagen);

            return RedirectToAction("Index", "Empresa");
        }
        #endregion

        #region LOG-OUT - GET    
        [HttpGet]
        public IActionResult LogOut()
        {

            if (!VerifyEmpresaLogin())
            {
                return RedirectToAction("Index");
            }

            HttpContext.Session.Remove("id_empresa");

            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region REGISTRO - GET
        public IActionResult Registro()
        {
            if (VerifyEmpresaLogin())
            {
                return RedirectToAction("Index");
            }

            Empresa empresa = new Empresa();

            return View(empresa);
        }
        #endregion

        #region REGISTRO - POST
        [HttpPost]
        public IActionResult Registro(Empresa empresa)
        {
            empresa.Imagen = "default";

            // Guardar la imagen en el servidor
            if (empresa.ImagenFile != null && empresa.ImagenFile.Length > 0)
            {

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "img");
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(empresa.ImagenFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    empresa.ImagenFile.CopyTo(stream);
                }

                // Actualizar la propiedad Imagen con la ruta relativa del archivo guardado
                empresa.Imagen = fileName;
            }

            empresa.Direccion = empresa.Departamento + " " + empresa.Estado;

            // Registrar el usuario en el contexto de la base de datos
            _db.Empresas.Add(empresa);
            _db.SaveChanges();

            return RedirectToAction("Index", "Empresa");
        }
        #endregion

        #region ADMINISTRAR POSTULACIONES
        public IActionResult AdministrarPostulaciones()
        {
            if (VerifyEmpresaLogin())
            {
                return RedirectToAction("Index");
            }

            return View();
        }
        #endregion

        #region CREAR OFERTA  - GET
        public IActionResult CrearOferta()
        {
            if (!VerifyEmpresaLogin())
            {
                return RedirectToAction("Login");
            }

            ViewBag.foto = HttpContext.Session.GetString("foto");

            return View();
        }
        #endregion

        #region 
        [HttpPost]
        public IActionResult CrearOferta(Oferta oferta)
        {
            try
            {
                oferta.Requisitos = "{'educacion': '" + oferta.Educacion + "',"
                                + "'experiencia': '" + oferta.Experiencia + "',"
                                + "'habilidades': ['" + oferta.Habilidades.Replace(",", "','") + "']}".Replace("'","\"");

                oferta.IdEmpresa = int.Parse(HttpContext.Session.GetString("id_empresa"));

                oferta.FechaPublicacion = DateTime.Now;

                _db.Ofertas.Add(oferta);
                _db.SaveChanges();
            }
            catch(Exception e)
            {

            }



            return RedirectToAction("Index");
        }
        #endregion

        // HELPERS
        #region OFERTAS DISPONIBLES
        public List<OfertaViewModel> OfertasDisponibles(int idEmpresa)
        {
            var ofertas = (from oferta in _db.Ofertas
                           where oferta.IdEmpresa == idEmpresa
                           select new OfertaViewModel
                           {
                               IdOferta = oferta.IdOferta,
                               Titulo = oferta.Titulo,
                               Descripcion = oferta.Descripcion.PadRight(10),
                               FechaPublicada = ObtenerTiempoPublicacion(oferta.FechaPublicacion),
                               Ciudad = oferta.Ubicacion,
                               Requisitos = JObject.Parse(oferta.Requisitos)

                           }).ToList();

            return ofertas;
        }
        #endregion
    }
}
