using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SuperSold.Data.Extensions;
using SuperSold.Identification;
using SuperSold.UI.AspDotNet.Constants;
using SuperSold.UI.AspDotNet.Models;
using System.Security.Claims;

namespace SuperSold.UI.AspDotNet.Controllers;
public class AuthenticationController : Controller {

    private readonly IAuthenticator _authenticator;

    public AuthenticationController(IAuthenticator authenticator) {
        _authenticator = authenticator;
    }

    [HttpGet]
    [RequireHttps]
    public IActionResult SignUp() => View();

    [HttpPost]
    [RequireHttps]
    public async Task<IActionResult> SignUp(SignUpModel model) {

        if(!ModelState.IsValid) {
            return View();
        }

        var result = await _authenticator.SignUp(model.UserName, model.Email, model.Password);
        var authProps = new AuthenticationProperties() {
            IsPersistent = model.RememberMe
        };

        return await result.Match(
            async principal => await LoginAndRedirect(principal, authProps),
            alreadyExists => ErrorMessageAndRetry("The given username is already in use.").AsTask()
        );

    }

    [HttpGet]
    [RequireHttps]
    public IActionResult Login() => View();

    [HttpPost]
    [RequireHttps]
    public async Task<IActionResult> Login(LoginModel login) {

        if(!ModelState.IsValid) {
            return View();
        }

        var result = await _authenticator.Login(login.UserName, login.Password);

        var authProps = new AuthenticationProperties() {
            IsPersistent = login.RememberMe
        };

        return await result.Match(
            async principal => await LoginAndRedirect(principal, authProps),
            notFound => ErrorMessageAndRetry("The given username does not exist.").AsTask(),
            wrongPass => ErrorMessageAndRetry("The given password is incorrect.").AsTask()
        );

    }

    public async Task<IActionResult> Logout() {
        await HttpContext.SignOutAsync(Cookies.Auth);
        return Redirect("/Home");
    }

    private IActionResult ErrorMessageAndRetry(string message) {
        ViewBag.Message = message;
        return View();
    }

    private async Task<IActionResult> LoginAndRedirect(ClaimsPrincipal principal, AuthenticationProperties authProps) {
        await HttpContext.SignInAsync(Cookies.Auth, principal, authProps);
        return Redirect("/Home");
    }

}
