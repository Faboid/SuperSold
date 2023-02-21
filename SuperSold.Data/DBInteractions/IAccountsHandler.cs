using Monads.Core;
using SuperSold.Data.Models;

namespace SuperSold.Data.DBInteractions;
public interface IAccountsHandler {

    Task<Option<AccountModel>> GetAccountByUserName(string accountName);
    Task<bool> RenameAccount(string accountName, string newName);
    Task<bool> ChangeEmail(string accountName, string newEmail);
    Task<bool> DeleteAccount(string accountName);
    Task<bool> CreateAccount(AccountModel model);

}
