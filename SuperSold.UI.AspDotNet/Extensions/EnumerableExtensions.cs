namespace SuperSold.UI.AspDotNet.Extensions;

public static class EnumerableExtensions {

    public static string JoinAsStrings(this IEnumerable<string> enumerable, char separator) {
        return string.Join(separator, enumerable);
    }

}
