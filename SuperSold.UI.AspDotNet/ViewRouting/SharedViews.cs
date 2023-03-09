using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.ViewRouting;

public static class SharedViews {

    public static IActionResult ProductListPartialView(this Controller controller, string rowPartialName, IEnumerable<Product> products) {
        return controller.PartialView("_ProductListPartialView", (rowPartialName, products));
    }

}
