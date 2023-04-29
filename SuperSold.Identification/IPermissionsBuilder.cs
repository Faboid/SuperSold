using System.Security.Claims;

namespace SuperSold.Identification;

public interface IPermissionsBuilder {
    IQueryable<Claim> GetRoleClaims(Guid accountId);
    IQueryable<Claim> GetRestrictionClaims(Guid accountId);
}
