using Microsoft.AspNetCore.Mvc;
using SuperSold.Data.DBInteractions;
using SuperSold.Data.Models;
using SuperSold.Identification;
using SuperSold.UI.AspDotNet.Services;
using SuperSold.UI.AspDotNet.ViewRouting;

namespace SuperSold.UI.AspDotNet.Handlers.Profile.Commands;
using Response = OneOf<Success, Unauthorized, NotFound>;

public record ChangeEmailCommand(Guid UserId, string Password, string NewEmail, IUrlHelper UrlHelper) : IRequest<Response>;

public class ChangeEmailHandler : IRequestHandler<ChangeEmailCommand, Response> {

    private readonly IAuthenticator _authenticator;
    private readonly IEmailService _emailService;
    private readonly IEmailViewsBuilder _emailViewsBuilder;
    private readonly IRollbackHandler _rollbackHandler;
    private readonly IAccountsHandler _accountsHandler;

    public ChangeEmailHandler(IAuthenticator authenticator, IEmailService emailService, 
                            IEmailViewsBuilder emailViewsBuilder, IRollbackHandler rollbackHandler, 
                            IAccountsHandler accountsHandler) {
        _authenticator = authenticator;
        _emailService = emailService;
        _emailViewsBuilder = emailViewsBuilder;
        _rollbackHandler = rollbackHandler;
        _accountsHandler = accountsHandler;
    }

    public async Task<Response> Handle(ChangeEmailCommand request, CancellationToken cancellationToken) {

        var passwordCheck = await _authenticator.Verify(request.UserId, request.Password);
        if(!passwordCheck.TryPickT0(out var _, out var passRemainder)) {
            return passRemainder.Match<Response>(
                notfound => notfound,
                wrongpassword => new Unauthorized()
            );
        }

        var cached = await _accountsHandler.GetAccountById(request.UserId);
        if(cached.TryPickT1(out var _, out var cachedAccount)) {
            return new NotFound();
        }

        var rollback = new RollbackModel() {
            IdRollback = Guid.NewGuid(),
            IdAccount = request.UserId,
            Body = cachedAccount.Email,
            RollbackType = RollbackType.Email,
            ExpireOn = DateTime.UtcNow.AddDays(3)
        };

        await _rollbackHandler.CreateRollback(rollback);

        var pathToRollback = request.UrlHelper.Rollbacks().RollbackEmail(request.UserId, rollback.IdRollback)!;
        var emailBody = _emailViewsBuilder.BuildRollbackEmailHtml(request.NewEmail, pathToRollback, rollback.ExpireOn.ToLongDateString());
        await _emailService.Send(cachedAccount.UserName, cachedAccount.Email, emailBody);

        var result = await _accountsHandler.ChangeEmail(request.UserId, request.NewEmail);
        return result.Match<Response>(x => x, x => x);

    }
}
