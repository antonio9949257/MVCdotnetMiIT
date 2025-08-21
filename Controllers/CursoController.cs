using Microsoft.AspNetCore.Mvc;
using MiIT.Models; // Added for Curso model

namespace MiIT.Controllers
{
    public class CursoController : Controller
    {
        public IActionResult Index()
        {
            // Populate with the exact data from the previous GetCursos method
            var cursos = new List<Curso>
            {
                new Curso { Codigo = "CAI-500", Nombre = "CONTROL Y AUTOMATIZACIÓN INDUSTRIAL II" },
                new Curso { Codigo = "TEW-500", Nombre = "TECNOLOGÍA WEB II" },
                new Curso { Codigo = "SII-500", Nombre = "SISTEMAS DE INFORMACIÓN II" },
                new Curso { Codigo = "TEL-500", Nombre = "TELEMÁTICA II" },
                new Curso { Codigo = "EMP-500", Nombre = "EMPRENDIMIENTO PRODUCTIVO I" },
                new Curso { Codigo = "TMG-500", Nombre = "TALLER DE MODALIDAD DE GRADUACIÓN I" }
            };
            return View(cursos);
        }
    }
}