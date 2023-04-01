using Microsoft.AspNetCore.Mvc;
using SuperSold.Data.DBInteractions;
using SuperSold.UI.AspDotNet.Extensions;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.Controllers;
public class ProfileController : Controller {

    private readonly IAccountsHandler _accountsHandler;

    public ProfileController(IAccountsHandler accountsHandler) {
        _accountsHandler = accountsHandler;
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

}
