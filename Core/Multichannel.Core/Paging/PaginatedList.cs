using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Multichannel.Core.Paging
{
    /// <summary>
    /// Paginated List.
    /// </summary>
    /// <typeparam name="T">List of T.</typeparam>
    [JsonObject(MemberSerialization.OptIn)]
    public class PaginatedList<T> : List<T>
    {
        /// <summary>
        /// Gets page number (starting at 1).
        /// </summary>
        [JsonProperty]
        public int PageNumber { get; private set; }

        /// <summary>
        /// Gets total pages.
        /// </summary>
        [JsonProperty]
        public int PageCount { get; private set; }

        /// <summary>
        /// Gets page size.
        /// </summary>
        [JsonProperty]
        public int PageSize { get; private set; }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.List`1"></see>.
        /// </summary>
        [JsonProperty]
        public new int Count => base.Count;

        /// <summary>
        /// Gets the total number of items.
        /// </summary>
        /// <value>
        /// The total count.
        /// </value>
        [JsonProperty]
        public int TotalCount { get; private set; }

        /// <summary>
        /// Gets the items so that the PaginatedList is serialized with the other properties 
        /// and not just the IEnumerable. It will be serialized as a JsonObject.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        [JsonProperty]
        public IEnumerable<T> Items => this.ToList();

        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedList{T}"/> class.
        /// </summary>
        /// <param name="items">List of items to paginate.</param>
        /// <param name="totalCount">Count of items.</param>
        /// <param name="pageNumber">Page index.</param>
        /// <param name="pageSize">Page size.</param>
        public PaginatedList(IList<T> items, int totalCount, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageCount = (PageSize > 0 || totalCount > 0) ? ((int)Math.Ceiling(totalCount / (double)pageSize)) : 0;
            PageSize = pageSize;
            TotalCount = totalCount;

            this.AddRange(items);
        }

        /// <summary>
        /// Creates a paged list asynchronous.
        /// </summary>
        /// <param name="source">List of items.</param>
        /// <param name="pageNumber">Page index.</param>
        /// <param name="pageSize">Page size.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public static async Task<PaginatedList<T>> CreateAsync(
            IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip(
                (pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageNumber, pageSize);
        }

        /// <summary>
        /// Creates a paged list asynchronous.
        /// </summary>
        /// <param name="source">List of items.</param>
        /// <param name="pageNumber">Page index.</param>
        /// <param name="pageSize">Page size.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public static PaginatedList<T> Create(
            IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip(
                (pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
