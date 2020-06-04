using FluentValidation;
using Multichannel.Application.Templates.Models;

namespace Multichannel.Application.Templates.Validators
{
    /// <summary>
    /// Templates Validator class.
    /// </summary>
    public class TemplatesValidator : AbstractValidator<TemplateModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TemplatesValidator"/> class.
        /// </summary>
        public TemplatesValidator()
        {
            RuleFor(temp => temp.Path).NotNull().MaximumLength(2048).MinimumLength(2);
            RuleFor(temp => temp.Description).NotNull().MaximumLength(512).MinimumLength(2);
        }
    }
}
