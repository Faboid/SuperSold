using SuperSold.UI.AspDotNet.Extensions;

namespace SuperSold.UI.AspDotNet.Models;

public class ScrollableViewModel {

    public ScrollableViewModel(string url, string? search = null) {
        Url = url;
        SearchItem = search;
    }

    public string Url { get; set; }
    public string? SearchItem { get; set; }

}