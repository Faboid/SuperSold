using Microsoft.AspNetCore.Mvc;

namespace SuperSold.UI.AspDotNet.ViewComponents;

public class ProfileEditorViewComponent : ViewComponent {

    public IViewComponentResult Invoke(string editType, object model) {
        return View(editType, model);
    }

}