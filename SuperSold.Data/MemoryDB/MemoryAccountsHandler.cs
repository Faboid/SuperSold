using Monads.Core;
using SuperSold.Data.DBInteractions;
using SuperSold.Data.Models;
using System.Security.Principal;

namespace SuperSold.Data.MemoryDB;

/// <summary>
/// A mock interface for handling accounts in memory. The underlying database does not retain information between sessions—use only for test purposes.
/// </summary>
public class MemoryAccountsHandler : IAccountsHandler {

    private MemoryDatabase _db { get; init; }

    public MemoryAccountsHandler() : this(new()) { }
    public MemoryAccountsHandler(MemoryDatabase db) {
        _db = db;
    }

    public Task<Option<AccountModel>> GetAccountByUserName(string accountName) {

        if(_db.AccountsTable.TryGetValue(accountName, out var account)) {

            //create defensive copy
            var output = new AccountModel() {
                IdAccount = account.IdAccount,
                UserName = account.UserName,
                Email = account.Email,
                HashedPassword = account.HashedPassword,
            };

            return Task.FromResult<Option<AccountModel>>(output);
        }

        return Task.FromResult(Option.None<AccountModel>());
    }

    public Task<bool> ChangeEmail(string accountName, string newEmail) {

        if(_db.AccountsTable.TryGetValue(accountName, out var account)) {
            account.Email = newEmail;
            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }

    public Task<bool> CreateAccount(AccountModel model) {
        
        if(_db.AccountsTable.ContainsKey(model.UserName)) {
            return Task.FromResult(false);
        }

        //create defensive copy
        var toCache = new AccountModel() {
            IdAccount = model.IdAccount,
            UserName = model.UserName,
            Email = model.Email,
            HashedPassword = model.HashedPassword,
        };

        _db.AccountsTable.Add(toCache.UserName, toCache);
        return Task.FromResult(true);

    }

    public Task<bool> DeleteAccount(string accountName) {
        var result = _db.AccountsTable.Remove(accountName);
        return Task.FromResult(result);
    }

    public Task<bool> RenameAccount(string accountName, string newName) {
        
        if(!_db.AccountsTable.Remove(accountName, out var account)) {
            return Task.FromResult(false);
        }

        account.UserName = newName;
        _db.AccountsTable.Add(newName, account);
        return Task.FromResult(true);

    }
}
