using OneOf;
using OneOf.Types;
using SuperSold.Data.DBInteractions;
using SuperSold.Data.Extensions;
using SuperSold.Data.Models;

namespace SuperSold.Data.EfCoreDB;

public class EFCoreCartHandler : ICartHandler {

    private readonly EfCoreDBContext _context;
    private readonly ISavedRelationshipsHandler _relationships;

    public EFCoreCartHandler(EfCoreDBContext context, ISavedRelationshipsHandler relationships) {
        _context = context;
        _relationships = relationships;
    }

    public IQueryable<ProductModel> QueryCartedProductsByUserId(Guid userId) {
        return _relationships
            .QuerySavedRelationshipsByUserId(userId)
            .WhereIs(RelationshipType.Cart)
            .Select(x => _context.Products.First(y => x.IdProduct == y.IdProduct));
    }

    public async Task<OneOf<Success, Error>> AddToCart(Guid userId, Guid productId) {
        return await _relationships.AddRelationship(new(userId, productId, RelationshipType.Cart));
    }

    public async Task<OneOf<Success, NotFound>> RemoveFromCart(Guid userId, Guid productId) {
        return await _relationships.RemoveRelationship(userId, productId);
    }

    public async Task<OneOf<Success, NotFound>> MoveToWishlist(Guid userId, Guid productId) {
        return await _relationships.UpdateRelationshipType(userId, productId, RelationshipType.Cart);
    }

}
