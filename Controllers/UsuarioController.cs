using MiChamba.Data;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region REGISTRO - GET
        public IActionResult Registro()
        {
            return View();
        }
        #endregion

        #region LOGIN - GET
        public IActionResult Login()
        {
            return View();
        }
        #endregion

        public IActionResult Ofertas() {
            return View();
        }

        public IActionResult Ajustes() {
            return View();
        }

        public IActionResult ModificacionPerfil()
        {
            return View();
        }


    }
}
