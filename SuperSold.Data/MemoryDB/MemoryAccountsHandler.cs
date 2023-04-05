using OneOf;
using OneOf.Types;
using SuperSold.Data.DBInteractions;
using SuperSold.Data.Extensions;
using SuperSold.Data.Models;
using SuperSold.Data.Models.ResponseTypes;

namespace SuperSold.Data.MemoryDB;

/// <summary>
/// A mock interface for handling accounts in memory. The underlying database does not retain information between sessions—use only for test purposes.
/// </summary>
public class MemoryAccountsHandler : IAccountsHandler {

    private readonly MemoryDatabase _db;

    public MemoryAccountsHandler() : this(new()) { }
    public MemoryAccountsHandler(MemoryDatabase db) {
        _db = db;
    }

    public Task<OneOf<AccountModel, NotFound>> GetAccountByUserName(string accountName) => InternalGetAccountByUserName(accountName).AsTask();
    private OneOf<AccountModel, NotFound> InternalGetAccountByUserName(string accountName) {

        if(_db.AccountsTable.TryGetValue(accountName, out var account)) {

            //create defensive copy
            var output = new AccountModel() {
                IdAccount = account.IdAccount,
                UserName = account.UserName,
                Email = account.Email,
                HashedPassword = account.HashedPassword,
            };

            return output;
        }

        return new NotFound();

    }

    public Task<OneOf<Success, NotFound>> ChangeEmail(string accountName, string newEmail) => InternalChangeEmail(accountName, newEmail).AsTask();
    private OneOf<Success, NotFound> InternalChangeEmail(string accountName, string newEmail) {

        if(_db.AccountsTable.TryGetValue(accountName, out var account)) {
            account.Email = newEmail;
            return new Success();
        }

        return new NotFound();
    }

    public Task<OneOf<Success, AlreadyExists>> CreateAccount(AccountModel model) => InternalCreateAccount(model).AsTask();
    public OneOf<Success, AlreadyExists> InternalCreateAccount(AccountModel model) {
        
        if(_db.AccountsTable.ContainsKey(model.UserName)) {
            return new AlreadyExists();
        }

        //create defensive copy
        var toCache = new AccountModel() {
            IdAccount = model.IdAccount,
            UserName = model.UserName,
            Email = model.Email,
            HashedPassword = model.HashedPassword,
        };

        _db.AccountsTable.Add(toCache.UserName, toCache);
        return new Success();

    }

    public Task<OneOf<Success, NotFound>> DeleteAccount(Guid accountId) => throw new NotImplementedException();
    private OneOf<Success, NotFound> InternalDeleteAccount(string accountName) {
        var result = _db.AccountsTable.Remove(accountName);
        return result ? new Success() : new NotFound();
    }

    public Task<OneOf<Success, NotFound, AlreadyExists>> RenameAccount(Guid accountId, string newName) => throw new NotImplementedException();
    public OneOf<Success, NotFound, AlreadyExists> InternalRenameAccount(string accountName, string newName) {
        
        if(!_db.AccountsTable.Remove(accountName, out var account)) {
            return new NotFound();
        }

        if(_db.AccountsTable.ContainsKey(newName)) {
            return new AlreadyExists();
        }

        account.UserName = newName;
        _db.AccountsTable.Add(newName, account);
        return new Success();

    }

    public Task<OneOf<AccountModel, NotFound>> GetAccountById(Guid accountId) => throw new NotImplementedException();
    public Task<bool> UserNameExists(string userName) => throw new NotImplementedException();
    public Task<OneOf<Success, NotFound>> ChangeEmail(Guid accountId, string newEmail) => throw new NotImplementedException();
    public Task<OneOf<Success, NotFound>> ChangePassword(Guid accountId, string newPassword) => throw new NotImplementedException();
}
