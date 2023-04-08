namespace SuperSold.UI.AspDotNet.Models;

public class ProductWithSavedRelationship {

    public required Product Product { get; set; }
    public required SavedRelationship SavedRelationship { get; set; }

}
