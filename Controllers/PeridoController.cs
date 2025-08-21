using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MiIT.Models; // Added for Periodo and Materia models

namespace MiIT.Controllers
{
    public class PeriodoController : Controller
    {
        public IActionResult Index()
        {
            var periodos = new List<Periodo>
            {
                new Periodo { Semestre = "Primer Semestre", Materias = new List<Materia> {
                    new Materia { Codigo = "INT-100", Nombre = "INGLÉS TÉCNICO I", Estado = "aprobado" },
                    new Materia { Codigo = "FIS-100", Nombre = "FÍSICA APLICADA", Estado = "aprobado" },
                    new Materia { Codigo = "MCE-100", Nombre = "MEDIDAS Y CIRCUITOS ELECTRÓNICOS", Estado = "aprobado" },
                    new Materia { Codigo = "STI-100", Nombre = "SISTEMAS OPERATIVOS Y TECNOLOGÍA DE LA INFORMACIÓN", Estado = "aprobado" },
                    new Materia { Codigo = "PRG-100", Nombre = "PROGRAMACIÓN I", Estado = "aprobado" },
                    new Materia { Codigo = "ALG-100", Nombre = "ÁLGEBRA LINEAL", Estado = "aprobado" }
                }},
                new Periodo { Semestre = "Segundo Semestre", Materias = new List<Materia> {
                    new Materia { Codigo = "INT-200", Nombre = "INGLÉS TÉCNICO II", Estado = "aprobado" },
                    new Materia { Codigo = "CAL-200", Nombre = "CÁLCULO APLICADO", Estado = "aprobado" },
                    new Materia { Codigo = "ELG-200", Nombre = "ELECTRÓNICA GENERAL I", Estado = "aprobado" },
                    new Materia { Codigo = "PRG-200", Nombre = "PROGRAMACIÓN II", Estado = "aprobado" },
                    new Materia { Codigo = "SID-200", Nombre = "SISTEMAS DIGITALES", Estado = "aprobado" },
                    new Materia { Codigo = "BDD-200", Nombre = "BASES DE DATOS I", Estado = "aprobado" }
                }},
                new Periodo { Semestre = "Tercer Semestre", Materias = new List<Materia> {
                    new Materia { Codigo = "TEW-300", Nombre = "TECNOLOGÍA WEB I", Estado = "aprobado" },
                    new Materia { Codigo = "SIM-300", Nombre = "SISTEMAS MICROPROCESADOS", Estado = "aprobado" },
                    new Materia { Codigo = "ELG-300", Nombre = "ELECTRÓNICA GENERAL II", Estado = "aprobado" },
                    new Materia { Codigo = "PRG-300", Nombre = "PROGRAMACIÓN III", Estado = "aprobado" },
                    new Materia { Codigo = "ELT-300", Nombre = "ELECTROTÉCNIA INDUSTRIAL", Estado = "aprobado" },
                    new Materia { Codigo = "BDD-300", Nombre = "BASES DE DATOS II", Estado = "aprobado" }
                }},
                new Periodo { Semestre = "Cuarto Semestre", Materias = new List<Materia> {
                    new Materia { Codigo = "CAI-400", Nombre = "CONTROL Y AUTOMATIZACIÓN INDUSTRIAL I", Estado = "aprobado" },
                    new Materia { Codigo = "MSI-400", Nombre = "MANTENIMIENTO DE SISTEMAS INDUSTRIALES", Estado = "aprobado" },
                    new Materia { Codigo = "SII-400", Nombre = "SISTEMAS DE INFORMACIÓN I", Estado = "aprobado" },
                    new Materia { Codigo = "TEL-400", Nombre = "TELEMÁTICA I", Estado = "aprobado" },
                    new Materia { Codigo = "PSM-400", Nombre = "PROGRAMACIÓN DE SISTEMAS MÓVILES", Estado = "aprobado" },
                    new Materia { Codigo = "DAC-400", Nombre = "DIBUJO ASISTIDO POR COMPUTADORA", Estado = "aprobado" }
                }}
            };
            return View(periodos);
        }
    }
}

