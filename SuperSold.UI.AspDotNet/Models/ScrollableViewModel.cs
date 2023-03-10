using SuperSold.UI.AspDotNet.Extensions;

namespace SuperSold.UI.AspDotNet.Models;

public class ScrollableViewModel {

    public ScrollableViewModel(string url) {
        Url = url;
    }

    public string Url { get; set; }

}