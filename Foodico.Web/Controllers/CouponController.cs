using Microsoft.AspNetCore.Mvc;

namespace Foodico.Web.Controllers
{
    public class CouponController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
