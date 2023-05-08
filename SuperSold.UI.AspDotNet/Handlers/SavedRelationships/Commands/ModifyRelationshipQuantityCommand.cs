using OneOf.Types;
using SuperSold.Data.DBInteractions;

namespace SuperSold.UI.AspDotNet.Handlers.SavedRelationships.Commands;

public record ModifyRelationshipQuantityCommand(Guid RelationshipId, int NewQuantity) : IRequest<OneOf<Success, NotFound>>;

public class ModifyRelationshipQuantityHandler : IRequestHandler<ModifyRelationshipQuantityCommand, OneOf<Success, NotFound>> {

    private readonly ISavedRelationshipsHandler _savedRelationshipsHandler;

    public ModifyRelationshipQuantityHandler(ISavedRelationshipsHandler savedRelationshipsHandler) {
        _savedRelationshipsHandler = savedRelationshipsHandler;
    }

    public async Task<OneOf<Success, NotFound>> Handle(ModifyRelationshipQuantityCommand request, CancellationToken cancellationToken) {
        return await _savedRelationshipsHandler.UpdateRelationshipQuantity(request.RelationshipId, request.NewQuantity);
    }
}
