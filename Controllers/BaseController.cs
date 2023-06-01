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


    }
}
