namespace SuperSold.UI.AspDotNet.Extensions;

public static class EnumerableExtensions {

    public static string JoinAsStrings(this IEnumerable<string> enumerable, char separator) {
        return string.Join(separator, enumerable);
    }

    public static async Task<List<T>> ToListAsync<T>(this IEnumerable<Task<T>> enumerable) {

        var output = new List<T>();
        foreach(var item in enumerable) {
            output.Add(await item);
        }

        return output;

    }

}
