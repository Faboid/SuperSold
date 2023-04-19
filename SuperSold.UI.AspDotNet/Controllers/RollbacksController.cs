using Microsoft.AspNetCore.Mvc;
using SuperSold.Data.DBInteractions;
using SuperSold.Data.Models;

namespace SuperSold.UI.AspDotNet.Controllers;
public class RollbacksController : Controller {

    private readonly IRollbackHandler _rollbackHandler;
    private readonly IAccountsHandler _accountsHandler;

    public RollbacksController(IRollbackHandler rollbackHandler, IAccountsHandler accountsHandler) {
        _rollbackHandler = rollbackHandler;
        _accountsHandler = accountsHandler;
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
        return Ok();

    }

}
