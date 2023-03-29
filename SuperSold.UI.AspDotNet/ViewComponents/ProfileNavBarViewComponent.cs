using Microsoft.AspNetCore.Mvc;

namespace SuperSold.UI.AspDotNet.ViewComponents;

public class ProfileNavBarViewComponent : ViewComponent {

    public IViewComponentResult Invoke() {
        
        if(!User.Identity!.IsAuthenticated) {
            return View("Login");
        }

        return View("Default");

    }

}
