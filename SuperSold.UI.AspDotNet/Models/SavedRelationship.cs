using SuperSold.Data.Models;

namespace SuperSold.UI.AspDotNet.Models;

public class SavedRelationship {

    public required Guid IdRelationship { get; set; }
    public required Guid IdUser { get; set; }
    public required Guid IdProduct { get; set; }
    public required RelationshipType RelationshipType { get; set; }
    public int Quantity { get; set; } = 1;

}