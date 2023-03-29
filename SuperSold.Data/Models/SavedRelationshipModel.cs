using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SuperSold.Data.Models;

/// <summary>
/// Defines the relationship between <see cref="AccountModel"/> and <see cref="ProductModel"/>.
/// </summary>
[Table("saved_relationships")]
public class SavedRelationshipModel {

    public SavedRelationshipModel(){}

    [SetsRequiredMembers]
    public SavedRelationshipModel(Guid idUser, Guid idProduct, RelationshipType relationshipType, int quantity = 1) {
        IdRelationship = Guid.NewGuid();
        IdUser = idUser;
        IdProduct = idProduct;
        RelationshipType = relationshipType;
        Quantity = quantity;
    }

    [Key]
    public required Guid IdRelationship { get; set; }

    [ForeignKey(nameof(AccountModel.IdAccount))]
    public required Guid IdUser { get; set; }

    [ForeignKey(nameof(ProductModel.IdProduct))]
    public required Guid IdProduct { get; set; }

    [Column(TypeName = "varchar(20)")]
    public required RelationshipType RelationshipType { get; set; }
    public int Quantity { get; set; } = 1;

}

public enum RelationshipType {
    Cart,
    Wishlist
}