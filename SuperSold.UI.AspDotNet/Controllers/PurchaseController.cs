using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperSold.Data.DBInteractions;
using SuperSold.UI.AspDotNet.Extensions;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.Controllers;

[Authorize]
[AutoValidateAntiforgeryToken]
public class PurchaseController : Controller {

    private readonly ICartHandler _cartHandler;
    private readonly IMapper _mapper;

    public PurchaseController(ICartHandler cartHandler, IMapper mapper) {
        _cartHandler = cartHandler;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index() {

        var userId = User.GetIdentity();
        var list = await _cartHandler
            .QueryCartedProductsByUserId(userId)
            .ProjectTo<ProductWithSavedRelationship>(_mapper.ConfigurationProvider)
            .ToListAsyncSafe();

        return View(list);
    }

    [HttpPost]
    public Task<IActionResult> SubmitPurchase(PurchaseInfoModel info, string products) {

        var objProducts = products.Split(',').Select(ProductWithSavedRelationship.ParseBasicString).ToList();

        throw new NotImplementedException();
    }

}
