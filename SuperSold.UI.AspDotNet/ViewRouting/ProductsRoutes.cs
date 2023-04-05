using Microsoft.AspNetCore.Mvc;

namespace SuperSold.UI.AspDotNet.ViewRouting;

public class ProductsRoutes {

    private readonly IUrlHelper _urlHelper;

    public ProductsRoutes(IUrlHelper urlHelper) {
        _urlHelper = urlHelper;
    }

    public string? MyProducts() => _urlHelper.Action("MyProducts", "Products");
    public string? MyProductsPartial() => _urlHelper.Action("MyProductsPartial", "Products");
    public string? Publish() => _urlHelper.Action("Publish", "Products");
    public string? Delete() => _urlHelper.Action("Delete", "Products");
    public string? Edit() => _urlHelper.Action("Edit", "Products");


}