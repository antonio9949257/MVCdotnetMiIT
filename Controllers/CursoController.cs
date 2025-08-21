using Microsoft.AspNetCore.Mvc;

namespace MiIT.Controllers
{
    public class CursoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}