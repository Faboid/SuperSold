using AutoMapper;
using SuperSold.Data.DBInteractions;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.Handlers.SavedRelationships.Queries;

public record GetProductWithRelationshipQuery(Guid RelationshipId) : IRequest<OneOf<ProductWithSavedRelationship, NotFound>>;

public class GetProductWithRelationshipHandler : IRequestHandler<GetProductWithRelationshipQuery, OneOf<ProductWithSavedRelationship, NotFound>> {

    private readonly ISavedRelationshipsHandler _savedRelationshipsHandler;
    private readonly IProductsHandler _productsHandler;
    private readonly IMapper _mapper;

    public GetProductWithRelationshipHandler(ISavedRelationshipsHandler savedRelationshipsHandler, IProductsHandler productsHandler, IMapper mapper) {
        _savedRelationshipsHandler = savedRelationshipsHandler;
        _productsHandler = productsHandler;
        _mapper = mapper;
    }

    public async Task<OneOf<ProductWithSavedRelationship, NotFound>> Handle(GetProductWithRelationshipQuery request, CancellationToken cancellationToken) {
        
        var relationshipResult = await _savedRelationshipsHandler.GetRelationship(request.RelationshipId);
        if(relationshipResult.TryPickT1(out var notfound, out var relationship)) {
            return notfound;
        }

        var productResult = await _productsHandler.GetProduct(relationship.IdProduct);
        if(productResult.TryPickT1(out notfound, out var product)) {
            return notfound;
        }

        return new ProductWithSavedRelationship() {
            Product = _mapper.Map<Product>(product),
            SavedRelationship = _mapper.Map<SavedRelationship>(relationship)
        };

    }

}
