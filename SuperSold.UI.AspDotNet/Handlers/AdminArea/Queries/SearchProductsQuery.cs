using AutoMapper;
using AutoMapper.QueryableExtensions;
using SuperSold.Data.DBInteractions;
using SuperSold.UI.AspDotNet.Extensions;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.Handlers.AdminArea.Queries;


public record SearchProductsQuery(string Match, int MinPrice, int MaxPrice) : IRequest<List<Product>>;

public class SearchProductsHandler : IRequestHandler<SearchProductsQuery, List<Product>> {

    private readonly IProductsHandler _productsHandler;
    private readonly IMapper _mapper;

    public SearchProductsHandler(IProductsHandler productsHandler, IMapper mapper) {
        _productsHandler = productsHandler;
        _mapper = mapper;
    }

    public async Task<List<Product>> Handle(SearchProductsQuery request, CancellationToken cancellationToken) {
        
        return await _productsHandler
            .QueryAllProducts()
            .Where(x => (string.IsNullOrWhiteSpace(request.Match) || x.Title.Contains(request.Match)) && x.Price >= request.MinPrice && (request.MaxPrice == 0 || x.Price <= request.MaxPrice))
            .ProjectTo<Product>(_mapper.ConfigurationProvider)
            .ToListAsyncSafe();

    }
}
