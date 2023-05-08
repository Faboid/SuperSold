using AutoMapper;
using AutoMapper.QueryableExtensions;
using SuperSold.Data.DBInteractions;
using SuperSold.UI.AspDotNet.Extensions;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.Handlers.Products.Queries;


public record GetOwnedProductsQuery(Guid UserId, int Page, int PageLength) : IRequest<List<Product>>;

public class GetOwnedProductsHandler : IRequestHandler<GetOwnedProductsQuery, List<Product>> {

    private readonly IProductsHandler _productHandler;
    private readonly IMapper _mapper;

    public GetOwnedProductsHandler(IProductsHandler productHandler, IMapper mapper) {
        _productHandler = productHandler;
        _mapper = mapper;
    }

    public async Task<List<Product>> Handle(GetOwnedProductsQuery request, CancellationToken cancellationToken) {
        return await _productHandler
            .QueryProductsBySellerId(request.UserId)
            .ProjectTo<Product>(_mapper.ConfigurationProvider)
            .SkipToPage(request.Page, request.PageLength)
            .ToListAsyncSafe();
    }
}
