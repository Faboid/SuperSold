using Microsoft.AspNetCore.Mvc;

namespace SuperSold.UI.AspDotNet.ViewRouting;

public class SavedRelationshipsRoutes {

    private readonly IUrlHelper _urlHelper;
    private const string _controllerName = "SavedRelationships";

    public SavedRelationshipsRoutes(IUrlHelper urlHelper) {
        _urlHelper = urlHelper;
    }

    public string? ModifyQuantity() => _urlHelper.Action("ModifyQuantity", _controllerName);

}