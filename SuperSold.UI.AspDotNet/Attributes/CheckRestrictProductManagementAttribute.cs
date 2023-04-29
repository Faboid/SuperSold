using Microsoft.AspNetCore.Mvc;
using SuperSold.UI.AspDotNet.Constants;

namespace SuperSold.UI.AspDotNet.Attributes;

public class CheckRestrictProductManagementAttribute : RestrictedAccessAttribute {
    public CheckRestrictProductManagementAttribute() : base(new Restriction(Restrictions.ProductSeller, "false")) { }
    protected override IActionResult GetRestrictedView() => new ForbidResult(); //todo - implement view
}