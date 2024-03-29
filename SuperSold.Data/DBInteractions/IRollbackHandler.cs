﻿using OneOf;
using OneOf.Types;
using SuperSold.Data.Models;
using SuperSold.Data.Models.ResponseTypes;

namespace SuperSold.Data.DBInteractions;
public interface IRollbackHandler {

    Task<OneOf<Success, AlreadyExists>> CreateRollback(RollbackModel rollback);
    Task<OneOf<RollbackModel, NotFound>> GetRollback(Guid rollbackId);
    Task<OneOf<RollbackModel, Unauthorized, NotFound>> GetRollback(Guid rollbackId, Guid userId, RollbackType type);
    Task<OneOf<Success, NotFound>> ExpireRollback(Guid rollbackId);
    IQueryable<RollbackModel> GetAll();
    IQueryable<RollbackModel> GetAllOlderThan(DateTime time);
    IQueryable<RollbackModel> GetAllByUserId(Guid userId);

}

public record struct Unauthorized();