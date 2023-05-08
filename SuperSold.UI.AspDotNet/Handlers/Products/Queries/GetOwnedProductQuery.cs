using AutoMapper;
using SuperSold.Data.DBInteractions;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.Handlers.Products.Queries;


public record GetOwnedProductQuery(Guid UserId, Guid ProductId) : IRequest<OneOf<Product, Unauthorized, NotFound>>;

public class GetOwnedProductHandler : IRequestHandler<GetOwnedProductQuery, OneOf<Product, Unauthorized, NotFound>> {

    private readonly IProductsHandler _productsHandler;
    private readonly IMapper _mapper;

    public GetOwnedProductHandler(IProductsHandler productsHandler, IMapper mapper) {
        _productsHandler = productsHandler;
        _mapper = mapper;
    }

    public async Task<OneOf<Product, Unauthorized, NotFound>> Handle(GetOwnedProductQuery request, CancellationToken cancellationToken) {
        
        var productOption = await _productsHandler.GetProduct(request.ProductId);
        if(productOption.TryPickT1(out var notfound, out var product)) {
            return notfound;
        }

        if(product.IdSellerAccount != request.UserId) {
            return new Unauthorized();
        }

        return _mapper.Map<Product>(product);

    }
}
