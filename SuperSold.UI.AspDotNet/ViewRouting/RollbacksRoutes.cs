using Microsoft.AspNetCore.Mvc;

namespace SuperSold.UI.AspDotNet.ViewRouting;

public class RollbacksRoutes : BaseRoutes {

    public RollbacksRoutes(IUrlHelper urlHelper) : base(urlHelper, "Rollbacks") { }

    public string? ForgotPassword() => BuildUrlToAction("ForgotPassword");
    public string? RollbackEmail() => BuildUrlToAction("RollbackEmail");

}