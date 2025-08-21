using Microsoft.AspNetCore.Mvc;
using MiIT.Models; // Added for DatosPersonales model

namespace MiIT.Controllers
{
    public class SeccionController : Controller
    {
        public IActionResult Index()
        {
            var datos = new DatosPersonales { Nombre = "Armin Daniel Antonio Mendieta", Cedula = "9949257", Carrera = "Infomatica Industrial", Correo = "antonio9949257@gmail.com" };
            return View(datos);
        }
    }
}
