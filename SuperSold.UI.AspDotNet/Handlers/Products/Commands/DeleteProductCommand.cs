using SuperSold.Data.DBInteractions;

namespace SuperSold.UI.AspDotNet.Handlers.Products.Commands;


public record DeleteProductCommand(Guid ProductId) : IRequest<OneOf<Success, NotFound>>;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, OneOf<Success, NotFound>> {

    private readonly IProductsHandler _productsHandler;

    public DeleteProductHandler(IProductsHandler productsHandler) {
        _productsHandler = productsHandler;
    }

    public async Task<OneOf<Success, NotFound>> Handle(DeleteProductCommand request, CancellationToken cancellationToken) {
        return await _productsHandler.DeleteProduct(request.ProductId);
    }
}