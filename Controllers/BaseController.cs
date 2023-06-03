using MiChamba.Data;
using Microsoft.AspNetCore.Mvc;

namespace MiChamba.Controllers
{
    public abstract class BaseController<T> : Controller where T : BaseController<T>
    {
        //Controlador base
        //Logs
        private ILogger<T>? _logger;
        protected ILogger<T>? Logger => _logger ?? (_logger = HttpContext.RequestServices.GetService<ILogger<T>>());
        protected MiChambaDbContext? _db;

        public bool VerifyUserLogin()
        {
            var idUsuario = HttpContext.Session.GetString("id_usuario") ?? "";

            if (idUsuario == "")
            {
                return false;
            }

            TempData["nombre_usuario"] = HttpContext.Session.GetString("nombre_usuario");

            return true;
        }

        public bool VerifyEmpresaLogin()
        {
            var idUsuario = HttpContext.Session.GetString("id_empresa") ?? "";

            if (idUsuario == "")
            {
                return false;
            }

            TempData["nombre_empresa"] = HttpContext.Session.GetString("nombre_empresa");

            return true;
        }

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
