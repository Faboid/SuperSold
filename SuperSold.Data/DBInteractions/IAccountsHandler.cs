using OneOf;
using OneOf.Types;
using SuperSold.Data.Models;

namespace SuperSold.Data.DBInteractions;
public interface IAccountsHandler {

    record struct AlreadyExists();

    Task<OneOf<AccountModel, NotFound>> GetAccountByUserName(string accountName);
    Task<OneOf<Success, NotFound, AlreadyExists>> RenameAccount(string accountName, string newName);
    Task<OneOf<Success, NotFound>> ChangeEmail(string accountName, string newEmail);
    Task<OneOf<Success, NotFound>> DeleteAccount(string accountName);
    Task<OneOf<Success, AlreadyExists>> CreateAccount(AccountModel model);

}
