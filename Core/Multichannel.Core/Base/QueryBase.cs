using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Multichannel.Core.Base
{
    /// <summary>
    /// Base Queries class.
    /// </summary>
    public class QueryBase
    {
        /// <summary>
        /// Gets current logger.
        /// </summary>
        protected ILogger<QueryBase> Logger { get; }

        /// <summary>
        /// Gets current automapper instance.
        /// </summary>
        protected IMapper Mapper { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryBase"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper.</param>
        public QueryBase(ILogger<QueryBase> logger, IMapper mapper)
        {
            this.Logger = logger;
            this.Mapper = mapper;
        }
    }
}
