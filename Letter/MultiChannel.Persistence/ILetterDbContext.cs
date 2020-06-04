using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Multichannel.Domain.Entities;

namespace MultiChannel.Persistence
{
    /// <summary>
    /// Interface Letter DbContext.
    /// </summary>
    public interface ILetterDbContext
    {
        /// <summary>
        /// Gets access to the database infrastructure layer.
        /// </summary>
        DatabaseFacade Database { get; }

        /// <summary>
        /// Gets or sets letters.
        /// </summary>
        DbSet<Letter> Letters { get; set; }

        /// <summary>
        /// Gets or sets templates.
        /// </summary>
        DbSet<Template> Templates { get; set; }

        /// <summary>
        /// Gets or sets receivers.
        /// </summary>
        DbSet<Receiver> Receivers { get; set; }

        /// <summary>
        /// Save all changes.
        /// </summary>
        /// <returns>The number of itens affecteds.</returns>
        int SaveChanges();

        /// <summary>
        /// Save all changes Async.
        /// </summary>
        /// <param name="cancellationToken">Waiting for the task to complete.</param>
        /// <returns>The number of itens affecteds for async task.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
