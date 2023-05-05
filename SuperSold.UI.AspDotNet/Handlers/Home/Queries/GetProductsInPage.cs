using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using SuperSold.Data.DBInteractions;
using SuperSold.UI.AspDotNet.Extensions;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.Handlers.Home.Queries;

public record GetProductsInPage(int Page, int PageLength) : IRequest<List<Product>>;

public class GetProductsInPageHandler : IRequestHandler<GetProductsInPage, List<Product>> {

    private readonly IProductsHandler _productsHandler;
    private readonly IMapper _mapper;

    public GetProductsInPageHandler(IProductsHandler productsHandler, IMapper mapper) {
        _productsHandler = productsHandler;
        _mapper = mapper;
    }

    public async Task<List<Product>> Handle(GetProductsInPage request, CancellationToken cancellationToken) {
        return await _productsHandler
            .QueryAllProducts()
            .SkipToPage(request.Page, request.PageLength)
            .ProjectTo<Product>(_mapper.ConfigurationProvider)
            .ToListAsyncSafe();
    }
}
