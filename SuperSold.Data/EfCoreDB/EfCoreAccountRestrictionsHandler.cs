using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using SuperSold.Data.DBInteractions;
using SuperSold.Data.Models;
using System.Data;

namespace SuperSold.Data.EfCoreDB;

public class EfCoreAccountRestrictionsHandler : EfCoreRepository, IAccountRestrictionsHandler {

    private readonly IAccountsHandler _accountsHandler;
    public EfCoreAccountRestrictionsHandler(EfCoreDBContext context, IAccountsHandler accountsHandler) : base(context) {
        _accountsHandler = accountsHandler;
    }

    public async Task<OneOf<Success, NotFound>> AddRestriction(Guid accountId, string restriction) {

        if(!await _accountsHandler.AccountExists(accountId)) {
            return new NotFound();
        }

        var restrictionModel = new AccountRestrictionModel() {
            IdAccountRestriction = Guid.NewGuid(),
            IdAccount = accountId,
            Restriction = restriction
        };

        await Context.Accounts_Restrictions.AddAsync(restrictionModel);
        await Context.SaveChangesAsync();
        return new Success();

    }

    public async Task<OneOf<Success, NotFound>> DeleteRestriction(Guid accountId, string restriction) {

        var restrictionModel = await Context.Accounts_Restrictions.FirstOrDefaultAsync(x => x.IdAccount == accountId && x.Restriction == restriction);
        if(restrictionModel is null) {
            return new NotFound();
        }

        Context.Accounts_Restrictions.Remove(restrictionModel);
        await Context.SaveChangesAsync();
        return new Success();

    }

    public IQueryable<AccountRestrictionModel> GetAccountRestrictions(Guid accountId) {
        return Context.Accounts_Restrictions.Where(x => x.IdAccount == accountId);
    }
}
