using OneOf;
using OneOf.Types;
using SuperSold.Data.Models;

namespace SuperSold.Data.DBInteractions;
public interface ISavedRelationshipsHandler {

    /// <summary>
    /// Retrieves all the relationships owned by the given user id.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    IQueryable<SavedRelationshipModel> QuerySavedRelationshipsByUserId(Guid userId);

    /// <summary>
    /// Retrieves relationship by id.
    /// </summary>
    /// <param name="relationshipId"></param>
    /// <returns></returns>
    Task<OneOf<SavedRelationshipModel, NotFound>> GetRelationship(Guid relationshipId);

    /// <summary>
    /// Retrieves relationship by user and product ids.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="productId"></param>
    /// <returns></returns>
    Task<OneOf<SavedRelationshipModel, NotFound>> GetRelationship(Guid userId, Guid productId);

    /// <summary>
    /// Will create a relationship. 
    /// If it already exists, will add one to stored <see cref="SavedRelationshipModel.Quantity"/> 
    /// and update <see cref="SavedRelationshipModel.RelationshipType"/> to <paramref name="relationship"/>.RelationshipType.
    /// </summary>
    /// <param name="relationship"></param>
    /// <returns></returns>
    Task<OneOf<Success, Error>> AddRelationship(SavedRelationshipModel relationship);

    /// <summary>
    /// Will update relationship type.
    /// </summary>
    /// <param name="relationship"></param>
    /// <returns></returns>
    Task<OneOf<Success, NotFound>> UpdateRelationshipType(Guid relationshipId, RelationshipType type);

    /// <summary>
    /// Will update relationship type.
    /// </summary>
    /// <param name="relationship"></param>
    /// <returns></returns>
    Task<OneOf<Success, NotFound>> UpdateRelationshipType(Guid userId, Guid productId, RelationshipType type);

    /// <summary>
    /// Will update relationship quantity. If quantity is set to 0, the relationship will be deleted instead.
    /// </summary>
    /// <param name="relationshipId"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    Task<OneOf<Success, NotFound>> UpdateRelationshipQuantity(Guid relationshipId, int quantity);

    /// <summary>
    /// Will update relationship quantity. If quantity is set to 0, the relationship will be deleted instead.
    /// </summary>
    /// <param name="relationshipId"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    Task<OneOf<Success, NotFound>> UpdateRelationshipQuantity(Guid userId, Guid productId, int quantity);

    /// <summary>
    /// Will delete the relationship with the given id.
    /// </summary>
    /// <param name="relationshipId"></param>
    /// <returns></returns>
    Task<OneOf<Success, NotFound>> RemoveRelationship(Guid relationshipId);

    /// <summary>
    /// Will delete the relationship between the user and product using the given ids.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="relationshipId"></param>
    /// <returns></returns>
    Task<OneOf<Success, NotFound>> RemoveRelationship(Guid userId, Guid productId);


}
