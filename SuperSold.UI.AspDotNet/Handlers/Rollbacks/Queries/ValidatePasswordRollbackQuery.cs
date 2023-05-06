using SuperSold.Data.DBInteractions;
using SuperSold.Data.Models;

namespace SuperSold.UI.AspDotNet.Handlers.Rollbacks.Queries;

public record ValidatePasswordRollbackQuery(Guid UserId, Guid Token) : IRequest<OneOf<Success, ExpiredToken, NotFound>>;

public class ValidatePasswordRollbackHandler : IRequestHandler<ValidatePasswordRollbackQuery, OneOf<Success, ExpiredToken, NotFound>> {

    private readonly IRollbackHandler _rollbackHandler;

    public ValidatePasswordRollbackHandler(IRollbackHandler rollbackHandler) {
        _rollbackHandler = rollbackHandler;
    }

    public async Task<OneOf<Success, ExpiredToken, NotFound>> Handle(ValidatePasswordRollbackQuery request, CancellationToken cancellationToken) {
        
        var rollback = await _rollbackHandler.GetRollback(request.Token, request.UserId, RollbackType.Password);
        
        if(!rollback.TryPickT0(out var rollbackModel, out var _)) {
            return new NotFound();
        }

        if(DateTime.UtcNow > rollbackModel.ExpireOn) {
            await _rollbackHandler.ExpireRollback(request.Token);
            return new ExpiredToken();
        }

        return new Success();

    }
}