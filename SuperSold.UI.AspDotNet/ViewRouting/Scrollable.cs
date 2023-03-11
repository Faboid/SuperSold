using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.ViewRouting;

public static class Scrollable {

    public static async Task<IHtmlContent> ScrollablePartialView<T>(this IHtmlHelper<T> htmlHelper, string url) {
        return await htmlHelper.PartialAsync("_ScrollablePageOnlyPartialView", new ScrollableViewModel(url));
    }

    public static async Task<IHtmlContent> ScrollablePartialView<T>(this IHtmlHelper<T> htmlHelper, string url, string search) {
        return await htmlHelper.PartialAsync("_ScrollablePageSearchPartialView", new ScrollableViewModel(url, search));
    }

}
