using OneOf;
using SuperSold.UI.AspDotNet.Extensions;

namespace SuperSold.UI.AspDotNet.Models;

public class ScrollableViewModel {

    public ScrollableViewModel(string url, OneOf<Guid, string> scrollId, string? search = null) {
        Url = url;
        SearchItem = search;
        ScrollableId = scrollId.Match(
            guid => guid.ToString().Replace('-', 'a'),
            s => s
        );
    }

    public string Url { get; set; }
    public string? SearchItem { get; set; }

    /// <summary>
    /// Id to distinguish between multiple scrollable views in the same page.
    /// </summary>
    public string ScrollableId { get; }

}