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

        #region PERFIL - USUARIO
        public IActionResult perfilUsuario()
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
    }
}
