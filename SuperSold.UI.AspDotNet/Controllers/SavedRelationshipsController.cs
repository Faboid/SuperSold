using Microsoft.AspNetCore.Mvc;
using SuperSold.Data.DBInteractions;

namespace SuperSold.UI.AspDotNet.Controllers;
public class SavedRelationshipsController : Controller {

    private readonly ISavedRelationshipsHandler _savedRelationshipsHandler;

    public SavedRelationshipsController(ISavedRelationshipsHandler savedRelationshipsHandler) {
        _savedRelationshipsHandler = savedRelationshipsHandler;
    }

    [HttpPost]
    public async Task<IActionResult> ModifyQuantity(Guid relationshipId, int quantity) {

        var result = await _savedRelationshipsHandler.UpdateRelationshipQuantity(relationshipId, quantity);
        return result.Match<IActionResult>(
            success => Ok(),
            notfound => NotFound()
        );

    } 

}
