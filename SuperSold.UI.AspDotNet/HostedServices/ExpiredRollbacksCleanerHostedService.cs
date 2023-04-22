using Microsoft.EntityFrameworkCore;
using SuperSold.Data.DBInteractions;

namespace SuperSold.UI.AspDotNet.HostedServices;

public class ExpiredRollbacksCleanerHostedService : BackgroundService, IHostedService {

    private readonly IServiceProvider _serviceProvider;
    private readonly TimeSpan _period;
    private readonly PeriodicTimer _timer;

    public ExpiredRollbacksCleanerHostedService(IServiceProvider serviceProvider, TimeSpan cleaningIntervalTime) {
        _serviceProvider = serviceProvider;
        _period = cleaningIntervalTime;
        _timer = new(_period);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        while(await _timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested) {
            await CleanExpired();
        }
    }

    private async Task CleanExpired() {
        await using var scope = _serviceProvider.CreateAsyncScope();
        var rollbackHandler = scope.ServiceProvider.GetRequiredService<IRollbackHandler>();

        await rollbackHandler
            .GetAllOlderThan(DateTime.Now)
            .ForEachAsync(x => rollbackHandler.ExpireRollback(x.IdRollback));
    }

}