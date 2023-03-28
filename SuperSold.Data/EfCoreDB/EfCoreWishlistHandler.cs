using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using SuperSold.Data.DBInteractions;
using SuperSold.Data.Extensions;
using SuperSold.Data.Models;

namespace SuperSold.Data.EfCoreDB;
public class EfCoreWishlistHandler : IWishlistHandler {

    private readonly EfCoreDBContext _context;
    private readonly ISavedRelationshipsHandler _relationships;

    public EfCoreWishlistHandler(EfCoreDBContext context, ISavedRelationshipsHandler relationships) {
        _context = context;
        _relationships = relationships;
    }

    public IQueryable<ProductModel> QueryWishlistedProductsByUserId(Guid userId) {
        return _relationships
            .QuerySavedRelationshipsByUserId(userId)
            .WhereIs(RelationshipType.Wishlist)
            .Select(x => _context.Products.First(y => x.IdProduct == y.IdProduct));
    }

    public async Task<OneOf<Success, Error>> WishlistProduct(Guid userId, Guid productId) {
        return await _relationships.AddRelationship(new(userId, productId, RelationshipType.Wishlist));
    }

    public async Task<OneOf<Success, NotFound>> RemoveWishlistProduct(Guid userId, Guid productId) {
        return await _relationships.RemoveRelationship(userId, productId);
    }

    public async Task<OneOf<Success, NotFound>> MoveToCart(Guid userId, Guid productId) {
        return await _relationships.UpdateRelationshipType(userId, productId, RelationshipType.Wishlist);
    }

}