using Microsoft.AspNetCore.Authentication;
using NuGet.Protocol.Plugins;
using OneOf;
using OneOf.Types;
using SuperSold.Data.Models.ResponseTypes;
using SuperSold.Identification;
using SuperSold.UI.AspDotNet.Constants;
using static SuperSold.Identification.Authenticator;

namespace SuperSold.UI.AspDotNet.Services;

public interface IAuthService {

    Task<OneOf<Success, AlreadyExists>> SignUp(string username, string email, string password, bool rememberMe);
    Task<OneOf<Success, NotFound, WrongPassword>> Login(string username, string password, bool rememberMe);
    Task Logout();

}

public class AuthService : IAuthService {

    private readonly IHttpContextAccessor _http;
    private readonly IAuthenticator _authenticator;

    public AuthService(IAuthenticator authenticator, IHttpContextAccessor http) {
        _authenticator = authenticator;
        _http = http;
    }

    public async Task<OneOf<Success, NotFound, WrongPassword>> Login(string username, string password, bool rememberMe) {
        
        var result = await _authenticator.Login(username, password);

        if(!result.TryPickT0(out var principal, out var remainder)) {
            return remainder.Match<OneOf<Success, NotFound, WrongPassword>>(x => x, x => x);
        }

        var authProps = BuildAuthProps(rememberMe);
        await _http.HttpContext!.SignInAsync(Cookies.Auth, principal, authProps);
        return new Success();

    }

    public async Task<OneOf<Success, AlreadyExists>> SignUp(string username, string email, string password, bool rememberMe) {

        var result = await _authenticator.SignUp(username, email, password);

        if(result.TryPickT1(out var alreadyExists, out var principal)) {
            return alreadyExists;
        }

        var authProps = BuildAuthProps(rememberMe);
        await _http.HttpContext!.SignInAsync(Cookies.Auth, principal, authProps);
        return new Success();

    }

    public async Task Logout() => await _http.HttpContext!.SignOutAsync(Cookies.Auth);

    private static AuthenticationProperties BuildAuthProps(bool rememberMe) {
        return new AuthenticationProperties() {
            IsPersistent = rememberMe
        };
    }

}
