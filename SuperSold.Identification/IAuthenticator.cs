using OneOf;
using OneOf.Types;
using System.Security.Claims;

namespace SuperSold.Identification;
public interface IAuthenticator {
    Task<OneOf<ClaimsPrincipal, NotFound, Authenticator.WrongPassword>> Login(string userName, string password);
}