using Microsoft.AspNetCore.Authentication;
using OneOf;
using OneOf.Types;
using SuperSold.Data.Models.ResponseTypes;
using SuperSold.Identification;
using System.Security.Claims;
using static SuperSold.Identification.Authenticator;

namespace SuperSold.UI.AspDotNet.Services;

public class AuthService : IAuthService {

    private readonly IAuthCookieService _authCookies;
    private readonly IAuthenticator _authenticator;

    public AuthService(IAuthenticator authenticator, IAuthCookieService authCookies) {
        _authenticator = authenticator;
        _authCookies = authCookies;
    }

    public async Task<OneOf<Success, NotFound, WrongPassword>> Login(string username, string password, bool rememberMe) {
        
        var result = await _authenticator.Login(username, password);

        if(!result.TryPickT0(out var principal, out var remainder)) {
            return remainder.Match<OneOf<Success, NotFound, WrongPassword>>(x => x, x => x);
        }

        var authProps = BuildAuthProps(rememberMe);
        await _authCookies.Login(principal, authProps);
        return new Success();

    }

    public async Task<OneOf<Success, AlreadyExists>> SignUp(string username, string email, string password, bool rememberMe) {

        var result = await _authenticator.SignUp(username, email, password);

        if(result.TryPickT1(out var alreadyExists, out var principal)) {
            return alreadyExists;
        }

        var authProps = BuildAuthProps(rememberMe);
        await _authCookies.Login(principal, authProps);
        return new Success();

    }

    public async Task Logout() => await _authCookies.Logout();

    public async Task RefreshAuthCookieWithNewUserName(string username) {

        var features = _authCookies.GetProperties();
        var principal = _authCookies.GetPrincipal();
        
        await Logout();

        if(principal.Identity is not ClaimsIdentity identity) {
            throw new Exception();
        }

        var oldName = identity.FindFirst(ClaimTypes.Name);
        if(oldName != null) {
            identity.RemoveClaim(oldName);
        }

        var newClaim = new Claim(ClaimTypes.Name, username);
        identity.AddClaim(newClaim);

        await _authCookies.Login(principal, features);

    }

    private static AuthenticationProperties BuildAuthProps(bool rememberMe) {
        return new AuthenticationProperties() {
            IsPersistent = rememberMe
        };
    }

}
