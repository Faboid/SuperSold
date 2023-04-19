using Microsoft.EntityFrameworkCore;
using SuperSold.Data.DBInteractions;

namespace SuperSold.Rollbacks.Cleanup;
public class ExpiredRollbacksCleaner : IAsyncDisposable {

    private readonly IRollbackHandler _rollbackHandler;
    private readonly TimeSpan _period;
    private readonly PeriodicTimer _timer;
    private readonly CancellationTokenSource _cts = new();
    private readonly Task _cleanupLoop;

    public ExpiredRollbacksCleaner(IRollbackHandler rollbackHandler, TimeSpan cleaningIntervalTime) {
        _rollbackHandler = rollbackHandler;
        _period = cleaningIntervalTime;
        _timer = new(_period);
        _cleanupLoop = Repeat();
    }

    private async Task Repeat() {
        while(await _timer.WaitForNextTickAsync(_cts.Token) && !_cts.Token.IsCancellationRequested) {
            await CleanExpired();
        }
    }

    private async Task CleanExpired() {
        await _rollbackHandler
            .GetAllOlderThan(DateOnly.FromDateTime(DateTime.Now))
            .ForEachAsync(x => _rollbackHandler.ExpireRollback(x.IdRollback));
    }

    public async ValueTask DisposeAsync() {
        _cts.Cancel();
        _timer.Dispose();
        await _cleanupLoop;
        GC.SuppressFinalize(this);
    }
}
