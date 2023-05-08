using Microsoft.AspNetCore.Mvc;
using SuperSold.UI.AspDotNet.Extensions;
using SuperSold.UI.AspDotNet.Handlers.Profile.Commands;
using SuperSold.UI.AspDotNet.Handlers.Profile.Queries;

namespace SuperSold.UI.AspDotNet.Controllers;

public class ProfileController : Controller {

    private readonly IMediator _mediator;

    public ProfileController(IMediator mediator) {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index() {

        var userId = User.GetIdentity();
        var query = new GetAccountDataQuery(userId);
        var result = await _mediator.Send(query);

        return result.Match<IActionResult>(
            user => View(user),
            notFound => NotFound()
        );
    }

    [AcceptVerbs("GET", "POST")]
    public async Task<IActionResult> IsUsernameUnique(string username) {
        var query = new ValidateUsernameExistenceQuery(username);
        var result = await _mediator.Send(query);
        return Json(!result);
    }

    [HttpPost]
    public async Task<IActionResult> RenameAccount(string username, string password) {

        var userId = User.GetIdentity();
        var command = new RenameAccountCommand(userId, username, password);
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(
            success => Ok(username),
            unauthorized => Unauthorized("The given password is wrong."),
            notfound => NotFound("There has been an error retrieving account information."),
            alreadyExists => Conflict("The new username is already in use by someone.")
        );

    }

    [HttpPost]
    public async Task<IActionResult> ChangeEmail(string email, string password) {

        var accountId = User.GetIdentity();
        var command = new ChangeEmailCommand(accountId, password, email, Url);
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(
            success => Ok("The email has been changed successfully."),
            unauthorized => Unauthorized("The given password is wrong."),
            notfound => NotFound("There has been an error retrieving account information.")
        );

    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(string newPassword, string password) {

        var accountId = User.GetIdentity();
        var command = new ChangePasswordCommand(accountId, newPassword, password);
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(
            success => Ok("The password has been changed successfully."),
            unauthorized => Unauthorized("The given password is wrong."),
            notfound => NotFound("There has been an error retrieving account information.")
        );

    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAccount(string password) {

        var accountId = User.GetIdentity();
        var command = new DeleteAccountCommand(accountId, password);
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(
            success => Ok(),
            unauthorized => Unauthorized("The given password is wrong."),
            notfound => NotFound("There has been an error retrieving account information.")
        );

    }

}
