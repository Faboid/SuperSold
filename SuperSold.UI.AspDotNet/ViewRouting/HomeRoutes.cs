using Microsoft.AspNetCore.Mvc;

namespace SuperSold.UI.AspDotNet.ViewRouting;

public class HomeRoutes {

    private readonly IUrlHelper _urlHelper;

    public HomeRoutes(IUrlHelper urlHelper) {
        _urlHelper = urlHelper;
    }

    public string? Index() => _urlHelper.Action("Index", "Home");
    public string? IndexPartial() => _urlHelper.Action("IndexPartial", "Home");
    public string? Search() => _urlHelper.Action("Search", "Home");
    public string? SearchPartial() => _urlHelper.Action("SearchPartial", "Home");
    public string? Privacy() => _urlHelper.Action("Privacy", "Home");
    public string? Error() => _urlHelper.Action("Error", "Home");
    public string? TermsAndConditions() => _urlHelper.Action("TermsAndConditions", "Home");


}
