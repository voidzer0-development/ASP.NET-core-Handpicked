using Microsoft.AspNetCore.Mvc;

namespace B2Handpicked.UI.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
