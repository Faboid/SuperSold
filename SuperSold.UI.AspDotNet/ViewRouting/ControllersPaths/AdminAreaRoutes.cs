using Microsoft.AspNetCore.Mvc;

namespace SuperSold.UI.AspDotNet.ViewRouting.ControllersPaths;

public class AdminAreaRoutes : BaseRoutes {
    public AdminAreaRoutes(IUrlHelper urlHelper) : base(urlHelper, "AdminArea") {}

    public string? DeleteProduct() => BuildUrlToAction(nameof(DeleteProduct));

    public string? SearchAccountsByUsername() => BuildUrlToAction(nameof(SearchAccountsByUsername));
    public string? SearchAccountByUsername() => BuildUrlToAction(nameof(SearchAccountByUsername));
    public string? SearchAccountById() => BuildUrlToAction(nameof(SearchAccountById));
    public string? SearchProducts() => BuildUrlToAction(nameof(SearchProducts));

}
