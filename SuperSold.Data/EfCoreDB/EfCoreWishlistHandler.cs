using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using SuperSold.Data.DBInteractions;
using SuperSold.Data.Models;
using SuperSold.Data.Models.ResponseTypes;

namespace SuperSold.Data.EfCoreDB;
public class EfCoreWishlistHandler : IWishlistHandler {

    private readonly EfCoreDBContext _context;

    public EfCoreWishlistHandler(EfCoreDBContext context) {
        _context = context;
    }

    public IQueryable<ProductModel> QueryWishlistedProductsByUserId(Guid userId) {

        var products = _context.Wishlists
            .AsNoTracking()
            .Where(x => x.IdAccount == userId)
            .Select(x => _context.Products.First(y => x.IdProduct == y.IdProduct));

        return products;

    }

    public async Task<OneOf<Success, NotFound>> RemoveWishlistProduct(Guid userId, Guid productId) {
        
        var wishlist = await _context.Wishlists.FirstOrDefaultAsync(x => x.IdAccount == userId && x.IdProduct == productId);
        if(wishlist is null) {
            return new NotFound();
        }

        _context.Wishlists.Remove(wishlist);
        await _context.SaveChangesAsync();
        return new Success();

    }

    public async Task<OneOf<Success, AlreadyExists>> WishlistProduct(Guid userId, Guid productId) {

        var wishlist = new AccountWishlistModel() { IdAccount = userId, IdProduct = productId };

        if(await _context.Wishlists.ContainsAsync(wishlist)) {
            return new AlreadyExists();
        }

        await _context.Wishlists.AddAsync(wishlist);
        await _context.SaveChangesAsync();
        return new Success();

    }


}