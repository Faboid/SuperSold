using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.ViewRouting.ViewComponents;

public class ProductRowBuilder : BaseViewComponentBuilder {
    public ProductRowBuilder(IViewComponentHelper vcHelper) : base(vcHelper, "ProductRow") { }

    public async Task<IHtmlContent> AdminManagementRow(Product product) => await Build(new { rowTemplateName = nameof(AdminManagementRow), product });

}