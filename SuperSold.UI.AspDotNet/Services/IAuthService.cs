using OneOf;
using OneOf.Types;
using SuperSold.Data.Models.ResponseTypes;
using static SuperSold.Identification.Authenticator;

namespace SuperSold.UI.AspDotNet.Services;

public interface IAuthService {

    Task<OneOf<Success, AlreadyExists>> SignUp(string username, string email, string password, bool rememberMe);
    Task<OneOf<Success, NotFound, WrongPassword>> Login(string username, string password, bool rememberMe);
    Task Logout();
    Task RefreshAuthCookieWithNewUserName(string username);

}
