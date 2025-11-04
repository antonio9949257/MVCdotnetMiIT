using Microsoft.AspNetCore.Mvc;
using MiIT.Data;
using MiIT.Models;
using System.Text.Json;

namespace MiIT.Controllers
{
    public class CursoController : Controller
    {
        private readonly MatriculasContext _context;

        public CursoController(MatriculasContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult CursosJson()
        {
            var cursos = _context.Curso.ToList();
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            return Json(cursos, options);
        }
    }
}