using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperSold.Data.DBInteractions;
using SuperSold.UI.AspDotNet.Extensions;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.Handlers.Home.Queries;

public record SearchProductsInPage(int Page, int PageLength, string Search) : IRequest<List<Product>>;

public class SearchProductsInPageHandler : IRequestHandler<SearchProductsInPage, List<Product>> {

    private readonly IProductsHandler _productsHandler;
    private readonly IMapper _mapper;

    public SearchProductsInPageHandler(IProductsHandler productsHandler, IMapper mapper) {
        _productsHandler = productsHandler;
        _mapper = mapper;
    }

    public async Task<List<Product>> Handle(SearchProductsInPage request, CancellationToken cancellationToken) {

        var searchExpression = $"%{request.Search ?? ""}%";
        return await _productsHandler
            .QueryAllProducts()
            .Where(x => EF.Functions.Like(x.Title, searchExpression))
            .SkipToPage(request.Page, request.PageLength)
            .ProjectTo<Product>(_mapper.ConfigurationProvider)
            .ToListAsyncSafe();
    }
}
