﻿using OneOf;
using OneOf.Types;
using SuperSold.Data.DBInteractions;
using SuperSold.Data.Models;
using System.Security.Claims;

namespace SuperSold.Identification;

public class Authenticator : IAuthenticator {

    private readonly IAccountsHandler _accountsHandler;

    public record struct WrongPassword();

    public Authenticator(IAccountsHandler accountsHandler) {
        _accountsHandler = accountsHandler;
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

        if(account.HashedPassword != password) { //todo - validation with encryption
            return new WrongPassword();
        }

        return new Success();
    }

}
