using SuperSold.UI.AspDotNet.Extensions;

namespace SuperSold.UI.AspDotNet.Models;

public class ScrollableViewModel {

    public ScrollableViewModel(string url, string? search = null) {
        Url = url;
        SearchItem = search;
    }

    public string Url { get; set; }
    public string? SearchItem { get; set; }

    /// <summary>
    /// Id generated on model creation to distinguish between multiple scrollable views in the same page.
    /// </summary>
    public string ScrollableId { get; } = Guid.NewGuid().ToString().Replace('-', 'a');

}