using Microsoft.AspNetCore.Mvc;

namespace SuperSold.UI.AspDotNet.ViewRouting.ControllersPaths;

public class RollbacksRoutes : BaseRoutes
{

    public RollbacksRoutes(IUrlHelper urlHelper) : base(urlHelper, "Rollbacks") { }

    public string? ForgotPassword() => BuildUrlToAction("ForgotPassword");
    public string? RollbackEmail() => BuildUrlToAction("RollbackEmail");
    public string? RollbackEmail(Guid userId, Guid token) => $"{BuildAbsoluteUrlToAction("RollbackEmail")}?userId={userId}&token={token}";

}