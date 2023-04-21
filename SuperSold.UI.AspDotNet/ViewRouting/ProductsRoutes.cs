using Microsoft.AspNetCore.Mvc;

namespace SuperSold.UI.AspDotNet.ViewRouting;

public class ProductsRoutes : BaseRoutes {

    public ProductsRoutes(IUrlHelper urlHelper) : base(urlHelper, "Products") { }

    public string? MyProducts() => BuildUrlToAction("MyProducts");
    public string? MyProductsPartial() => BuildUrlToAction("MyProductsPartial");
    public string? Publish() => BuildUrlToAction("Publish");
    public string? Delete() => BuildUrlToAction("Delete");
    public string? Edit() => BuildUrlToAction("Edit");

}