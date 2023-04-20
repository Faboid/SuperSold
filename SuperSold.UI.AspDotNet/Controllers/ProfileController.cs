using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OneOf;
using OneOf.Types;
using SuperSold.Data.DBInteractions;
using SuperSold.Data.Extensions;
using SuperSold.Identification;
using SuperSold.UI.AspDotNet.Constants;
using SuperSold.UI.AspDotNet.Extensions;
using SuperSold.UI.AspDotNet.Models;
using SuperSold.UI.AspDotNet.Services;

namespace SuperSold.UI.AspDotNet.Controllers;
public class ProfileController : Controller {

    private readonly IAccountsHandler _accountsHandler;
    private readonly IAuthService _authService;
    private readonly IAuthenticator _authenticator;
    private readonly IEmailService _emailService;

    public ProfileController(IAccountsHandler accountsHandler, IAuthenticator authenticator, IAuthService authService, IEmailService emailService) {
        _accountsHandler = accountsHandler;
        _authenticator = authenticator;
        _authService = authService;
        _emailService = emailService;
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

        var userId = User.GetIdentity();
        var passwordCheck = await _authenticator.Verify(userId, password);

        if(!passwordCheck.TryPickT0(out var _, out var remainder)) {
            return remainder.Match<IActionResult>(
                notfound => NotFound(),
                wrongpassword => BadRequest("The given password is wrong.")
            );
        }

        var result = await _accountsHandler.RenameAccount(userId, username);

        return await result.Match<Task<IActionResult>>(
            async success => await RefreshAuthCookiesAndReturnOk(username),
            async notfound => await NotFound().AsTask(),
            async alredyExists => await BadRequest("The new username is already in use.").AsTask()
        );

    }

    [HttpPost]
    public async Task<IActionResult> ChangeEmail(string email, string password) {

        var accountId = User.GetIdentity();
        var passwordCheck = await _authenticator.Verify(accountId, password);

        if(!passwordCheck.TryPickT0(out var _, out var remainder)) {
            return remainder.Match<IActionResult>(
                notfound => NotFound(),
                wrongpassword => BadRequest("The given password is wrong.")
            );
        }

        var cached = await _accountsHandler.GetAccountById(accountId);
        if(cached.TryPickT1(out var _, out var cachedAccount)) {
            return NotFound();
        }

        await _emailService.Send(cachedAccount.UserName, cachedAccount.Email, "<html><h1>As requested, your SuperSold account email has been moved to {email}. If it wasn't you, you can rollback with <a href=\"dunno yet\">this link</a> to use this email again. The link will expire in 3 days.</h1></html>");

        var result = await _accountsHandler.ChangeEmail(accountId, email);
        return result.Match<IActionResult>(
            success => Ok(),
            notFound => NotFound()
        );

    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(string newPassword, string password) {

        var accountId = User.GetIdentity();
        var passwordCheck = await _authenticator.Verify(accountId, password);

        if(!passwordCheck.TryPickT0(out var _, out var remainder)) {
            return remainder.Match<IActionResult>(
                notfound => NotFound(),
                wrongpassword => BadRequest("The given password is wrong.")
            );
        }

        var hashPassword = _authenticator.HashPassword(newPassword);
        var result = await _accountsHandler.ChangePassword(accountId, hashPassword);
        return result.Match<IActionResult>(
            success => Ok(),
            notfound => NotFound()
        );

    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAccount(string password) {

        var accountId = User.GetIdentity();
        var passwordCheck = await _authenticator.Verify(accountId, password);

        if(!passwordCheck.TryPickT0(out var _, out var remainder)) {
            return remainder.Match<IActionResult>(
                notfound => NotFound(),
                wrongpassword => BadRequest("The given password is wrong.")
            );
        }

        var result = await _accountsHandler.DeleteAccount(accountId);

        return await result.Match<Task<IActionResult>>(
            async success => {
                await HttpContext.SignOutAsync(Cookies.Auth);
                return Ok();
            },
            async notfound => await NotFound("There has been an error retrieving the account information for deletion.").AsTask()
        );

    }

    private async Task<IActionResult> RefreshAuthCookiesAndReturnOk(string newname) {
        await _authService.RefreshAuthCookieWithNewUserName(newname);
        return Ok();
    }

}
