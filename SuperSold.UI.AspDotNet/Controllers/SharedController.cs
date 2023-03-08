using Microsoft.AspNetCore.Mvc;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.Controllers;
public class SharedController : Controller {
    
    public IActionResult RenderProductList(string rowPartialName, ICollection<Product> products) {
        return PartialView("_ProductListPartialView", (rowPartialName, products));
    }

}
