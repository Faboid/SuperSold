using Microsoft.AspNetCore.Mvc;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.ViewComponents;

public class AccountDisplayViewComponent : ViewComponent {

    public IViewComponentResult Invoke(AccountInfoModel account, string displayType) {
        return View(displayType, account);
    }

}
