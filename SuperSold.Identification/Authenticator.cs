using OneOf;
using OneOf.Types;
using SuperSold.Data.DBInteractions;
using SuperSold.Data.Models;
using SuperSold.Data.Models.ResponseTypes;
using System.Security.Claims;
using BC = BCrypt.Net.BCrypt;

namespace SuperSold.Identification;

public class Authenticator : IAuthenticator {

    private readonly IAccountsHandler _accountsHandler;

    public record struct WrongPassword();

    public Authenticator(IAccountsHandler accountsHandler) {
        _accountsHandler = accountsHandler;
    }

    public async Task<OneOf<Success, NotFound, WrongPassword>> Verify(Guid userId, string password) {

        var query = await _accountsHandler.GetAccountById(userId);
        return query.Match(
            account => VerifyAccount(account, password)
                .Match<OneOf<Success, NotFound, WrongPassword>>(
                    success => success, 
                    wrongpassword => wrongpassword
                ),
            notfound => notfound
        );

    }

    public async Task<OneOf<ClaimsPrincipal, AlreadyExists>> SignUp(string userName, string email, string password) {
        
        var accountModel = new AccountModel() {
            IdAccount = Guid.NewGuid(),
            UserName = userName,
            Email = email,
            HashedPassword = BC.EnhancedHashPassword(password)
        };

        var result = await _accountsHandler.CreateAccount(accountModel);
        return result.MapT0(success => BuildPrincipal(accountModel));

    }

    public async Task<OneOf<ClaimsPrincipal, NotFound, WrongPassword>> Login(string userName, string password) {
        var account = await _accountsHandler.GetAccountByUserName(userName);
        return Login(account, password);
    }

    private static OneOf<ClaimsPrincipal, NotFound, WrongPassword> Login(OneOf<AccountModel, NotFound> queryResult, string password) {

        if(queryResult.TryPickT1(out var notfound, out var account)) {
            return notfound;
        }

        return VerifyAccount(account, password).Match<OneOf<ClaimsPrincipal, NotFound, WrongPassword>>(
            success => BuildPrincipal(account),
            wrongPass => wrongPass
        );

    }

    private static ClaimsPrincipal BuildPrincipal(AccountModel account) {
        var builder = new ClaimsBuilder("Login");
        builder.AddClaim(new Claim(ClaimTypes.Name, account.UserName));
        builder.AddClaim(new Claim(ClaimTypes.NameIdentifier, account.IdAccount.ToString()!));
        return builder.BuildPrincipal();
    }

    private static OneOf<Success, WrongPassword> VerifyAccount(AccountModel account, string password) {

        if(!BC.EnhancedVerify(password, account.HashedPassword)) {
            return new WrongPassword();
        }

        return new Success();
    }

}

