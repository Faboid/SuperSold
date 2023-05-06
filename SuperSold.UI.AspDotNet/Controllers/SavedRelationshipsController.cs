using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SuperSold.UI.AspDotNet.Handlers.SavedRelationships.Commands;
using SuperSold.UI.AspDotNet.Handlers.SavedRelationships.Queries;
using SuperSold.UI.AspDotNet.ViewRouting;

namespace SuperSold.UI.AspDotNet.Controllers;
public class SavedRelationshipsController : Controller {


    private readonly IMediator _mediator;

    public SavedRelationshipsController(IMediator mediator) {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> ModifyQuantity(Guid relationshipId, int quantity) {

        var command = new ModifyRelationshipQuantityCommand(relationshipId, quantity);
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(
            success => Ok(),
            notfound => NotFound()
        );

    }

    [HttpGet]
    public async Task<IActionResult> Get(Guid relationshipId, string itemRowFormat) {

        var query = new GetProductWithRelationshipQuery(relationshipId);
        var result = await _mediator.Send(query);

        return result.Match(
            product => this.RelationshipListPartialView(itemRowFormat, product),
            notfound => NotFound()
        );

    }

}
