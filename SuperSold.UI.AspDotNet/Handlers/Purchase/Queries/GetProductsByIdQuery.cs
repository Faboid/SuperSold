using AutoMapper;
using SuperSold.Data.DBInteractions;
using SuperSold.UI.AspDotNet.Extensions;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.Handlers.Purchase.Queries;

public record GetProductsByIdQuery(params Guid[] ProductIds) : IRequest<OneOf<Product[], NotFound>>;

public class GetProductsByIdHandler : IRequestHandler<GetProductsByIdQuery, OneOf<Product[], NotFound>> {

    private readonly IProductsHandler _productsHandler;
    private readonly IMapper _mapper;

    public GetProductsByIdHandler(IProductsHandler productsHandler, IMapper mapper) {
        _productsHandler = productsHandler;
        _mapper = mapper;
    }

    public async Task<OneOf<Product[], NotFound>> Handle(GetProductsByIdQuery request, CancellationToken cancellationToken) {

        var products = await request.ProductIds
            .Select(async x => await _productsHandler.GetProduct(x))
            .ToListAsync();

        if(products.Any(x => x.IsT1)) {
            return new NotFound();
        }

        return products
            .Select(x => x.AsT0)
            .Select(_mapper.Map<Product>)
            .ToArray();

    }
}
