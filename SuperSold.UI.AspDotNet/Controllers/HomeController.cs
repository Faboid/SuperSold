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
    public async Task<IActionResult> Index(int? page) {

        var products = await _productsHandler
            .QueryAllProducts()
            .SkipToPage(page, pageLength)
            .Select(x => (Product)x)
            .ToListAsyncSafe();

        ViewBag.PageLength = pageLength;
        ViewBag.Page = page ?? 0;

        if(Request.IsAjaxRequest()) {
            return this.ProductListPartialView("_BuyableProductRowPartial", products);
        }

        return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> Search(int? page, string? search) {
        
        if(string.IsNullOrWhiteSpace(search)) {
            return RedirectToAction("Index");
        }
        
        var searchExpression = $"%{search ?? ""}%";
        var products = await _productsHandler
            .QueryAllProducts()
            .Where(x => EF.Functions.Like(x.Title, searchExpression))
            .SkipToPage(page, pageLength)
            .Select(x => (Product)x)
            .ToListAsyncSafe();

        ViewBag.PageLength = pageLength;
        ViewBag.Page = page ?? 0;
        ViewBag.SearchItem = search;
        return View(products);
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
