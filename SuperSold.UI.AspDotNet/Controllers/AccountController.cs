using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SuperSold.Data.DBInteractions;
using SuperSold.UI.AspDotNet.Constants;
using SuperSold.UI.AspDotNet.Models;
using System.Security.Claims;

namespace SuperSold.UI.AspDotNet.Controllers;
public class AccountController : Controller {

    private readonly IAccountsHandler _accountsHandler;

    public AccountController(IAccountsHandler accountsHandler) {
        _accountsHandler = accountsHandler;
    }

    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel login) {

        if(!ModelState.IsValid) {
            return View();
        }

        //todo - implement encryption
        login.Password = login.Password; //set up encryption

        var accountOption = await _accountsHandler.GetAccountByUserName(login.UserName);
        if(!accountOption.TryGetValue(out var account)) {
            ViewBag.Message = "The given username does not exist.";
            return View();
        }

        if(login.Password != account!.HashedPassword) {
            ViewBag.Message = "The given password is incorrect.";
            return View();
        }

        var claims = new List<Claim> {
            new(ClaimTypes.Name, login.UserName)
        };

        var claimsIdentity = new ClaimsIdentity(claims, "Login");
        var authProps = new AuthenticationProperties() {
            IsPersistent = login.RememberMe //todo - even if remember me is false, the cookie remains through sessions
        };

        await HttpContext.SignInAsync(Cookies.Auth, new ClaimsPrincipal(claimsIdentity), authProps);

        return Redirect("/Home");

    }

    public async Task<IActionResult> Logout() {
        await HttpContext.SignOutAsync(Cookies.Auth);
        return Redirect("/Home");
    }

}
