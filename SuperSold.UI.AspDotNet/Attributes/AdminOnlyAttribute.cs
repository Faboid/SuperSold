using SuperSold.UI.AspDotNet.Constants;

namespace SuperSold.UI.AspDotNet.Attributes;

public class AdminOnlyAttribute : RequiresClaimsAttribute {
    public AdminOnlyAttribute() : base(new RequiredClaim(Roles.Admin, "true")) { }
}

public record struct RequiredClaim(string Name, string Value);