using Microsoft.AspNetCore.Mvc;
using SuperSold.Data.DBInteractions;
using SuperSold.UI.AspDotNet.Extensions;
using SuperSold.UI.AspDotNet.Models;
using System.Diagnostics;

namespace SuperSold.UI.AspDotNet.Controllers;
public class HomeController : Controller {

    private const int pageLength = 5;
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
