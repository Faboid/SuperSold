using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SuperSold.UI.AspDotNet.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public abstract class RequiresClaimsAttribute : Attribute, IAuthorizationFilter {

    private readonly RequiredClaim[] _requiredClaims;

    protected RequiresClaimsAttribute(params RequiredClaim[] requiredClaims) {
        _requiredClaims = requiredClaims;
    }

    public void OnAuthorization(AuthorizationFilterContext context) {

        if(!_requiredClaims.All(x => context.HttpContext.User.HasClaim(x.Name, x.Value))) {
            context.Result = GetUnauthorizedView();
        }

    }

    protected abstract IActionResult GetUnauthorizedView();

}