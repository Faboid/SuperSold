using SuperSold.Data.Models;

namespace SuperSold.Data.Extensions;
internal static class IQueryableExtensions {

    public static IQueryable<SavedRelationshipModel> WhereIsCart(this IQueryable<SavedRelationshipModel> query) => query.WhereIs(RelationshipType.Cart);
    public static IQueryable<SavedRelationshipModel> WhereIsWishlist(this IQueryable<SavedRelationshipModel> query) => query.WhereIs(RelationshipType.Wishlist);
    public static IQueryable<SavedRelationshipModel> WhereIs(this IQueryable<SavedRelationshipModel> query, RelationshipType type) => query.Where(x => x.RelationshipType == type);

}
