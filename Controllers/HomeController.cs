using MiChamba.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MiChamba.Controllers
{
    public class HomeController : BaseController<HomeController>
    {

        public HomeController(MiChambaDbContext db) {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }


    }
}