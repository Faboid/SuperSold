using Microsoft.AspNetCore.Mvc.Rendering;

namespace SuperSold.UI.AspDotNet.ViewRouting.PartialViews;

public static class PartialBuilder {

    public static PartialPurchaseRoutes PartialPurchase(this IHtmlHelper htmlHelper) => new(htmlHelper);

}