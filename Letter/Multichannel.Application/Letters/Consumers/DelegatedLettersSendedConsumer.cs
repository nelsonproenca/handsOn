using System;
using System.Threading.Tasks;
using MassTransit;
using Multichannel.Application.Letters.Commands;
using Multichannel.Core.Contracts;

namespace Multichannel.Application.Letters.Consumers
{
    /// <summary>
    /// DelegatedLettersSended Consumer class.
    /// </summary>
    public class DelegatedLettersSendedConsumer : IConsumer<SentLetters>
    {
        private readonly ILettersCommands lettersCommands;

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegatedLettersSendedConsumer"/> class.
        /// </summary>
        /// <param name="lettersCommands">lettersCommands.</param>
        public DelegatedLettersSendedConsumer(ILettersCommands lettersCommands)
        {
            this.lettersCommands = lettersCommands;
        }

        /// <summary>
        /// Consume.
        /// </summary>
        /// <param name="context">context.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task Consume(ConsumeContext<SentLetters> context)
        {
            if (context.Message.LetterID > 0)
            {
                var letter = new Models.LetterModel()
                {
                    LetterID = context.Message.LetterID,
                    RequestID = context.Message.RequestID,
                    TenantIdentifier = context.Message.TenantIdentifier
                };

                await lettersCommands.SendLetter(letter);
            } 
        }
    }
}
