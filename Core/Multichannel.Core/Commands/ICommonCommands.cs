using System.Threading.Tasks;

namespace Multichannel.Core.Commands
{
    /// <summary>
    /// Interface Common Commands.
    /// </summary>
    /// <typeparam name="T">As a class.</typeparam>
    public interface ICommonCommands<T> where T : class
    {
        /// <summary>
        /// Create async command.
        /// </summary>
        /// <param name="entity">new entity data.</param>
        /// <returns>identifier guid.</returns>
        Task<int> CreateAsync(T entity);

        /// <summary>
        /// Delete async command.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>number of rows affected.</returns>
        Task<int> DeleteAsync(int id);

        /// <summary>
        /// Update async command.
        /// </summary>
        /// <param name="id">id.</param>
        /// <param name="entity">new entity data.</param>
        /// <returns>number of rows affected.</returns>
        Task<int> UpdateAsync(int id, T entity);
    }
}
