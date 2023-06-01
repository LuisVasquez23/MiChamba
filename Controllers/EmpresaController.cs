using Microsoft.AspNetCore.Mvc;

namespace MiChamba.Controllers
{
    public class EmpresaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
