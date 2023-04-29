using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace SuperSold.UI.AspDotNet.ViewRouting.ViewComponents;

public abstract class BaseViewComponentBuilder {

    protected readonly IViewComponentHelper VCHelper;
    protected readonly string VCName;
    protected BaseViewComponentBuilder(IViewComponentHelper vcHelper, string vcName) {
        VCHelper = vcHelper;
        VCName = vcName;
    }

    protected async Task<IHtmlContent> Build(object? args) => await VCHelper.InvokeAsync(VCName, args);

}
