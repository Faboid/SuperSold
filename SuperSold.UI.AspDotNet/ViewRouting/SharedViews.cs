using Microsoft.AspNetCore.Mvc;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.ViewRouting;

public static class SharedViews {

    public static IActionResult ProductListPartialView(this Controller controller, string rowType, IEnumerable<Product> products) {
        return controller.PartialView("_ProductListPartialView", (rowType, products));
    }

}
