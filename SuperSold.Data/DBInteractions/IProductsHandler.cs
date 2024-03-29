﻿using OneOf;
using OneOf.Types;
using SuperSold.Data.Models;
using SuperSold.Data.Models.ResponseTypes;

namespace SuperSold.Data.DBInteractions;
public interface IProductsHandler {

    IQueryable<ProductModel> QueryAllProducts();
    IQueryable<ProductModel> QueryProductsBySellerId(Guid sellerId);
    Task<OneOf<Success, NotFound>> EditProduct(Guid productId, ProductModel updatedValues);
    Task<OneOf<Success, NotFound>> DeleteProduct(Guid productId);
    Task<OneOf<Success, AlreadyExists>> CreateProduct(ProductModel product, string sellerUserName);
    Task<OneOf<ProductModel, NotFound>> GetProduct(Guid productId);

}
