using SuperSold.Data.DBInteractions;
using System.Security.Claims;

namespace SuperSold.Identification;

public class PermissionsBuilder : IPermissionsBuilder {

    private readonly IAccountRolesHandler _rolesHandler;
    private readonly IAccountRestrictionsHandler _restrictionsHandler;

    public PermissionsBuilder(IAccountRolesHandler rolesHandler, IAccountRestrictionsHandler restrictionsHandler) {
        _rolesHandler = rolesHandler;
        _restrictionsHandler = restrictionsHandler;
    }

    public IQueryable<Claim> GetRoleClaims(Guid accountId) {
        return _rolesHandler.GetAccountRoles(accountId).Select(x => new Claim(x.Role, "true"));
    }

    public IQueryable<Claim> GetRestrictionClaims(Guid accountId) {
        return _restrictionsHandler.GetAccountRestrictions(accountId).Select(x => new Claim(x.Restriction, "false"));
    }

}
