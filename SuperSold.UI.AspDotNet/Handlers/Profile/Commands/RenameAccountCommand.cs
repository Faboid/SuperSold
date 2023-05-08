using Microsoft.AspNetCore.Mvc;
using SuperSold.Data.DBInteractions;
using SuperSold.Data.Models.ResponseTypes;
using SuperSold.Identification;
using SuperSold.UI.AspDotNet.Services;

namespace SuperSold.UI.AspDotNet.Handlers.Profile.Commands;
using Response = OneOf<Success, Unauthorized, NotFound, AlreadyExists>;

public record RenameAccountCommand(Guid UserId, string NewUsername, string Password) : IRequest<Response>;

public class RenameAccountHandler : IRequestHandler<RenameAccountCommand, Response> {

    private readonly IAccountsHandler _accountsHandler;
    private readonly IAuthenticator _authenticator;
    private readonly IAuthService _authService;

    public RenameAccountHandler(IAccountsHandler accountsHandler, IAuthenticator authenticator, IAuthService authService) {
        _accountsHandler = accountsHandler;
        _authenticator = authenticator;
        _authService = authService;
    }

    public async Task<Response> Handle(RenameAccountCommand request, CancellationToken cancellationToken) {

        var passwordCheck = await _authenticator.Verify(request.UserId, request.Password);
        if(!passwordCheck.TryPickT0(out var _, out var passRemainder)) {
            return passRemainder.Match<Response>(
                notfound => notfound,
                wrongpassword => new Unauthorized()
            );
        }

        var result = await _accountsHandler.RenameAccount(request.UserId, request.NewUsername);
        if(!result.TryPickT0(out var success, out var remainder)) {
            return remainder.Match<Response>(x => x, x => x);
        }

        await _authService.RefreshAuthCookieWithNewUserName(request.NewUsername);
        return success;

    }
}
