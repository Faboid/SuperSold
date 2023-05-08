namespace SuperSold.UI.AspDotNet.Models;

public class ProductWithSavedRelationship {

    public required Product Product { get; set; }
    public required SavedRelationship SavedRelationship { get; set; }

    //todo - extract the methods below somewhere else

    /// <summary>
    /// Returns a string with product id and quantity.
    /// </summary>
    /// <returns></returns>
    public string ToBasicString() {
        return $"[{Product.Id}]-{SavedRelationship.Quantity}";
    }

    /// <summary>
    /// Parses a previously built string from <see cref="ToBasicString"/> and returns the stored product id and quantity.
    /// </summary>
    /// <param name="basicString"></param>
    /// <returns></returns>
    public static BasicProductRelationship ParseBasicString(string basicString) {
        var endId = basicString.IndexOf(']');
        var guid = new Guid(basicString[1..endId]);
        var quantity = int.Parse(basicString[(endId + 2)..]);
        return new(guid, quantity);
    }

    public record struct BasicProductRelationship(Guid ProductId, int Quantity);

}
