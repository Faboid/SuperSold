using SuperSold.UI.AspDotNet.Constants;
using System.Security.Claims;

namespace SuperSold.UI.AspDotNet.Extensions;

public static class ClaimsPrincipalExtensions {

    public static Guid GetIdentity(this ClaimsPrincipal principal) {
        var userId = principal.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        var asGuid = Guid.Parse(userId);
        return asGuid;
    }

    public static bool IsAdmin(this ClaimsPrincipal principal) {
        var userId = principal.Claims.FirstOrDefault(x => x.Type == Roles.Admin)?.Value;
        return userId == "true";
    }

}
