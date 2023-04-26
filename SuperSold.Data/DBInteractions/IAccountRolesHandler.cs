using OneOf;
using OneOf.Types;
using SuperSold.Data.Models;

namespace SuperSold.Data.DBInteractions;
public interface IAccountRolesHandler {

    Task<OneOf<Success, NotFound>> AddRole(Guid accountId, string role);
    Task<OneOf<Success, NotFound>> DeleteRole(Guid accountId, string role);
    IQueryable<AccountRoleModel> GetAccountRoles(Guid accountId);

}