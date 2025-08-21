using Microsoft.AspNetCore.Mvc;

namespace MiIT.Controllers
{
    public class SeccionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
