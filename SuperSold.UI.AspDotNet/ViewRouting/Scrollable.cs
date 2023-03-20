using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using OneOf;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.ViewRouting;

public static class Scrollable {

    public static async Task<IHtmlContent> ScrollablePartialView<T>(this IHtmlHelper<T> htmlHelper, string url, OneOf<Guid, string>? customId = null) {
        return await htmlHelper.PartialAsync("_ScrollablePageOnlyPartialView", new ScrollableViewModel(url, customId ?? Guid.NewGuid()));
    }

    public static async Task<IHtmlContent> ScrollablePartialView<T>(this IHtmlHelper<T> htmlHelper, string url, string search, OneOf<Guid, string>? customId = null) {
        return await htmlHelper.PartialAsync("_ScrollablePageSearchPartialView", new ScrollableViewModel(url, customId ?? Guid.NewGuid(), search));
    }

}
