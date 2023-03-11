using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperSold.Data.DBInteractions;
using SuperSold.UI.AspDotNet.Extensions;
using SuperSold.UI.AspDotNet.Models;
using SuperSold.UI.AspDotNet.ViewRouting;
using System.Diagnostics;

namespace SuperSold.UI.AspDotNet.Controllers;
public class HomeController : Controller {

    private const int pageLength = 2;
    private readonly ILogger<HomeController> _logger;
    private readonly IProductsHandler _productsHandler;

    public HomeController(ILogger<HomeController> logger, IProductsHandler productsHandler) {
        _logger = logger;
        _productsHandler = productsHandler;
    }

    [HttpGet]
    public IActionResult Index() => View();

    [HttpGet]
    public IActionResult Search(string search) => View(model: search);

    [HttpGet]
    public async Task<IActionResult> IndexPartial(int page) {

        var products = await _productsHandler
            .QueryAllProducts()
            .SkipToPage(page, pageLength)
            .Select(x => (Product)x)
            .ToListAsyncSafe();

        return this.ProductListPartialView(PartialViewNames.BuyableProductRowPartial, products);
    }

    [HttpGet]
    public async Task<IActionResult> SearchPartial(int page, string search) {
        
        if(string.IsNullOrWhiteSpace(search)) {
            return RedirectToPage("Index");
        }

        var searchExpression = $"%{search ?? ""}%";
        var products = await _productsHandler
            .QueryAllProducts()
            .Where(x => EF.Functions.Like(x.Title, searchExpression))
            .SkipToPage(page, pageLength)
            .Select(x => (Product)x)
            .ToListAsyncSafe();

        ViewBag.SearchItem = search;
        return this.ProductListPartialView(PartialViewNames.BuyableProductRowPartial, products);
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
