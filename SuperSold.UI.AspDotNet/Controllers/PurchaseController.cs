using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperSold.UI.AspDotNet.Extensions;
using SuperSold.UI.AspDotNet.Handlers.Purchase.Queries;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.Controllers;

[Authorize]
[AutoValidateAntiforgeryToken]
public class PurchaseController : Controller {

    private readonly IMediator _mediator;

    public PurchaseController(IMediator mediator) {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index() {
        var userId = User.GetIdentity();
        var query = new GetCartedProductsQuery(userId);
        var result = await _mediator.Send(query);
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> SubmitPurchase(PurchaseInfoModel info, decimal price, string products) {

        if(!info.WarningCheckbox) {
            return BadRequest("Must tick the disclaimer checkbox.");
        }

        var objProducts = products
            .Split(',')
            .Select(ProductWithSavedRelationship.ParseBasicString)
            .ToList();

        var query = new GetProductsByIdQuery(objProducts.Select(x => x.ProductId).ToArray());
        var result = await _mediator.Send(query);

        if(result.TryPickT1(out var notfound, out var dbProducts)) {
            return StatusCode(412, "One or more of the products in the cart is not on sale anymore.");
        }

        var updatedPrice = dbProducts.Select(x => objProducts.Single(y => x.Id == y.ProductId).Quantity * x.Price).Sum();

        if(price != updatedPrice) {
            return StatusCode(412, "The price has changed for one or more items. Please refresh the page.");
        }

        return Accepted(value: "The purchase request has been received, but it is only simulated, so the card hasn't been billed.");

    }

}
