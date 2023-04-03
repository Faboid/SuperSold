using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SuperSold.Data.DBInteractions;
using SuperSold.Data.Extensions;
using SuperSold.Identification;
using SuperSold.UI.AspDotNet.Constants;
using SuperSold.UI.AspDotNet.Extensions;
using SuperSold.UI.AspDotNet.Models;
using System.Security.Claims;

namespace SuperSold.UI.AspDotNet.Controllers;
public class ProfileController : Controller {

    private readonly IAccountsHandler _accountsHandler;
    private readonly IAuthenticator _authenticator;

    public ProfileController(IAccountsHandler accountsHandler, IAuthenticator authenticator) {
        _accountsHandler = accountsHandler;
        _authenticator = authenticator;
    }

    public async Task<IActionResult> Index() {

        var userId = User.GetIdentity();
        var user = await _accountsHandler.GetAccountById(userId);

        return user.Match<IActionResult>(
            user => View(new AccountInfoModel() { Username = user.UserName, Email = user.Email }),
            notFound => NotFound()
        );
    }

    [AcceptVerbs("GET", "POST")]
    public async Task<IActionResult> IsUsernameUnique(string username) {
        var result = await _accountsHandler.UserNameExists(username);
        return Json(!result);
    }

    [HttpPost]
    public async Task<IActionResult> RenameAccount(string username, string password) {

        var currUsername = User.Identity!.Name!;
        var passwordCheck = await _authenticator.Login(currUsername, password);

        if(!passwordCheck.TryPickT0(out var cookies, out var remainder)) {
            return remainder.Match<IActionResult>(
                notfound => NotFound(),
                wrongpassword => BadRequest("The given password is wrong.")
            );
        }

        var userId = User.GetIdentity();
        var result = await _accountsHandler.RenameAccount(userId, username);

        return await result.Match<Task<IActionResult>>(
            async success => await RefreshAuthCookiesAndReturnOk(cookies, username),
            async notfound => await NotFound().AsTask(),
            async alredyExists => await BadRequest("The new username is already in use.").AsTask()
        );

    }

    private async Task<IActionResult> RefreshAuthCookiesAndReturnOk(ClaimsPrincipal principal, string newname) {

        var features = HttpContext.Features.Get<IAuthenticateResultFeature>()?.AuthenticateResult?.Properties;

        await HttpContext.SignOutAsync(Cookies.Auth);
        if(principal.Identity is not ClaimsIdentity identity) {
            throw new Exception();
        }

        var oldName = identity.FindFirst(ClaimTypes.Name);
        if(oldName != null) {
            identity.RemoveClaim(oldName);
        }

        var newClaim = new Claim(ClaimTypes.Name, newname);
        identity.AddClaim(newClaim);

        await HttpContext.SignInAsync(Cookies.Auth, principal, features ?? new());

        return Ok();
    }

}
