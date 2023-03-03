using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using SuperSold.Data.DBInteractions;
using SuperSold.Data.Models;
using SuperSold.Data.Models.ResponseTypes;

namespace SuperSold.Data.EfCoreDB;

public class EfCoreAccountsHandler : IAccountsHandler {

    private readonly EfCoreDBContext _context;

    public EfCoreAccountsHandler(EfCoreDBContext context) {
        _context = context;
    }

    public async Task<OneOf<Success, NotFound>> ChangeEmail(string accountName, string newEmail) {

        var account = await _context
            .Accounts
            .FirstOrDefaultAsync(x => x.UserName == accountName);

        if(account is null) {
            return new NotFound();
        }

        account.Email = newEmail;
        await _context.SaveChangesAsync();
        return new Success();

    }

    public async Task<OneOf<Success, AlreadyExists>> CreateAccount(AccountModel model) {

        var account = await _context.Accounts.FirstOrDefaultAsync(x => x.UserName == model.UserName);
        if(account is not null) {
            return new AlreadyExists();
        }

        await _context.Accounts.AddAsync(model);
        await _context.SaveChangesAsync();
        return new Success();

    }

    public async Task<OneOf<Success, NotFound>> DeleteAccount(string accountName) {

        var get = await GetAccountByUserName(accountName);
        if(get.TryPickT1(out var notFound, out var account)) {
            return notFound;
        }

        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();
        return new Success();

    }

    public async Task<OneOf<AccountModel, NotFound>> GetAccountByUserName(string accountName) {
        
        var account = await _context.Accounts.FirstOrDefaultAsync(x => x.UserName == accountName);
        if(account is null) {
            return new NotFound();
        }

        return account;
        
    }

    public async Task<OneOf<Success, NotFound, AlreadyExists>> RenameAccount(string accountName, string newName) {
        
        var account = await _context
            .Accounts
            .FirstOrDefaultAsync(x => x.UserName == accountName);

        if(account is null) {
            return new NotFound();
        }

        account.UserName = newName;
        await _context.SaveChangesAsync();
        return new Success();
    }

}

