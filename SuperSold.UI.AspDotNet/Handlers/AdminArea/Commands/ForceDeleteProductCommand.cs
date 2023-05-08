using SuperSold.Data.DBInteractions;

namespace SuperSold.UI.AspDotNet.Handlers.AdminArea.Commands;


public record ForceDeleteProductCommand(Guid SellerId, Guid ProductId) : IRequest<OneOf<Success, MismatchedIds, NotFound>>;

public class ForceDeleteProductHandler : IRequestHandler<ForceDeleteProductCommand, OneOf<Success, MismatchedIds, NotFound>> {

    private readonly IProductsHandler _productsHandler;

    public ForceDeleteProductHandler(IProductsHandler productsHandler) {
        _productsHandler = productsHandler;
    }

    public async Task<OneOf<Success, MismatchedIds, NotFound>> Handle(ForceDeleteProductCommand request, CancellationToken cancellationToken) {

        var productOption = await _productsHandler.GetProduct(request.ProductId);
        if(productOption.TryPickT1(out var notfound, out var product)) {
            return notfound;
        }

        if(product.IdSellerAccount != request.SellerId) {
            return new MismatchedIds();
        }

        var result = await _productsHandler.DeleteProduct(request.ProductId);
        return result.Match<OneOf<Success, MismatchedIds, NotFound>>(x => x, x => x);

    }
}
