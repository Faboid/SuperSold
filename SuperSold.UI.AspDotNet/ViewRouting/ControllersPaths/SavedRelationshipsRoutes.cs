using Microsoft.AspNetCore.Mvc;

namespace SuperSold.UI.AspDotNet.ViewRouting.ControllersPaths;

public class SavedRelationshipsRoutes : BaseRoutes
{
    public SavedRelationshipsRoutes(IUrlHelper urlHelper) : base(urlHelper, "SavedRelationships") { }

    public string? ModifyQuantity() => BuildUrlToAction("ModifyQuantity");

}