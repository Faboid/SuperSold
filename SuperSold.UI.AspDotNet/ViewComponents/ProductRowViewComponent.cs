using Microsoft.AspNetCore.Mvc;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.ViewComponents;

public class ProductRowViewComponent : ViewComponent {

    public IViewComponentResult Invoke(string rowTemplateName, Product product) {
        return View(rowTemplateName, product);
    }

}