using Microsoft.AspNetCore.Mvc;

namespace SuperSold.UI.AspDotNet.ViewRouting.ControllersPaths;

public class HomeRoutes : BaseRoutes
{

    public HomeRoutes(IUrlHelper urlHelper) : base(urlHelper, "Home") { }

    public string? Index() => BuildUrlToAction("Index");
    public string? IndexPartial() => BuildUrlToAction("IndexPartial");
    public string? Search() => BuildUrlToAction("Search");
    public string? SearchPartial() => BuildUrlToAction("SearchPartial");
    public string? Privacy() => BuildUrlToAction("Privacy");
    public string? Error() => BuildUrlToAction("Error");
    public string? TermsAndConditions() => BuildUrlToAction("TermsAndConditions");

}
