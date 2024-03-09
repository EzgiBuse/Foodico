using Microsoft.AspNetCore.Mvc;

namespace Foodico.Web.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
