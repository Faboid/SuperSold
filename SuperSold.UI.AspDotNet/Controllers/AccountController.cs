using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SuperSold.UI.AspDotNet.Constants;
using SuperSold.UI.AspDotNet.Models;
using System.Security.Claims;

namespace SuperSold.UI.AspDotNet.Controllers;
public class AccountController : Controller {

    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel login) {

        if(login.Password != "12345678") {
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
