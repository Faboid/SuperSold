using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperSold.UI.AspDotNet.Attributes;
using SuperSold.UI.AspDotNet.Constants;
using SuperSold.UI.AspDotNet.Handlers.AdminArea.Commands;
using SuperSold.UI.AspDotNet.Handlers.AdminArea.Queries;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.Controllers;

[Authorize]
[AdminOnly]
public class AdminAreaController : Controller {

    private readonly IMediator _mediator;

    public AdminAreaController(IMediator mediator) {
        _mediator = mediator;
    }

    [HttpGet]
    public IActionResult Index() {
        return View();
    }

    [HttpGet]
    public IActionResult AccountDetails(AccountDetailsModel details) {
        return View(nameof(AccountDetails), details);
    }

    [HttpGet]
    public IActionResult AccountsTable(IEnumerable<AccountInfoModel> accounts) {
        return View(nameof(AccountsTable), accounts);
    }

    [HttpGet]
    public IActionResult ProductsList(IEnumerable<Product> products) {
        return View(nameof(ProductsList), products);
    }

    [HttpGet]
    public async Task<IActionResult> SearchProducts(string match, int minPrice, int maxPrice) {
        var query = new SearchProductsQuery(match, minPrice, maxPrice);
        var result = await _mediator.Send(query);
        return ProductsList(result);
    }

    [HttpGet]
    public async Task<IActionResult> SearchAccountsByUsername(string username) {
        var query = new SearchAccountsByUsernameQuery(username);
        var result = await _mediator.Send(query);
        return AccountsTable(result);
    }

    [HttpGet]
    public async Task<IActionResult> SearchAccountByUsername(string username) {

        if(string.IsNullOrEmpty(username)) {
            return BadRequest("Must specify an username.");
        }

        var query = new GetAccountDetailsQuery(username);
        var result = await _mediator.Send(query);
        return result.Match(
            accountdetails => AccountDetails(accountdetails),
            notfound => NotFound("The query has not found any result.")
        );

    }
    
    [HttpGet]
    public async Task<IActionResult> SearchAccountById(Guid accountId) {

        var query = new GetAccountDetailsQuery(accountId);
        var result = await _mediator.Send(query);
        return result.Match(
            accountdetails => AccountDetails(accountdetails),
            notfound => NotFound("The query has not found any result.")
        );

    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProduct(Guid sellerId, Guid productId) {

        var command = new ForceDeleteProductCommand(sellerId, productId);
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(
            success => Ok("Deleted product successfully"),
            mismatchedIds => BadRequest("The given seller Id is not the product's seller id."),
            notfound => NotFound("The product has not been found.")
        );

    }

    [HttpPost]
    public async Task<IActionResult> RestrictAccountFromSelling(Guid accountId) {

        var command = new AddAccountRestrictionCommand(accountId, Restrictions.ProductSeller);
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(
            success => Ok("The account has been restricted."),
            notfound => NotFound("The account has not been found.")
        );

    }

}