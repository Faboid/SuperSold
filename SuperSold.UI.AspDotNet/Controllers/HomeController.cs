using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    private readonly IMapper _mapper;

    public HomeController(ILogger<HomeController> logger, IProductsHandler productsHandler, IMapper mapper) {
        _logger = logger;
        _productsHandler = productsHandler;
        _mapper = mapper;
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
            .ProjectTo<Product>(_mapper.ConfigurationProvider)
            .ToListAsyncSafe();

        _logger.LogInformation("Loaded {number} items with IndexPartial", products.Count);
        return this.ProductListPartialView(PartialViewNames.HomeSearchRow, products);
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
            .ProjectTo<Product>(_mapper.ConfigurationProvider)
            .ToListAsyncSafe();

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
