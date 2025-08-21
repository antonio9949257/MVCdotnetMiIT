using Microsoft.AspNetCore.Mvc;

namespace MiIT.Controllers
{
    public class CursoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetCursos()
        {
            // En una aplicación real, estos datos vendrían de una base de datos.
            var cursos = new List<object>
            {
                new { Codigo = "CAI-500", Nombre = "CONTROL Y AUTOMATIZACIÓN INDUSTRIAL II" },
                new { Codigo = "TEW-500", Nombre = "TECNOLOGÍA WEB II" },
                new { Codigo = "SII-500", Nombre = "SISTEMAS DE INFORMACIÓN II" },
                new { Codigo = "TEL-500", Nombre = "TELEMÁTICA II" },
                new { Codigo = "EMP-500", Nombre = "EMPRENDIMIENTO PRODUCTIVO I" },
                new { Codigo = "TMG-500", Nombre = "TALLER DE MODALIDAD DE GRADUACIÓN I" }
            };
            return Json(cursos);
        }
    }
}