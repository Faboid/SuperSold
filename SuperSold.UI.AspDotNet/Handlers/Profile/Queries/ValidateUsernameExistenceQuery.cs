using SuperSold.Data.DBInteractions;

namespace SuperSold.UI.AspDotNet.Handlers.Profile.Queries;


public record ValidateUsernameExistenceQuery(string Username) : IRequest<bool>;

public class ValidateUsernameExistenceHandler : IRequestHandler<ValidateUsernameExistenceQuery, bool> {

    private readonly IAccountsHandler _accountsHandler;

    public ValidateUsernameExistenceHandler(IAccountsHandler accountsHandler) {
        _accountsHandler = accountsHandler;
    }

    public async Task<bool> Handle(ValidateUsernameExistenceQuery request, CancellationToken cancellationToken) {
        return await _accountsHandler.UserNameExists(request.Username);
    }
}
