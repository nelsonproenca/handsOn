using System.Threading.Tasks;
using Multichannel.Application.Letters.Models;

namespace Multichannel.Application.Letters.Commands
{
    /// <summary>
    /// Interface LettersCommands.
    /// </summary>
    public interface ILettersCommands
    {
        /// <summary>
        /// Send a letter.
        /// </summary>
        /// <param name="letterModel">letter Model object.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<bool> SendLetter(LetterModel letterModel);
    }
}
