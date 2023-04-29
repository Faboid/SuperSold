using Microsoft.AspNetCore.Mvc;
using SuperSold.UI.AspDotNet.Constants;

namespace SuperSold.UI.AspDotNet.Attributes;

public class AdminOnlyAttribute : RequiresClaimsAttribute {
    public AdminOnlyAttribute() : base(new RequiredClaim(Roles.Admin, "true")) { }

    protected override IActionResult GetUnauthorizedView() => new ForbidResult(); //todo - implement proper forbidden view
}
