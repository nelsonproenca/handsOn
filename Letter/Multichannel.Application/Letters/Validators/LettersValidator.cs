using FluentValidation;
using Multichannel.Application.Letters.Models;

namespace Multichannel.Application.Letters.Validators
{
    /// <summary>
    /// Letters Validator class.
    /// </summary>
    public class LettersValidator : AbstractValidator<LetterModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LettersValidator"/> class.
        /// </summary>
        public LettersValidator()
        {
            RuleFor(letter => letter.TemplateId).NotNull().GreaterThan(0);
            RuleFor(letter => letter.RequestID).NotNull();
            RuleFor(letter => letter.TenantIdentifier).NotNull();
            RuleFor(letter => letter.Receivers).NotNull();
        }
    }
}
