using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SuperSold.Data.Extensions;
using SuperSold.UI.AspDotNet.Constants;
using SuperSold.UI.AspDotNet.Models;
using SuperSold.UI.AspDotNet.Services;

namespace SuperSold.UI.AspDotNet.Controllers;
public class AuthenticationController : Controller {

    private readonly IAuthService _authService;

    public AuthenticationController(IAuthService authService) {
        _authService = authService;
    }

    [HttpGet]
    [RequireHttps]
    public IActionResult SignUp() => View();

    [HttpPost]
    [RequireHttps]
    public async Task<IActionResult> SignUp(SignUpModel model) {

        if(!ModelState.IsValid) {
            return BadRequest();
        }

        var result = await _authService.SignUp(model.UserName, model.Email, model.Password, model.RememberMe);

        return result.Match(
            success => Redirect("/Home"),
            alreadyExists => ErrorMessageAndRetry("The given username is already in use.", Conflict())
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

        var result = await _authService.Login(login.UserName, login.Password, login.RememberMe);

        return result.Match(
            success => Redirect("/Home"),
            notFound => ErrorMessageAndRetry("The given username does not exist.", NotFound()),
            wrongPass => ErrorMessageAndRetry("The given password is incorrect.", BadRequest())
        );

    }

    public async Task<IActionResult> Logout() {
        await _authService.Logout();
        return Redirect("/Home");
    }

    private IActionResult ErrorMessageAndRetry(string message, IActionResult error) {
        ViewBag.Message = message;
        return error;
    }

}
