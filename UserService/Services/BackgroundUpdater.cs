using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace UserService.Services
{
    public class BackgroundUpdater : BackgroundService
    {
        private readonly IUserCacheUpdate userCacheUpdate;

        public BackgroundUpdater(IUserCacheUpdate userCacheUpdate)
        {
            this.userCacheUpdate = userCacheUpdate ?? throw new ArgumentNullException(nameof(userCacheUpdate));
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await userCacheUpdate.UpdateCacheAsync();
                await Task.Delay(TimeSpan.FromMinutes(1));
            }
        }
    }
}
