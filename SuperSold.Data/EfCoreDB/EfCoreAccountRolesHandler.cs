using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using SuperSold.Data.DBInteractions;
using SuperSold.Data.Models;
using System.Data;

namespace SuperSold.Data.EfCoreDB;

public class EfCoreAccountRolesHandler : EfCoreRepository, IAccountRolesHandler {

    private readonly IAccountsHandler _accountsHandler;

    public EfCoreAccountRolesHandler(EfCoreDBContext context, IAccountsHandler accountsHandler) : base(context) {
        _accountsHandler = accountsHandler;
    }

    public async Task<OneOf<Success, NotFound>> AddRole(Guid accountId, string role) {

        if(!await _accountsHandler.AccountExists(accountId)) {
            return new NotFound();
        }

        var roleModel = new AccountRoleModel() {
            IdAccountRole = Guid.NewGuid(),
            IdAccount = accountId,
            Role = role
        };

        await Context.Accounts_Roles.AddAsync(roleModel);
        await Context.SaveChangesAsync();
        return new Success();

    }

    public async Task<OneOf<Success, NotFound>> DeleteRole(Guid accountId, string role) {
        
        var roleModel = await Context.Accounts_Roles.FirstOrDefaultAsync(x => x.IdAccount == accountId && x.Role == role);
        if(roleModel is null) {
            return new NotFound();
        }

        Context.Accounts_Roles.Remove(roleModel);
        await Context.SaveChangesAsync();
        return new Success();

    }

    public IQueryable<AccountRoleModel> GetAccountRoles(Guid accountId) {
        return Context.Accounts_Roles.Where(x => x.IdAccount == accountId);
    }

}