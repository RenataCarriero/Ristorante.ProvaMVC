using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ristorante.ProvaMVC.WebbAppMVC.Controllers
{
    [Authorize]
    public class ContattiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
