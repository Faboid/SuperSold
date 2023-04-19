using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using SuperSold.Data.DBInteractions;
using SuperSold.Data.Models;
using SuperSold.Data.Models.ResponseTypes;

namespace SuperSold.Data.EfCoreDB;
public class EfCoreRollbackHandler : IRollbackHandler {

    private readonly EfCoreDBContext _context;

    public EfCoreRollbackHandler(EfCoreDBContext context) {
        _context = context;
    }

    public async Task<OneOf<Success, AlreadyExists>> CreateRollback(RollbackModel rollback) {

        await _context.Rollbacks.AddAsync(rollback);
        var result = await _context.SaveChangesAsync();

        if(result == 1) {
            return new Success();
        } else {
            return new AlreadyExists();
        }

    }

    public async Task<OneOf<Success, NotFound>> ExpireRollback(Guid rollbackId) {

        var rollback = await _context.Rollbacks.FindAsync(rollbackId);
        if(rollback is null) {
            return new NotFound();
        }

        _context.Rollbacks.Remove(rollback);
        await _context.SaveChangesAsync();
        return new Success();

    }

    public async Task<OneOf<RollbackModel, NotFound>> GetRollback(Guid rollbackId) {

        var rollback = await _context.Rollbacks.FindAsync(rollbackId);
        if(rollback is null) {
            return new NotFound();
        }

        return rollback;

    }

    public async Task<OneOf<RollbackModel, Unauthorized, NotFound>> GetRollback(Guid rollbackId, Guid userId, RollbackType type) {

        var rollback = await _context.Rollbacks.FindAsync(rollbackId);
        if(rollback is null) {
            return new NotFound();
        }

        if(rollback.IdAccount != userId || rollback.RollbackType != type) {
            return new Unauthorized();
        }

        return rollback;

    }

    public IQueryable<RollbackModel> GetAll() {
        return _context.Rollbacks;
    }

    public IQueryable<RollbackModel> GetAllByUserId(Guid userId) {
        return _context.Rollbacks.Where(x => x.IdAccount == userId);
    }

    public IQueryable<RollbackModel> GetAllOlderThan(DateOnly time) => throw new NotImplementedException();
}
