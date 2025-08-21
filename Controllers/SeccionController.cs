using Microsoft.AspNetCore.Mvc;

namespace MiIT.Controllers
{
    public class SeccionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        

        [HttpGet]
        public JsonResult GetDatosPersonales()
        {
            var datos = new { Nombre = "Armin Daniel Antonio Mendieta", Cedula = "9949257", Carrera = "Infomatica Industrial", Correo = "antonio9949257@gmail.com" };
            return Json(datos);
        }
    }
}
