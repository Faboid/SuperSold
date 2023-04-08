using Microsoft.AspNetCore.Mvc;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.ViewComponents;

public class RelationshipRowViewComponent : ViewComponent {

    public IViewComponentResult Invoke(string rowTemplateName, ProductWithSavedRelationship relationship) {
        return View(rowTemplateName, relationship);
    }

}