using Microsoft.AspNetCore.Mvc;
using SuperSold.Data.DBInteractions;
using SuperSold.Data.Models;
using SuperSold.UI.AspDotNet.ResponseTypes;
using SuperSold.UI.AspDotNet.Services;
using SuperSold.UI.AspDotNet.ViewRouting;

namespace SuperSold.UI.AspDotNet.Handlers.Rollbacks.Commands;

public record CreateForgotPasswordCodeCommand(string Username, IUrlHelper UrlHelper) : IRequest<OneOf<Success<Email>, NotFound>>;

public class CreateForgotPasswordCodeHandler : IRequestHandler<CreateForgotPasswordCodeCommand, OneOf<Success<Email>, NotFound>> {

    private readonly IRollbackHandler _rollbackHandler;
    private readonly IAccountsHandler _accountsHandler;
    private readonly IEmailService _emailService;
    private readonly IEmailViewsBuilder _emailBuilder;
    private readonly IHttpContextAccessor _contextAccessor;

    public CreateForgotPasswordCodeHandler(IRollbackHandler rollbackHandler, IAccountsHandler accountsHandler,
                                            IEmailService emailService, IEmailViewsBuilder emailBuilder, IHttpContextAccessor contextAccessor) {
        _rollbackHandler = rollbackHandler;
        _accountsHandler = accountsHandler;
        _emailService = emailService;
        _emailBuilder = emailBuilder;
        _contextAccessor = contextAccessor;
    }

    public async Task<OneOf<Success<Email>, NotFound>> Handle(CreateForgotPasswordCodeCommand request, CancellationToken cancellationToken) {

        var result = await _accountsHandler.GetAccountByUserName(request.Username);
        if(result.TryPickT1(out var notfound, out var account)) {
            return notfound;
        }

        //create rollback code
        var rollback = new RollbackModel() {
            IdRollback = Guid.NewGuid(),
            IdAccount = account.IdAccount,
            Body = string.Empty,
            ExpireOn = DateTime.UtcNow.AddMinutes(20),
            RollbackType = RollbackType.Password
        };

        await _rollbackHandler.CreateRollback(rollback);

        //send mail
        var email = account.Email;
        var url = request.UrlHelper.Rollbacks().ForgotPassword(rollback.IdAccount, rollback.IdRollback)!;
        var body = _emailBuilder.BuildForgotPasswordEmailHtml(url, rollback.ExpireOn.ToLongTimeString());
        await _emailService.Send(request.Username, email, body);

        return new Success<Email>(email);

    }
}
