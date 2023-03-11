using System.Security.Claims;

namespace SuperSold.UI.AspDotNet.Extensions;

public static class ClaimsPrincipalExtensions {

    public static Guid GetIdentity(this ClaimsPrincipal principal) {
        var userId = principal.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        var asGuid = Guid.Parse(userId);
        return asGuid;
    }

}
