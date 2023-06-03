using MiChamba.Data;
using MiChamba.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MiChamba.Controllers
{
    public class EmpresaController : BaseController<EmpresaController>
    {
        public EmpresaController(MiChambaDbContext db)
        {
            _db = db;

        }
        public IActionResult Index()
        {
            return View();
        }

       public IActionResult AdministrarPostulaciones()
        {
            return View();
        }
        public List<object> OfertasDisponibles()
        {
            var oferta = _db.Ofertas.FirstOrDefault(o => o.IdOferta == 1);

            if (oferta != null)
            {
                // Acceder al JSON almacenado en la propiedad Requisitos
                var requisitosJson = oferta.Requisitos;

                // Aquí puedes realizar las operaciones necesarias con el JSON, como parsearlo o utilizarlo directamente
                // Por ejemplo, puedes convertirlo a un objeto o mostrarlo en tu vista

                return oferta;
            }


            return null;
        }
    }
}
