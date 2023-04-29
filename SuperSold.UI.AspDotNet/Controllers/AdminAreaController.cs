﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneOf;
using OneOf.Types;
using SuperSold.Data.DBInteractions;
using SuperSold.Data.Models;
using SuperSold.UI.AspDotNet.Attributes;
using SuperSold.UI.AspDotNet.Constants;
using SuperSold.UI.AspDotNet.Extensions;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.Controllers;

[Authorize]
[AdminOnly]
public class AdminAreaController : Controller {

    private readonly IAccountsHandler _accountsHandler;
    private readonly IProductsHandler _productsHandler;
    private readonly IAccountRolesHandler _accountRolesHandler;
    private readonly IAccountRestrictionsHandler _accountRestrictionsHandler;
    private readonly IMapper _mapper;

    public AdminAreaController(IAccountsHandler accountsHandler, IMapper mapper, IProductsHandler productsHandler, IAccountRestrictionsHandler accountRestrictionsHandler, IAccountRolesHandler accountRolesHandler) {
        _accountsHandler = accountsHandler;
        _mapper = mapper;
        _productsHandler = productsHandler;
        _accountRestrictionsHandler = accountRestrictionsHandler;
        _accountRolesHandler = accountRolesHandler;
    }

    [HttpGet]
    public IActionResult Index() {
        return View();
    }

    [HttpGet]
    public IActionResult AccountDetails(AccountInfoModel account, IEnumerable<string> roles, IEnumerable<string> restrictions,  IEnumerable<Product> products) {
        return View(nameof(AccountDetails), (account, roles, restrictions, products));
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

        var products = await _productsHandler
            .QueryAllProducts()
            .Where(x => (string.IsNullOrWhiteSpace(match) || x.Title.Contains(match)) && x.Price >= minPrice && (maxPrice == 0 || x.Price <= maxPrice))
            .ProjectTo<Product>(_mapper.ConfigurationProvider)
            .ToListAsyncSafe();

        return ProductsList(products);

    }

    [HttpGet]
    public async Task<IActionResult> SearchAccountsByUsername(string username) {

        var accounts = await _accountsHandler
            .QueryAccounts()
            .Where(x => string.IsNullOrWhiteSpace(username) || x.UserName.Contains(username))
            .ProjectTo<AccountInfoModel>(_mapper.ConfigurationProvider)
            .ToListAsyncSafe();

        return AccountsTable(accounts);

    }

    [HttpGet]
    public async Task<IActionResult> SearchAccountByUsername(string username) {

        if(string.IsNullOrEmpty(username)) {
            return BadRequest("Must specify an username.");
        }

        var queryResult = await _accountsHandler.GetAccountByUserName(username);
        return await MapUserWithSoldProducts(queryResult);
    }
    
    [HttpGet]
    public async Task<IActionResult> SearchAccountById(Guid accountId) {
        var queryResult = await _accountsHandler.GetAccountById(accountId);
        return await MapUserWithSoldProducts(queryResult);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProduct(Guid sellerId, Guid productId) {

        var productOption = await _productsHandler.GetProduct(productId);
        if(productOption.TryPickT1(out var _, out var product)) {
            return NotFound("Product is not found.");
        }

        if(product.IdSellerAccount != sellerId) {
            return BadRequest("The given seller Id is not the product's seller id.");
        }

        var result = await _productsHandler.DeleteProduct(productId);
        return result.Match<IActionResult>(
            success => Ok("Deleted product successfully."), 
            notfound => NotFound("The product is not found.")
        );

    }

    [HttpPost]
    public async Task<IActionResult> RestrictAccountFromSelling(Guid accountId) {

        var result = await _accountRestrictionsHandler.AddRestriction(accountId, Restrictions.ProductSeller);
        return result.Match<IActionResult>(
            success => Ok("The account has been restricted."),
            notfound => NotFound("The account has not been found.")
        );

    }

    private async Task<IActionResult> MapUserWithSoldProducts(OneOf<AccountModel, NotFound> queryResult) {

        if(queryResult.TryPickT1(out var _, out var acc)) {
            return NotFound("The query has not found any result.");
        }

        var products = await _productsHandler
            .QueryProductsBySellerId(acc.IdAccount)
            .ProjectTo<Product>(_mapper.ConfigurationProvider)
            .ToListAsyncSafe();

        var roles = await _accountRolesHandler.GetAccountRoles(acc.IdAccount).Select(x => x.Role).ToListAsyncSafe();
        var restrictions = await _accountRestrictionsHandler.GetAccountRestrictions(acc.IdAccount).Select(x => x.Restriction).ToListAsyncSafe();

        return queryResult.Match<IActionResult>(
            account => AccountDetails(_mapper.Map<AccountInfoModel>(account), roles, restrictions, products),
            notfound => NotFound()
        );

    }

}