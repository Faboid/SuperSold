using OneOf;
using OneOf.Types;
using SuperSold.Data.Models.ResponseTypes;
using System.Security.Claims;

namespace SuperSold.Identification;
public interface IAuthenticator {
    Task<OneOf<ClaimsPrincipal, NotFound, Authenticator.WrongPassword>> Login(string userName, string password);
    Task<OneOf<ClaimsPrincipal, AlreadyExists>> SignUp(string userName, string email, string password);
}