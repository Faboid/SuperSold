using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperSold.UI.AspDotNet.Attributes;
using SuperSold.UI.AspDotNet.Extensions;
using SuperSold.UI.AspDotNet.Handlers.Products.Commands;
using SuperSold.UI.AspDotNet.Handlers.Products.Queries;
using SuperSold.UI.AspDotNet.Models;
using SuperSold.UI.AspDotNet.ViewRouting;

namespace SuperSold.UI.AspDotNet.Controllers;
public class ProductsController : Controller {

    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator) {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    public IActionResult MyProducts() => View();

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> MyProductsPartial(int page) {

        var userId = User.GetIdentity();
        var query = new GetOwnedProductsQuery(userId, page, 10);
        var products = await _mediator.Send(query);
        return this.ProductListPartialView(PartialViewNames.OnSaleRow, products);

    }

    [HttpGet]
    [Authorize]
    [CheckRestrictProductManagement]
    public IActionResult Publish() => View();

    [HttpPost]
    [Authorize]
    [CheckRestrictProductManagement]
    public async Task<IActionResult> Publish(Product product) {

        if(!ModelState.IsValid) {
            return View();
        }

        var user = User.Identity!.Name!;
        var sellerId = User.GetIdentity();

        product.Id = Guid.NewGuid();
        product.SellerId = sellerId;

        var command = new PublishProductCommand(user, product);
        var result = await _mediator.Send(command);

        return result.Match<IActionResult>(
            success => Redirect("MyProducts"),
            alreadyExists => {
                ViewBag.Message = "There has been a duplication error. It's suggested to reload the page and re-enter the values.";
                //todo - this should never happen. Add logging and consider returning a 500 error
                return View();
            }
        );

    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Delete(Product model) {

        if(!ModelState.IsValid) {
            return BadRequest("The given model is not valid.");
        }

        var userId = User.GetIdentity();

        if(userId != model.SellerId) {
            return Unauthorized();
        }

        var command = new DeleteProductCommand(model.Id);
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(
            success => Ok(),
            notFound => NotFound()
        );

    }

    [HttpGet]
    [Authorize]
    [CheckRestrictProductManagement]
    public async Task<IActionResult> Edit(Guid id) {

        var userId = User.GetIdentity();
        var query = new GetOwnedProductQuery(userId, id);
        var result = await _mediator.Send(query);
        return result.Match<IActionResult>(
            product => View(product),
            unauthorized => Unauthorized(),
            notfound => NotFound()
        );

    }

    [HttpPost]
    [Authorize]
    [CheckRestrictProductManagement]
    public async Task<IActionResult> Edit(Product model) {

        if(!ModelState.IsValid) {
            return View(model);
        }

        var userId = User.GetIdentity();
        if(model.SellerId != userId) {
            return new UnauthorizedResult();
        }

        var command = new EditProductCommand(model.Id, model);
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(
            success => RedirectToAction("MyProducts"),
            notFound => NotFound()
        );

    }

    [HttpGet]
    public async Task<IActionResult> Get(Guid id, string itemRowFormat) {

        var query = new GetProductQuery(id);
        var result = await _mediator.Send(query);
        return result.Match<IActionResult>(
            product => this.ProductListPartialView(itemRowFormat, product),
            notFound => NotFound()
        );

    }

}
