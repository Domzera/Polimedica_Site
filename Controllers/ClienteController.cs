using Microsoft.AspNetCore.Mvc;

namespace Polimedica.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
