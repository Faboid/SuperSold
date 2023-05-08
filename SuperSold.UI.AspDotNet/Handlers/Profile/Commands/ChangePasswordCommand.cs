using SuperSold.Data.DBInteractions;
using SuperSold.Identification;
using SuperSold.UI.AspDotNet.Services;

namespace SuperSold.UI.AspDotNet.Handlers.Profile.Commands;
using Response = OneOf<Success, Unauthorized, NotFound>;

public record ChangePasswordCommand(Guid UserId, string NewPassword, string Password) : IRequest<Response>;

public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, Response> {

    private readonly IAuthenticator _authenticator;
    private readonly IAccountsHandler _accountsHandler;

    public ChangePasswordHandler(IAuthenticator authenticator, IAccountsHandler accountsHandler) {
        _authenticator = authenticator;
        _accountsHandler = accountsHandler;
    }

    public async Task<Response> Handle(ChangePasswordCommand request, CancellationToken cancellationToken) {

        var passwordCheck = await _authenticator.Verify(request.UserId, request.Password);
        if(!passwordCheck.TryPickT0(out var _, out var passRemainder)) {
            return passRemainder.Match<Response>(
                notfound => notfound,
                wrongpassword => new Unauthorized()
            );
        }

        var hashPassword = _authenticator.HashPassword(request.NewPassword);
        var result = await _accountsHandler.ChangePassword(request.UserId, hashPassword);
        return result.Match<Response>(x => x, x => x);

    }
}
