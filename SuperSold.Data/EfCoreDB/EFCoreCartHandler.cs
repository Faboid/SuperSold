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
        var products = _context.Products
            .AsNoTracking()
            .GroupJoin(_context.Cart.Where(x => x.AccountId == userId), x => x.IdProduct, x => x.ProductId, (x, y) => x);

        return products;
    }

    public async Task<OneOf<Success, AlreadyExists>> AddToCart(Guid userId, Guid productId) {

        var cartItem = new AccountCartModel() { AccountId = userId, ProductId = productId };

        //check if this item is already in the cart
        if(await _context.Cart.ContainsAsync(cartItem)) {
            return new AlreadyExists();
        }

        await _context.Cart.AddAsync(cartItem);
        await _context.SaveChangesAsync();
        return new Success();

    }

    public async Task<OneOf<Success, NotFound>> RemoveFromCart(Guid userId, Guid productId) {

        var cartItem = await _context.Cart.FirstOrDefaultAsync(x => x.AccountId == userId && x.ProductId == productId);
        if(cartItem is null) {
            return new NotFound();
        }

        _context.Cart.Remove(cartItem);
        await _context.SaveChangesAsync();
        return new Success();

    }
}
