using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using SuperSold.Data.DBInteractions;
using SuperSold.Data.Models;
using SuperSold.Data.Models.ResponseTypes;
using System.Security.Cryptography.X509Certificates;

namespace SuperSold.Data.EfCoreDB;

public class EfCoreProductsHandler : IProductsHandler {

    private readonly EfCoreDBContext _context;

    public EfCoreProductsHandler(EfCoreDBContext context) {
        _context = context;
    }

    public async Task<OneOf<Success, AlreadyExists>> CreateProduct(ProductModel product, string sellerUserName) {
        
        await _context.Products.AddAsync(product);
        var result = await _context.SaveChangesAsync();
        
        if(result == 1) {
            return new Success();
        } else {
            //todo - log
            return new AlreadyExists();
        }

    }

    public async Task<OneOf<Success, NotFound>> DeleteProduct(Guid productId) {

        var product = await _context.Products.FirstOrDefaultAsync(x => x.IdProduct == productId);
        if(product is null) {
            return new NotFound();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return new Success();
        
    }

    public async Task<OneOf<Success, NotFound>> EditProduct(Guid productId, ProductModel updatedValues) {

        var productOption = await GetProduct(productId);
        if(productOption.TryPickT1(out var notFound, out var product)) {
            return notFound;
        }

        product.Title = updatedValues.Title;
        product.Description = updatedValues.Description;
        product.ImageUrl = updatedValues.ImageUrl;
        product.Price = updatedValues.Price;
        await _context.SaveChangesAsync();
        return new Success();

    }

    public async Task<OneOf<ProductModel, NotFound>> GetProduct(Guid productId) {

        var product = await _context.Products.FirstOrDefaultAsync(x => x.IdProduct == productId);
        if(product is null) {
            return new NotFound();
        }

        return product;

    }

    public IQueryable<ProductModel> QueryAllProducts() => _context.Products;
    public IQueryable<ProductModel> QueryProductsBySellerId(Guid sellerId) => _context.Products.Where(x => x.IdSellerAccount == sellerId);
    
}

