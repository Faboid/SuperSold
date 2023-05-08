using AutoMapper;
using SuperSold.Data.DBInteractions;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.Handlers.Products.Queries;


public record GetProductQuery(Guid ProductId) : IRequest<OneOf<Product, NotFound>>;

public class GetProductHandler : IRequestHandler<GetProductQuery, OneOf<Product, NotFound>> {

    private readonly IProductsHandler _productsHandler;
    private readonly IMapper _mapper;

    public GetProductHandler(IProductsHandler productsHandler, IMapper mapper) {
        _productsHandler = productsHandler;
        _mapper = mapper;
    }

    public async Task<OneOf<Product, NotFound>> Handle(GetProductQuery request, CancellationToken cancellationToken) {

        var result = await _productsHandler.GetProduct(request.ProductId);
        return result.MapT0(_mapper.Map<Product>);

    }
}
