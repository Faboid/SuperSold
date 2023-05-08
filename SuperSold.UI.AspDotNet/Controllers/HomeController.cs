using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperSold.UI.AspDotNet.Handlers.Home.Queries;
using SuperSold.UI.AspDotNet.Models;
using SuperSold.UI.AspDotNet.ViewRouting;
using System.Diagnostics;

namespace SuperSold.UI.AspDotNet.Controllers;
public class HomeController : Controller {

    private const int pageLength = 2;
    private readonly ILogger<HomeController> _logger;
    private readonly IMediator _mediator;

    public HomeController(ILogger<HomeController> logger, IMediator mediator) {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    public IActionResult Index() => View();

    [HttpGet]
    public IActionResult Search(string search) => View(model: search);

    [HttpGet]
    public async Task<IActionResult> IndexPartial(int page) {

        var query = new GetProductsInPage(page, pageLength);
        var products = await _mediator.Send(query);

        _logger.LogInformation("Loaded {number} items with IndexPartial", products.Count);
        return this.ProductListPartialView(PartialViewNames.HomeSearchRow, products);
    }

    [HttpGet]
    public async Task<IActionResult> SearchPartial(int page, string search) {
        
        if(string.IsNullOrWhiteSpace(search)) {
            return RedirectToPage(nameof(Index));
        }

        var query = new SearchProductsInPage(page, pageLength, search);
        var products = await _mediator.Send(query);

        ViewBag.SearchItem = search;
        _logger.LogInformation("Loaded {number} items with SearchPartial", products.Count);
        return this.ProductListPartialView(PartialViewNames.HomeSearchRow, products);
    }

    public IActionResult Privacy() {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult TermsAndConditions() => View();

}
