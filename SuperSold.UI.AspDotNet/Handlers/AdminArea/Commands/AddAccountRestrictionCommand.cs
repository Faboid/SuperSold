using SuperSold.Data.DBInteractions;

namespace SuperSold.UI.AspDotNet.Handlers.AdminArea.Commands;


public record AddAccountRestrictionCommand(Guid AccountId, string Restriction) : IRequest<OneOf<Success, NotFound>>;

public class AddAccountRestrictionHandler : IRequestHandler<AddAccountRestrictionCommand, OneOf<Success, NotFound>> {

    private readonly IAccountRestrictionsHandler _restrictionsHandler;

    public AddAccountRestrictionHandler(IAccountRestrictionsHandler restrictionsHandler) {
        _restrictionsHandler = restrictionsHandler;
    }

    public async Task<OneOf<Success, NotFound>> Handle(AddAccountRestrictionCommand request, CancellationToken cancellationToken) {
        return await _restrictionsHandler.AddRestriction(request.AccountId, request.Restriction);
    }
}
