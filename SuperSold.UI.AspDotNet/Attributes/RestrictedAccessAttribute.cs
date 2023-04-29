using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SuperSold.UI.AspDotNet.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public abstract class RestrictedAccessAttribute : Attribute, IAuthorizationFilter {

    private readonly Restriction[] _restrictions;

    protected RestrictedAccessAttribute(params Restriction[] restrictions) {
        _restrictions = restrictions;
    }

    public void OnAuthorization(AuthorizationFilterContext context) {
        
        if(_restrictions.Any(x => context.HttpContext.User.HasClaim(x.Name, x.Value))) {
            context.Result = GetRestrictedView();
        }

    }

    protected abstract IActionResult GetRestrictedView();

}
