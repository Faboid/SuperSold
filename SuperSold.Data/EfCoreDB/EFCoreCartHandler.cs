using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using SuperSold.Data.DBInteractions;
using SuperSold.Data.Models;
using SuperSold.Data.Models.ResponseTypes;

namespace SuperSold.Data.EfCoreDB;

public class EFCoreCartHandler : ICartHandler {

    private readonly EfCoreDBContext _context;

    public EFCoreCartHandler(EfCoreDBContext context) {
        _context = context;
    }

    public IQueryable<ProductModel> QueryCartedProductsByUserId(Guid userId) {

        var products = _context.Cart
            .AsNoTracking()
            .Where(x => x.IdAccount == userId)
            .Select(x => _context.Products.First(y => x.IdProduct == y.IdProduct));

        return products;
    }

    public async Task<OneOf<Success, AlreadyExists>> AddToCart(Guid userId, Guid productId) {

        var cartItem = new AccountCartModel() { IdAccount = userId, IdProduct = productId };

        //check if this item is already in the cart
        if(await _context.Cart.ContainsAsync(cartItem)) {
            return new AlreadyExists();
        }

        await _context.Cart.AddAsync(cartItem);
        await _context.SaveChangesAsync();
        return new Success();

    }

    public async Task<OneOf<Success, NotFound>> RemoveFromCart(Guid userId, Guid productId) {

        var cartItem = await _context.Cart.FirstOrDefaultAsync(x => x.IdAccount == userId && x.IdProduct == productId);
        if(cartItem is null) {
            return new NotFound();
        }

        _context.Cart.Remove(cartItem);
        await _context.SaveChangesAsync();
        return new Success();

    }
}
