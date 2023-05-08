using SuperSold.Data.DBInteractions;
using SuperSold.Identification;
using SuperSold.UI.AspDotNet.Services;

namespace SuperSold.UI.AspDotNet.Handlers.Profile.Commands;
using Response = OneOf<Success, Unauthorized, NotFound>;

public record DeleteAccountCommand(Guid UserId, string Password) : IRequest<Response>;

public class DeleteAccountHandler : IRequestHandler<DeleteAccountCommand, Response> {

    private readonly IAuthenticator _authenticator;
    private readonly IAccountsHandler _accountsHandler;
    private readonly IAuthService _authService;

    public DeleteAccountHandler(IAuthenticator authenticator, IAccountsHandler accountsHandler, IAuthService authService) {
        _authenticator = authenticator;
        _accountsHandler = accountsHandler;
        _authService = authService;
    }

    public async Task<Response> Handle(DeleteAccountCommand request, CancellationToken cancellationToken) {

        var passwordCheck = await _authenticator.Verify(request.UserId, request.Password);
        if(!passwordCheck.TryPickT0(out var _, out var passRemainder)) {
            return passRemainder.Match<Response>(
                notfound => notfound,
                wrongpassword => new Unauthorized()
            );
        }

        var result = await _accountsHandler.DeleteAccount(request.UserId);
        if(result.TryPickT1(out var notfound, out var success)) {
            return notfound;
        }

        await _authService.Logout();
        return success;

    }
}
