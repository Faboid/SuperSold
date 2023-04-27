using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperSold.UI.AspDotNet.Attributes;

namespace SuperSold.UI.AspDotNet.Controllers;

[Authorize]
[AdminOnly]
public class AdminAreaController : Controller {
    public IActionResult Index() {
        return View();
    }
}
