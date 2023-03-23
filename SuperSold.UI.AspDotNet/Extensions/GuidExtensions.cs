namespace SuperSold.UI.AspDotNet.Extensions;

public static class GuidExtensions {

    public static string ToValidHtml(this Guid guid) => guid.ToString().Replace('-', 'a');

}
