using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.Controllers;
public class ProductsController : Controller {

    [HttpGet]
    [Authorize]
    public IActionResult MyProducts() {

        if(User.Identity!.Name != "admin") {
            return View(new List<Product>());
        }

        var listProducts = new List<Product> {
            new() { Id = Guid.NewGuid(), Title = "Something nice", Description = "A nice description", Price = 10, UserImgUrl = "" },
            new() { Id = Guid.NewGuid(), Title = "Xbox", Description = "Used xbox with minor scratches.", Price = 280.32M, UserImgUrl = "" }
        };

        return View(listProducts);
    }

}
