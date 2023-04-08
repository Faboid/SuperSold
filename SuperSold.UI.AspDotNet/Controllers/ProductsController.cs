using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperSold.Data.DBInteractions;
using SuperSold.Data.Models;
using SuperSold.UI.AspDotNet.Extensions;
using SuperSold.UI.AspDotNet.Models;
using SuperSold.UI.AspDotNet.ViewRouting;

namespace SuperSold.UI.AspDotNet.Controllers;
public class ProductsController : Controller {

    private readonly IProductsHandler _productsHandler;
    private readonly IMapper _mapper;

    public ProductsController(IProductsHandler productsHandler, IMapper mapper) {
        _productsHandler = productsHandler;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize]
    public IActionResult MyProducts() => View();

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> MyProductsPartial(int page) {

        var userId = User.GetIdentity();

        var listProducts = await _productsHandler
            .QueryProductsBySellerId(userId)
            .ProjectTo<Product>(_mapper.ConfigurationProvider)
            .SkipToPage(page, 10)
            .ToListAsyncSafe();

        return this.ProductListPartialView(PartialViewNames.OnSaleRow, listProducts);

    }

    [HttpGet]
    [Authorize]
    public IActionResult Publish() => View();

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Publish(Product product) {

        if(!ModelState.IsValid) {
            return View();
        }

        var user = User.Identity!.Name!;
        var sellerId = User.GetIdentity();

        product.Id = Guid.NewGuid();
        product.SellerId = sellerId;

        var result = await _productsHandler.CreateProduct(_mapper.Map<ProductModel>(product), user);

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
            return new UnauthorizedResult();
        }

        var result = await _productsHandler.DeleteProduct(model.Id);
        return result.Match<IActionResult>(
            success => Ok(),
            notFound => NotFound()
        );

    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Edit(Guid id) {

        var product = await _productsHandler.GetProduct(id);

        if(product.TryPickT1(out var notFound, out var model)) {
            return NotFound();
        }

        var userId = User.GetIdentity();
        if(model.IdSellerAccount != userId) {
            return new UnauthorizedResult();
        }

        return View(_mapper.Map<Product>(model));

    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Edit(Product model) {

        if(!ModelState.IsValid) {
            return View(model);
        }

        var userId = User.GetIdentity();
        if(model.SellerId != userId) {
            return new UnauthorizedResult();
        }

        var result = await _productsHandler.EditProduct(model.Id, _mapper.Map<ProductModel>(model));
        return result.Match<IActionResult>(
            success => RedirectToAction("MyProducts"),
            notFound => NotFound()
        );

    }

    [HttpGet]
    public async Task<IActionResult> Get(Guid id, string itemRowFormat) {

        var result = await _productsHandler.GetProduct(id);

        if(result.TryPickT1(out var notFound, out var product)) {
            return NotFound();
        }

        return this.ProductListPartialView(itemRowFormat, _mapper.Map<Product>(product));

    }

}
