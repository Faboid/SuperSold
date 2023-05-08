using SuperSold.Data.DBInteractions;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.Handlers.Profile.Queries;

public record GetAccountDataQuery(Guid AccountId) : IRequest<OneOf<AccountInfoModel, NotFound>>;

public class GetAccountDataHandler : IRequestHandler<GetAccountDataQuery, OneOf<AccountInfoModel, NotFound>> {

    private readonly IAccountsHandler _accountHandler;

    public GetAccountDataHandler(IAccountsHandler accountHandler) {
        _accountHandler = accountHandler;
    }

    public async Task<OneOf<AccountInfoModel, NotFound>> Handle(GetAccountDataQuery request, CancellationToken cancellationToken) {
        var user = await _accountHandler.GetAccountById(request.AccountId);
        return user.MapT0(account => new AccountInfoModel() { Username = account.UserName, Email = account.Email });
    }
}