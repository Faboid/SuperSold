using Microsoft.AspNetCore.Mvc;

namespace SuperSold.UI.AspDotNet.ViewRouting;

public class ProfileRoutes : BaseRoutes {

    public ProfileRoutes(IUrlHelper urlHelper) : base(urlHelper, "Profile") { }

    public string? Index() => BuildUrlToAction("Index");
    public string? IsUsernameUnique() => BuildUrlToAction("IsUsernameUnique");
    public string? RenameAccount() => BuildUrlToAction("RenameAccount");
    public string? ChangeEmail() => BuildUrlToAction("ChangeEmail");
    public string? ChangePassword() => BuildUrlToAction("ChangePassword");
    public string? DeleteAccount() => BuildUrlToAction("DeleteAccount");

}