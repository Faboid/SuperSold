using AutoMapper;
using SuperSold.Data.DBInteractions;
using SuperSold.Data.Models;
using SuperSold.Data.Models.ResponseTypes;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.Handlers.Products.Commands;


public record PublishProductCommand(string SellerName, Product Product) : IRequest<OneOf<Success, AlreadyExists>>;

public class PublishProductHandler : IRequestHandler<PublishProductCommand, OneOf<Success, AlreadyExists>> {

    private readonly IProductsHandler _productsHandler;
    private readonly IMapper _mapper;

    public PublishProductHandler(IProductsHandler productsHandler, IMapper mapper) {
        _productsHandler = productsHandler;
        _mapper = mapper;
    }

    public async Task<OneOf<Success, AlreadyExists>> Handle(PublishProductCommand request, CancellationToken cancellationToken) {
        return await _productsHandler.CreateProduct(_mapper.Map<ProductModel>(request.Product), request.SellerName);
    }
}
