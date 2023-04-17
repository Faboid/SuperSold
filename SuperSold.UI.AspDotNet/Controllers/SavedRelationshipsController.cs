using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SuperSold.Data.DBInteractions;
using SuperSold.UI.AspDotNet.Models;
using SuperSold.UI.AspDotNet.ViewRouting;

namespace SuperSold.UI.AspDotNet.Controllers;
public class SavedRelationshipsController : Controller {

    private readonly ISavedRelationshipsHandler _savedRelationshipsHandler;
    private readonly IProductsHandler _productsHandler;
    private readonly IMapper _mapper;

    public SavedRelationshipsController(ISavedRelationshipsHandler savedRelationshipsHandler, IMapper mapper, IProductsHandler productsHandler) {
        _savedRelationshipsHandler = savedRelationshipsHandler;
        _mapper = mapper;
        _productsHandler = productsHandler;
    }

    [HttpPost]
    public async Task<IActionResult> ModifyQuantity(Guid relationshipId, int quantity) {

        var result = await _savedRelationshipsHandler.UpdateRelationshipQuantity(relationshipId, quantity);
        return result.Match<IActionResult>(
            success => Ok(),
            notfound => NotFound()
        );

    }

    [HttpGet]
    public async Task<IActionResult> Get(Guid relationshipId, string itemRowFormat) {

        var relationshipResult = await _savedRelationshipsHandler.GetRelationship(relationshipId);
        if(relationshipResult.TryPickT1(out var _, out var relationship)) {
            return NotFound();
        }

        var productResult = await _productsHandler.GetProduct(relationship.IdProduct);
        if(productResult.TryPickT1(out var _, out var product)) {
            return NotFound();
        }

        var savedRelationship = new ProductWithSavedRelationship() { 
            Product = _mapper.Map<Product>(product),
            SavedRelationship = _mapper.Map<SavedRelationship>(relationship)
        };

        return this.RelationshipListPartialView(itemRowFormat, savedRelationship);

    }

}
