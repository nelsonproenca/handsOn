using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Multichannel.Application.Letters.Models;
using Multichannel.Core.Base;
using Multichannel.Core.Commands;
using Multichannel.Core.Paging;
using Multichannel.Domain.Entities;
using MultiChannel.Persistence;

namespace Multichannel.Application.Letters.Queries
{
    /// <summary>
    /// Class1.
    /// </summary>
    public class LettersQueries : QueryBase, ICommonQueries<LetterModel>
    {
        private readonly ILetterDbContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="LettersQueries"/> class.
        /// </summary>
        /// <param name="context">Letter dbcontext.</param>
        /// <param name="logger">logger.</param>
        /// <param name="mapper">mapper.</param>
        public LettersQueries(ILetterDbContext context, ILogger<QueryBase> logger, IMapper mapper) : base(logger, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        /// <inheritdoc/>
        public async Task<LetterModel> GetByRequestAsync(Guid requestId)
        {
            var letter = await context.Letters?.SingleOrDefaultAsync(let => let.RequestID == requestId);

            // if requestId not exists
            if (letter == null)
            {
                return null;
            }

            var result = mapper.Map<LetterModel>(letter);

            return result;
        }

        /// <inheritdoc/>
        public async Task<PaginatedList<LetterModel>> GetByTenantAsync(Guid tenantId, int pageNumber = 1, int pageSize = 10)
        {
            IQueryable<Letter> model = context.Letters.AsQueryable();

            model = model.Where(mail => mail.TenantIdentifier.Equals(tenantId));

            var results = model.ProjectTo<LetterModel>(mapper.ConfigurationProvider).AsNoTracking();

            var list = await PaginatedList<LetterModel>.CreateAsync(results, pageNumber, pageSize);

            return list;
        }

        /// <inheritdoc/>
        public async Task<LetterModel> GetOneAsync(int id)
        {
            var letter = await context.Letters?.SingleOrDefaultAsync(let => let.Id == id);

            // if id not exists
            if (letter == null)
            {
                return null;
            }

            var result = mapper.Map<LetterModel>(letter);

            return result;
        }
    }
}
