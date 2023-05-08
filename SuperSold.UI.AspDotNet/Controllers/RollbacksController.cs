using Microsoft.AspNetCore.Mvc;
using SuperSold.Data.DBInteractions;
using SuperSold.UI.AspDotNet.Handlers.Rollbacks.Commands;
using SuperSold.UI.AspDotNet.Handlers.Rollbacks.Queries;

namespace SuperSold.UI.AspDotNet.Controllers;
public class RollbacksController : Controller {

    private readonly IMediator _mediator;

    public RollbacksController(IMediator mediator) {
        _mediator = mediator;
    }

    [HttpGet]
    public IActionResult ForgotPasswordRequest() {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPasswordRequest(string username) {

        if(username is null) {
            return BadRequest("Must insert valid username.");
        }

        var command = new CreateForgotPasswordCodeCommand(username, Url);
        var result = await _mediator.Send(command);

        return result.Match<IActionResult>(
            response => StatusCode(209, $"The reset code has been sent to the email linked to this account. The email starts with [{response.Value.EmailAddress[..4]}...]"),
            notfound => NotFound()
        );

    }

    [HttpGet]
    public async Task<IActionResult> ForgotPassword(Guid userId, Guid token) {

        var query = new ValidatePasswordRollbackQuery(userId, token);
        var result = await _mediator.Send(query);
        return result.Match<IActionResult>(
            success => View((userId, token)),
            expired => Unauthorized("The token has expired"),
            notfound => NotFound("The rollback token or user id do not exist.")
        );

    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(Guid userId, Guid token, string newPassword, string confirmPassword) {

        if(newPassword != confirmPassword) {
            return BadRequest("The new password and confirm password do not match.");
        }

        var command = new RollbackPasswordCommand(userId, token, newPassword);
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(
            success => Ok("The password has been changed successfully."),
            expired => Unauthorized("The token has expired"),
            notfound => NotFound("The rollback token or user id do not exist.")
        );

    }

    [HttpPost]
    public async Task<IActionResult> RollbackEmail(Guid userId, Guid token) {

        var command = new RollbackEmailCommand(userId, token);
        var result = await _mediator.Send(command);
        return result.Match(
            success => Ok("The email has been rerolled back successfully."),
            notfound => NotFound("The rollback token or user id do not exist."),
            expired => Unauthorized("The token has expired"),
            missingBody => StatusCode(500, "The previous email is missing from the saved token's body.")
        );

    }

}
