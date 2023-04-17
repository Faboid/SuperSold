using Microsoft.AspNetCore.Mvc;

namespace SuperSold.UI.AspDotNet.ViewRouting;

public class PurchaseRoutes {

    private readonly IUrlHelper _urlHelper;

    public PurchaseRoutes(IUrlHelper urlHelper) {
        _urlHelper = urlHelper;
    }

    public string? Index() => _urlHelper.Action("Index", "Purchase");
    public string? SubmitPurchase() => _urlHelper.Action("SubmitPurchase", "Purchase");

}