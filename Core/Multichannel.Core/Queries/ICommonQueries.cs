using System;
using System.Threading.Tasks;
using Multichannel.Core.Paging;

namespace Multichannel.Core.Commands
{
    /// <summary>
    /// Interface Common Queries.
    /// </summary>
    /// <typeparam name="T">As a class.</typeparam>
    public interface ICommonQueries<T> where T : class
    {
        /// <summary>
        /// Get one item details.
        /// </summary>
        /// <param name="id">identifier.</param>
        /// <returns>a object model.</returns>
        Task<T> GetOneAsync(int id);

        /// <summary>
        /// Get all by Tenant.
        /// </summary>
        /// <param name="tenantId">tenant id.</param>
        /// <param name="pageNumber">page number.</param>
        /// <param name="pageSize">page size.</param>
        /// <returns>a list of itens.</returns>
        Task<PaginatedList<T>> GetByTenantAsync(Guid tenantId, int pageNumber = 1, int pageSize = 10);

        /// <summary>
        /// Get By request Id.
        /// </summary>
        /// <param name="requestId">requestId.</param>
        /// <returns>object T.</returns>
        Task<T> GetByRequestAsync(Guid requestId);
    }
}
