using Microsoft.AspNetCore.Mvc;
using SuperSold.Data.DBInteractions;
using SuperSold.Data.Models;
using SuperSold.UI.AspDotNet.Services;
using SuperSold.UI.AspDotNet.ViewRouting;

namespace SuperSold.UI.AspDotNet.Controllers;
public class RollbacksController : Controller {

    private readonly IRollbackHandler _rollbackHandler;
    private readonly IAccountsHandler _accountsHandler;
    private readonly IEmailService _emailService;
    private readonly IEmailViewsBuilder _emailBuilder;
    private readonly ILogger<RollbacksController> _logger;

    public RollbacksController(IRollbackHandler rollbackHandler, IAccountsHandler accountsHandler, IEmailService emailService, IEmailViewsBuilder emailBuilder, ILogger<RollbacksController> logger) {
        _rollbackHandler = rollbackHandler;
        _accountsHandler = accountsHandler;
        _emailService = emailService;
        _emailBuilder = emailBuilder;
        _logger = logger;
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

        var result = await _accountsHandler.GetAccountByUserName(username);
        if(result.TryPickT1(out var _, out var account)) {
            return NotFound("The given username does not exist.");
        }

        //create rollback code
        var rollback = new RollbackModel() {
            IdRollback = Guid.NewGuid(),
            IdAccount = account.IdAccount,
            Body = string.Empty,
            ExpireOn = DateTime.UtcNow.AddMinutes(20),
            RollbackType = RollbackType.Password
        };

        await _rollbackHandler.CreateRollback(rollback);

        //send mail
        var email = account.Email;
        var url = Url.Rollbacks().ForgotPassword(rollback.IdAccount, rollback.IdRollback)!;
        var body = _emailBuilder.BuildForgotPasswordEmailHtml(url, rollback.ExpireOn.ToLongTimeString());
        await _emailService.Send(username, email, body);

        return StatusCode(209, $"The reset code has been sent to the email linked to this account. The email starts with [{email[..4]}...]");

    }

    [HttpGet]
    public async Task<IActionResult> ForgotPassword(Guid userId, Guid token) {

        var rollback = await _rollbackHandler.GetRollback(token, userId, RollbackType.Password);
        if(!rollback.TryPickT0(out var rollbackModel, out var _)) {
            return NotFound("The rollback token or user id do not exist.");
        }

        if(DateTime.UtcNow > rollbackModel.ExpireOn) {
            await _rollbackHandler.ExpireRollback(token);
            return Unauthorized("The token has expired");
        }
        
        return View();

    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(Guid userId, Guid token, string newPassword, string confirmPassword) {

        if(newPassword != confirmPassword) {
            return BadRequest("The new password and confirm password do not match.");
        }

        var rollbackOption = await _rollbackHandler.GetRollback(token, userId, RollbackType.Password);
        if(!rollbackOption.TryPickT0(out var rollbackModel, out var _)) {
            return NotFound("The rollback token or user id do not exist.");
        }

        if(DateTime.UtcNow > rollbackModel.ExpireOn) {
            await _rollbackHandler.ExpireRollback(token);
            return Unauthorized("The token has expired");
        }

        var result = await _accountsHandler.ChangePassword(userId, newPassword);
        if(result.TryPickT1(out var _, out var success)) {
            return NotFound("The user id does not exist.");
        }

        await _rollbackHandler.ExpireRollback(token);
        return Ok();

    }

    [HttpPost]
    public async Task<IActionResult> RollbackEmail(Guid userId, Guid token) {

        var rollback = await _rollbackHandler.GetRollback(token, userId, RollbackType.Email);
        if(!rollback.TryPickT0(out var rollbackModel, out var _)) {
            return NotFound("The rollback token or user id do not exist.");
        }

        if(DateTime.UtcNow > rollbackModel.ExpireOn) {
            await _rollbackHandler.ExpireRollback(token);
            return Unauthorized("The token has expired");
        }

        if(string.IsNullOrWhiteSpace(rollbackModel.Body)) {
            return StatusCode(500, "The previous email is missing from the saved token's body.");
        }

        var curr = await _accountsHandler.GetAccountById(userId);
        if(curr.TryPickT1(out var _, out var account)) {
            return NotFound("The rollback token or user id do not exist.");
        }

        var result = await _accountsHandler.ChangeEmail(userId, rollbackModel.Body);
        if(result.TryPickT1(out var _, out var _)) {
            return NotFound("There has been an error finding the account to rollback the email.");
        }

        await _rollbackHandler.ExpireRollback(token);
        return Ok("The email has been rerolled back successfully.");

    }

}
