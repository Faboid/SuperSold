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

    public async Task<OneOf<Success, NotFound>> DeleteAccount(Guid accountId) {

        var account = await _context.Accounts.FindAsync(accountId);
        if(account is null) {
            return new NotFound();
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

    public async Task<bool> UserNameExists(string accountName) {
        return await _context.Accounts.AnyAsync(x => x.UserName == accountName);
    }

    public async Task<bool> AccountExists(Guid accountId) {
        return await _context.Accounts.AnyAsync(x => x.IdAccount == accountId);
    }

    public async Task<OneOf<AccountModel, NotFound>> GetAccountById(Guid guid) {
        var result = await _context.Accounts.FindAsync(guid);
        if(result is null) {
            return new NotFound();
        }

        return result;
    }

    public async Task<OneOf<Success, NotFound, AlreadyExists>> RenameAccount(Guid accountId, string newName) {

        var account = await _context
            .Accounts
            .FindAsync(accountId);

        if(account is null) {
            return new NotFound();
        }

        if(await UserNameExists(newName)) {
            return new AlreadyExists();
        }

        account.UserName = newName;
        await _context.SaveChangesAsync();
        return new Success();
    }

    public async Task<OneOf<Success, NotFound>> ChangeEmail(Guid accountId, string newEmail) {

        var account = await _context
            .Accounts
            .FindAsync(accountId);

        if(account is null) {
            return new NotFound();
        }

        account.Email = newEmail;
        await _context.SaveChangesAsync();
        return new Success();

    }

    public async Task<OneOf<Success, NotFound>> ChangePassword(Guid accountId, string newHashedPassword) {

        var account = await _context
            .Accounts
            .FindAsync(accountId);

        if(account is null) {
            return new NotFound();
        }

        account.HashedPassword = newHashedPassword;
        await _context.SaveChangesAsync();
        return new Success();

    }

}

