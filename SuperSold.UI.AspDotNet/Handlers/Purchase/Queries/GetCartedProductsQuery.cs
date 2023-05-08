using AutoMapper;
using AutoMapper.QueryableExtensions;
using SuperSold.Data.DBInteractions;
using SuperSold.UI.AspDotNet.Extensions;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.Handlers.Purchase.Queries;

public record GetCartedProductsQuery(Guid UserId) : IRequest<List<ProductWithSavedRelationship>>;

public class GetCartedProductsHandler : IRequestHandler<GetCartedProductsQuery, List<ProductWithSavedRelationship>> {

    private readonly ICartHandler _cartHandler;
    private readonly IMapper _mapper;

    public GetCartedProductsHandler(ICartHandler cartHandler, IMapper mapper) {
        _cartHandler = cartHandler;
        _mapper = mapper;
    }

    public async Task<List<ProductWithSavedRelationship>> Handle(GetCartedProductsQuery request, CancellationToken cancellationToken) {

        return await _cartHandler
            .QueryCartedProductsByUserId(request.UserId)
            .ProjectTo<ProductWithSavedRelationship>(_mapper.ConfigurationProvider)
            .ToListAsyncSafe();
        
    }
}
