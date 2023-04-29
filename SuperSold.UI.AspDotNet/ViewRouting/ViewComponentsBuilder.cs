using Microsoft.AspNetCore.Mvc;
using SuperSold.UI.AspDotNet.ViewRouting.ViewComponents;

namespace SuperSold.UI.AspDotNet.ViewRouting;

public static class ViewComponentsBuilder {

    public static ProductRowBuilder ProductRow(this IViewComponentHelper helper) => new(helper);
    public static AccountDisplayBuilder AccountDisplay(this IViewComponentHelper helper) => new(helper);

}
