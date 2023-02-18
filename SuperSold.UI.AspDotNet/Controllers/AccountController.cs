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

        if(login.UserName == "admin" && login.Password == "1234") {

            var claims = new List<Claim> {
                new(ClaimTypes.Name, login.UserName)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Login");
            var authProps = new AuthenticationProperties() {
                IsPersistent = login.RememberMe
            };
            
            await HttpContext.SignInAsync(Cookies.Auth, new ClaimsPrincipal(claimsIdentity), authProps);

            return Redirect("/Home");
        }

        return View();

    }

    public async Task<IActionResult> Logout() {
        await HttpContext.SignOutAsync(Cookies.Auth);
        return Redirect("/Home");
    }

}
