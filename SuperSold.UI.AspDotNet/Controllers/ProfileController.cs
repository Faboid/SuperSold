using Microsoft.AspNetCore.Mvc;

namespace SuperSold.UI.AspDotNet.Controllers;
public class ProfileController : Controller {
    public IActionResult Index() {
        return View();
    }
}
