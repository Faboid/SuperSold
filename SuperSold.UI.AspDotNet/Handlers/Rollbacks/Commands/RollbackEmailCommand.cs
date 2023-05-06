using Newtonsoft.Json.Linq;
using NuGet.Common;
using SuperSold.Data.DBInteractions;
using SuperSold.Data.Models;

namespace SuperSold.UI.AspDotNet.Handlers.Rollbacks.Commands;

public record RollbackEmailCommand(Guid UserId, Guid Token) : IRequest<OneOf<Success, NotFound, ExpiredToken, MissingRollbackBody>>;

public class RollbackEmailHandler : IRequestHandler<RollbackEmailCommand, OneOf<Success, NotFound, ExpiredToken, MissingRollbackBody>> {

    private readonly IRollbackHandler _rollbackHandler;
    private readonly IAccountsHandler _accountsHandler;

    public RollbackEmailHandler(IRollbackHandler rollbackHandler, IAccountsHandler accountsHandler) {
        _rollbackHandler = rollbackHandler;
        _accountsHandler = accountsHandler;
    }

    public async Task<OneOf<Success, NotFound, ExpiredToken, MissingRollbackBody>> Handle(RollbackEmailCommand request, CancellationToken cancellationToken) {

        var rollbackOption = await _rollbackHandler.GetRollback(request.Token, request.UserId, RollbackType.Email);
        if(!rollbackOption.TryPickT0(out var rollback, out var _)) {
            return new NotFound();
        }

        if(DateTime.UtcNow > rollback.ExpireOn) {
            await _rollbackHandler.ExpireRollback(request.Token);
            return new ExpiredToken();
        }

        if(string.IsNullOrWhiteSpace(rollback.Body)) {
            return new MissingRollbackBody();
        }

        var result = await _accountsHandler.ChangeEmail(request.UserId, rollback.Body);
        if(result.TryPickT1(out var notfound, out var success)) {
            return notfound;
        }

        return success;

    }
}