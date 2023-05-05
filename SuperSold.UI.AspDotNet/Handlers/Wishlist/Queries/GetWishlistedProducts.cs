using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using SuperSold.Data.DBInteractions;
using SuperSold.UI.AspDotNet.Extensions;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.Handlers.Wishlist.Queries;

public record GetWishlistedProducts(Guid AccountId, int Page, int PageLength) : IRequest<List<ProductWithSavedRelationship>>;

public class GetWishlistedProductsHandler : IRequestHandler<GetWishlistedProducts, List<ProductWithSavedRelationship>> {

    private readonly IWishlistHandler _wishlistHandler;
    private readonly IMapper _mapper;

    public GetWishlistedProductsHandler(IWishlistHandler wishlistHandler, IMapper mapper) {
        _wishlistHandler = wishlistHandler;
        _mapper = mapper;
    }

    public async Task<List<ProductWithSavedRelationship>> Handle(GetWishlistedProducts request, CancellationToken cancellationToken) {

        return await _wishlistHandler.QueryWishlistedProductsByUserId(request.AccountId)
           .SkipToPage(request.Page, request.PageLength)
           .ProjectTo<ProductWithSavedRelationship>(_mapper.ConfigurationProvider)
           .ToListAsyncSafe();
    }
}
