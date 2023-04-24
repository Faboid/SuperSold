using Microsoft.AspNetCore.Mvc;

namespace SuperSold.UI.AspDotNet.ViewRouting.ControllersPaths;

public class RollbacksRoutes : BaseRoutes
{

    public RollbacksRoutes(IUrlHelper urlHelper) : base(urlHelper, "Rollbacks") { }

    public string? ForgotPasswordRequest() => BuildUrlToAction("ForgotPasswordRequest");
    public string? ForgotPassword() => BuildUrlToAction("ForgotPassword");
    public string? RollbackEmail() => BuildUrlToAction("RollbackEmail");

    //used to send the link from outside the application's page
    public string? RollbackEmail(Guid userId, Guid token) => $"{BuildAbsoluteUrlToAction("RollbackEmail")}?userId={userId}&token={token}";
    public string? ForgotPassword(Guid userId, Guid token) => $"{BuildAbsoluteUrlToAction("ForgotPassword")}?userId={userId}&token={token}";

}