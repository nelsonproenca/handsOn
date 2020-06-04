using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Multichannel.Application.Letters.Models;
using Multichannel.Application.Letters.Validators;
using Multichannel.Core.Base;
using Multichannel.Core.Commands;
using Multichannel.Domain.Entities;
using MultiChannel.Persistence;

namespace Multichannel.Application.Letters.Commands
{
    /// <summary>
    /// Letters Commands class. 
    /// </summary>
    public class LettersCommands : CommandBase, ICommonCommands<LetterModel>, ILettersCommands
    {
        private readonly ILetterDbContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="LettersCommands"/> class.
        /// </summary>
        /// <param name="context">Letter DbContext.</param>
        /// <param name="logger">logger.</param>
        /// <param name="mapper">mapper.</param>
        public LettersCommands(ILetterDbContext context, ILogger<CommandBase> logger, IMapper mapper) : base(logger, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<int> CreateAsync(LetterModel model)
        {
            // Validate entity
            ValidateCommand(new LettersValidator(), model);

            // mapper domain class
            var newLetter = mapper.Map<Letter>(model);

            // add to Letter table
            context.Letters.Add(newLetter);

            // save context
            await context.SaveChangesAsync();

            // return letter id
            return newLetter.Id;
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(int id)
        {
            var deleteLetter = context.Letters?.SingleOrDefault(let => let.Id == id);

            if (deleteLetter == null)
            {
                return -1;
            }

            deleteLetter.IsDeleted = true;

            var result = await context.SaveChangesAsync();

            return result;
        }

        /// <inheritdoc/>
        public Task<bool> SendLetter(LetterModel letterModel)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<int> UpdateAsync(int id, LetterModel entity)
        {
            var originalLetter = context.Letters?.SingleOrDefault(let => let.Id == id);

            if (originalLetter == null)
            {
                return -1;
            }

            ValidateCommand(new LettersValidator(), entity);

            mapper.Map(entity, originalLetter);

            var result = await context.SaveChangesAsync();

            return result;
        }
    }
}
