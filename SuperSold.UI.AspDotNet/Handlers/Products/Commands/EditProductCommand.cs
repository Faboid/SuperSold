using AutoMapper;
using SuperSold.Data.DBInteractions;
using SuperSold.Data.Models;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.Handlers.Products.Commands;

public record EditProductCommand(Guid ProductId, Product NewProductValues) : IRequest<OneOf<Success, NotFound>>;

public class EditProductHandler : IRequestHandler<EditProductCommand, OneOf<Success, NotFound>> {

    private readonly IProductsHandler _productsHandler;
    private readonly IMapper _mapper;

    public EditProductHandler(IProductsHandler productsHandler, IMapper mapper) {
        _productsHandler = productsHandler;
        _mapper = mapper;
    }

    public async Task<OneOf<Success, NotFound>> Handle(EditProductCommand request, CancellationToken cancellationToken) {
        return await _productsHandler.EditProduct(request.ProductId, _mapper.Map<ProductModel>(request.NewProductValues));
    }
}
