using Microsoft.AspNetCore.Mvc;
using MiIT.Models;

namespace MiIT.Controllers
{
    public class CursoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult CursosJson()
        {
            var cursos = new List<Curso>
            {
                new Curso { idCurso = 1, nombre = "CAI-500 CONTROL Y AUTOMATIZACIÓN INDUSTRIAL II", descripcion = "Descripción de CAI-500", bhabilitado = true },
                new Curso { idCurso = 2, nombre = "TEW-500 TECNOLOGÍA WEB II", descripcion = "Descripción de TEW-500", bhabilitado = true },
                new Curso { idCurso = 3, nombre = "SII-500 SISTEMAS DE INFORMACIÓN II", descripcion = "Descripción de SII-500", bhabilitado = true },
                new Curso { idCurso = 4, nombre = "TEL-500 TELEMÁTICA II", descripcion = "Descripción de TEL-500", bhabilitado = true },
                new Curso { idCurso = 5, nombre = "EMP-500 EMPRENDIMIENTO PRODUCTIVO I", descripcion = "Descripción de EMP-500", bhabilitado = true },
                new Curso { idCurso = 6, nombre = "TMG-500 TALLER DE MODALIDAD DE GRADUACIÓN I", descripcion = "Descripción de TMG-500", bhabilitado = true }
            };
            return Json(cursos);
        }
    }
}