using NuGet.Common;
using SuperSold.Data.DBInteractions;
using SuperSold.Data.Models;
using SuperSold.Identification;

namespace SuperSold.UI.AspDotNet.Handlers.Rollbacks.Commands;

public record RollbackPasswordCommand(Guid UserId, Guid Token, string NewPassword) : IRequest<OneOf<Success, ExpiredToken, NotFound>>;

public class RollbackPasswordHandler : IRequestHandler<RollbackPasswordCommand, OneOf<Success, ExpiredToken, NotFound>> {

    private readonly IRollbackHandler _rollbackHandler;
    private readonly IAccountsHandler _accountsHandler;
    private readonly IAuthenticator _authenticator;

    public RollbackPasswordHandler(IRollbackHandler rollbackHandler, IAccountsHandler accountsHandler, IAuthenticator authenticator) {
        _rollbackHandler = rollbackHandler;
        _accountsHandler = accountsHandler;
        _authenticator = authenticator;
    }

    public async Task<OneOf<Success, ExpiredToken, NotFound>> Handle(RollbackPasswordCommand request, CancellationToken cancellationToken) {

        var rollbackOption = await _rollbackHandler.GetRollback(request.Token, request.UserId, RollbackType.Password);

        if(!rollbackOption.TryPickT0(out var rollback, out var _)) {
            return new NotFound();
        }

        if(DateTime.UtcNow > rollback.ExpireOn) {
            await _rollbackHandler.ExpireRollback(request.Token);
            return new ExpiredToken();
        }

        var hashedPassword = _authenticator.HashPassword(request.NewPassword);
        var result = await _accountsHandler.ChangePassword(request.UserId, hashedPassword);
        if(result.TryPickT1(out var _, out var success)) {
            return new NotFound();
        }

        await _rollbackHandler.ExpireRollback(request.Token);
        return success;

    }
}