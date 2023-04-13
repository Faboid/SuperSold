using Microsoft.AspNetCore.Mvc;

namespace SuperSold.UI.AspDotNet.Controllers;
public class PurchaseController : Controller {
    public IActionResult Index() {
        return View();
    }

}
