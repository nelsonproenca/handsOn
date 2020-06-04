using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace MultiChannel.WebApi
{
    /// <summary>
    /// MassTransitConsoleHostedService class.
    /// </summary>
    public class MassTransitConsoleHostedService : IHostedService
    {
        readonly IBusControl bus;

        /// <summary>
        /// Initializes a new instance of the <see cref="MassTransitConsoleHostedService"/> class.
        /// </summary>
        /// <param name="bus">Bus.</param>
        public MassTransitConsoleHostedService(IBusControl bus)
        {
            this.bus = bus;
        }

        /// <inheritdoc/>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await bus.StartAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return bus.StopAsync(cancellationToken);
        }
    }
}
