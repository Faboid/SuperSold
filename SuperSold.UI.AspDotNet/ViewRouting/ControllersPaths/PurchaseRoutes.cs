using Microsoft.AspNetCore.Mvc;

namespace SuperSold.UI.AspDotNet.ViewRouting.ControllersPaths;

public class PurchaseRoutes : BaseRoutes
{

    public PurchaseRoutes(IUrlHelper urlHelper) : base(urlHelper, "Purchase") { }

    public string? Index() => BuildUrlToAction("Index");
    public string? SubmitPurchase() => BuildUrlToAction("SubmitPurchase");

}