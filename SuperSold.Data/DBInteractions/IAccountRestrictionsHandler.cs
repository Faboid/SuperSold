
using OneOf;
using OneOf.Types;
using SuperSold.Data.Models;

namespace SuperSold.Data.DBInteractions;

public interface IAccountRestrictionsHandler {

    Task<OneOf<Success, NotFound>> AddRestriction(Guid accountId, string restriction);
    Task<OneOf<Success, NotFound>> DeleteRestriction(Guid accountId, string restriction);
    IQueryable<AccountRestrictionModel> GetAccountRestrictions(Guid accountId);

}