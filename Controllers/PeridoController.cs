using Microsoft.AspNetCore.Mvc;

namespace MiIT.Controllers
{
    public class PeriodoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetPeriodos()
        {
            var periodos = new List<object>
            {
                new { Semestre = "Primer Semestre", Materias = new List<object> {
                    new { Codigo = "INT-100", Nombre = "INGLÉS TÉCNICO I", Estado = "aprobado" },
                    new { Codigo = "FIS-100", Nombre = "FÍSICA APLICADA", Estado = "aprobado" },
                    new { Codigo = "MCE-100", Nombre = "MEDIDAS Y CIRCUITOS ELECTRÓNICOS", Estado = "aprobado" },
                    new { Codigo = "STI-100", Nombre = "SISTEMAS OPERATIVOS Y TECNOLOGÍA DE LA INFORMACIÓN", Estado = "aprobado" },
                    new { Codigo = "PRG-100", Nombre = "PROGRAMACIÓN I", Estado = "aprobado" },
                    new { Codigo = "ALG-100", Nombre = "ÁLGEBRA LINEAL", Estado = "aprobado" }
                }},
                new { Semestre = "Segundo Semestre", Materias = new List<object> {
                    new { Codigo = "INT-200", Nombre = "INGLÉS TÉCNICO II", Estado = "aprobado" },
                    new { Codigo = "CAL-200", Nombre = "CÁLCULO APLICADO", Estado = "aprobado" },
                    new { Codigo = "ELG-200", Nombre = "ELECTRÓNICA GENERAL I", Estado = "aprobado" },
                    new { Codigo = "PRG-200", Nombre = "PROGRAMACIÓN II", Estado = "aprobado" },
                    new { Codigo = "SID-200", Nombre = "SISTEMAS DIGITALES", Estado = "aprobado" },
                    new { Codigo = "BDD-200", Nombre = "BASES DE DATOS I", Estado = "aprobado" }
                }},
                new { Semestre = "Tercer Semestre", Materias = new List<object> {
                    new { Codigo = "TEW-300", Nombre = "TECNOLOGÍA WEB I", Estado = "aprobado" },
                    new { Codigo = "SIM-300", Nombre = "SISTEMAS MICROPROCESADOS", Estado = "aprobado" },
                    new { Codigo = "ELG-300", Nombre = "ELECTRÓNICA GENERAL II", Estado = "aprobado" },
                    new { Codigo = "PRG-300", Nombre = "PROGRAMACIÓN III", Estado = "aprobado" },
                    new { Codigo = "ELT-300", Nombre = "ELECTROTÉCNIA INDUSTRIAL", Estado = "aprobado" },
                    new { Codigo = "BDD-300", Nombre = "BASES DE DATOS II", Estado = "aprobado" }
                }},
                new { Semestre = "Cuarto Semestre", Materias = new List<object> {
                    new { Codigo = "CAI-400", Nombre = "CONTROL Y AUTOMATIZACIÓN INDUSTRIAL I", Estado = "aprobado" },
                    new { Codigo = "MSI-400", Nombre = "MANTENIMIENTO DE SISTEMAS INDUSTRIALES", Estado = "aprobado" },
                    new { Codigo = "SII-400", Nombre = "SISTEMAS DE INFORMACIÓN I", Estado = "aprobado" },
                    new { Codigo = "TEL-400", Nombre = "TELEMÁTICA I", Estado = "aprobado" },
                    new { Codigo = "PSM-400", Nombre = "PROGRAMACIÓN DE SISTEMAS MÓVILES", Estado = "aprobado" },
                    new { Codigo = "DAC-400", Nombre = "DIBUJO ASISTIDO POR COMPUTADORA", Estado = "aprobado" }
                }}
            };
            return Json(periodos);
        }
    }
}

