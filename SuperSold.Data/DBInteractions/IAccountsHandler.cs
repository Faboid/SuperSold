using OneOf;
using OneOf.Types;
using SuperSold.Data.Models;
using SuperSold.Data.Models.ResponseTypes;

namespace SuperSold.Data.DBInteractions;
public interface IAccountsHandler {

    Task<OneOf<AccountModel, NotFound>> GetAccountByUserName(string accountName);
    Task<OneOf<AccountModel, NotFound>> GetAccountById(Guid accountId);
    Task<bool> UserNameExists(string userName);
    Task<bool> AccountExists(Guid accountId);
    Task<OneOf<Success, NotFound, AlreadyExists>> RenameAccount(Guid accountId, string newName);
    Task<OneOf<Success, NotFound>> ChangeEmail(Guid accountId, string newEmail);
    Task<OneOf<Success, NotFound>> ChangePassword(Guid accountId, string newPassword);
    Task<OneOf<Success, NotFound>> DeleteAccount(Guid accountId);
    Task<OneOf<Success, AlreadyExists>> CreateAccount(AccountModel model);

}
