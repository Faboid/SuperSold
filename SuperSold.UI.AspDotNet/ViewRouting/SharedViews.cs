using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SuperSold.Data.Models;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.ViewRouting;

public static class SharedViews {

    public static IActionResult ProductListPartialView(this Controller controller, string rowType, IEnumerable<Product> products) {
        return controller.PartialView("_ProductListPartialView", (rowType, products));
    }

    public static IActionResult ProductListPartialView(this Controller controller, string rowType, Product product) {
        return controller.ProductListPartialView(rowType, new Product[] { product });
    }

    public static IActionResult RelationshipListPartialView(this Controller controller, string rowType, IEnumerable<ProductWithSavedRelationship> products) {
        return controller.PartialView("_RelationshipListPartialView", (rowType, products));
    }

    public static async Task<IHtmlContent> RelationshipListPartialView(this IHtmlHelper html, string rowType, IEnumerable<ProductWithSavedRelationship> products) {
        return await html.PartialAsync("_RelationshipListPartialView", (rowType, products));
    }

    public static IActionResult RelationshipListPartialView(this Controller controller, string rowType, ProductWithSavedRelationship product) {
        return controller.RelationshipListPartialView(rowType, new ProductWithSavedRelationship[] { product });
    }

}
