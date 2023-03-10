using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.ViewRouting;

public static class Scrollable {

    public static async Task<IHtmlContent> ScrollablePageOnly<T>(this IHtmlHelper<T> htmlHelper, string url) {
        return await htmlHelper.PartialAsync("ScrollableViewCollection", new ScrollableViewModel(url));
    }

    public static Task<IHtmlContent> ScrollablePageSearch(string search) {
        throw new NotImplementedException();
    }

}
