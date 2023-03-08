using SuperSold.UI.AspDotNet.Extensions;

namespace SuperSold.UI.AspDotNet.Models;

public class ScrollableViewModel<T> {

    public ScrollableViewModel(IQueryable<T> modelList) {
        ModelList = modelList;
    }

    public IQueryable<T> ModelList { get; set; }
    public string RowFormatPartialViewName { get; set; } = "_BuyableProductRowPartial"; //temp hardcoded

    public int Cursor { get; set; } = 0;
    public async Task<List<T>> NextAsync(int amount) {

        var output = await ModelList
            .Skip(Cursor)
            .Take(amount)
            .ToListAsyncSafe();

        Cursor += amount;
        return output;

    }

}