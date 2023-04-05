using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using SuperSold.Data.DBInteractions;
using SuperSold.Data.Models;

namespace SuperSold.Data.EfCoreDB;
public class EfCoreSavedRelationshipsHandler : ISavedRelationshipsHandler {
    //todo - reduce duplication in this class
    private readonly EfCoreDBContext _context;
    public EfCoreSavedRelationshipsHandler(EfCoreDBContext context) {
        _context = context;
    }

    public async Task<OneOf<Success, Error>> AddRelationship(SavedRelationshipModel relationship) {

        var option = await GetRelationship(relationship.IdUser, relationship.IdProduct);

        if(option.TryPickT0(out var stored, out var _)) {

            stored.Quantity++;
            stored.RelationshipType = relationship.RelationshipType;
            var res = await _context.SaveChangesAsync();
            return res == 1 ? new Success() : new Error();
        }

        await _context.SavedRelationships.AddAsync(relationship);
        var result = await _context.SaveChangesAsync();
        return result == 1 ? new Success() : new Error();

    }

    public async Task<OneOf<SavedRelationshipModel, NotFound>> GetRelationship(Guid relationshipId) {
        var result = await _context.SavedRelationships.FindAsync(relationshipId);
        if(result is null) {
            return new NotFound();
        }

        return result;
    }

    public async Task<OneOf<SavedRelationshipModel, NotFound>> GetRelationship(Guid userId, Guid productId) {
        var result = await _context.SavedRelationships.FirstOrDefaultAsync(x => x.IdUser == userId && x.IdProduct == productId);
        if(result is null) {
            return new NotFound();
        }

        return result;
    }

    public IQueryable<SavedRelationshipModel> QuerySavedRelationshipsByUserId(Guid userId) {
        return _context.SavedRelationships.AsNoTracking().Where(x => x.IdUser == userId);
    }

    public async Task<OneOf<Success, NotFound>> RemoveRelationship(Guid relationshipId) {

        var result = await GetRelationship(relationshipId);

        if(result.TryPickT0(out var relationship, out var _)) {
            _context.SavedRelationships.Remove(relationship);
            await _context.SaveChangesAsync();
            return new Success();
        }

        return new NotFound();

    }

    public async Task<OneOf<Success, NotFound>> RemoveRelationship(Guid userId, Guid productId) {

        var result = await GetRelationship(userId, productId);

        if(result.TryPickT0(out var relationship, out var _)) {
            _context.SavedRelationships.Remove(relationship);
            await _context.SaveChangesAsync();
            return new Success();
        }

        return new NotFound();

    }

    public async Task<OneOf<Success, NotFound>> UpdateRelationshipQuantity(Guid relationshipId, int quantity) {

        if(quantity == 0) {
            return await RemoveRelationship(relationshipId);
        }

        var result = await GetRelationship(relationshipId);
        if(result.TryPickT1(out var notfound, out var relationship)) {
            return notfound;
        }

        relationship.Quantity = quantity;
        await _context.SaveChangesAsync();
        return new Success();
    
    }

    public async Task<OneOf<Success, NotFound>> UpdateRelationshipQuantity(Guid userId, Guid productId, int quantity) {

        if(quantity == 0) {
            return await RemoveRelationship(userId, productId);
        }

        var result = await GetRelationship(userId, productId);
        if(result.TryPickT1(out var notfound, out var relationship)) {
            return notfound;
        }

        relationship.Quantity = quantity;
        await _context.SaveChangesAsync();
        return new Success();

    }

    public async Task<OneOf<Success, NotFound>> UpdateRelationshipType(Guid relationshipId, RelationshipType type) {

        var result = await GetRelationship(relationshipId);
        if(result.TryPickT1(out var notfound, out var relationship)) {
            return notfound;
        }

        relationship.RelationshipType = type;
        await _context.SaveChangesAsync();
        return new Success();

    }

    public async Task<OneOf<Success, NotFound>> UpdateRelationshipType(Guid userId, Guid productId, RelationshipType type) {
        
        var result = await GetRelationship(userId, productId);
        if(result.TryPickT1(out var notfound, out var relationship)) {
            return notfound;
        }

        relationship.RelationshipType = type;
        await _context.SaveChangesAsync();
        return new Success();

    }

}
