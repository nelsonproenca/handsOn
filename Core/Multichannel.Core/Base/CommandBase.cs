using System;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace Multichannel.Core.Base
{
    /// <summary>
    /// Base Commands class.
    /// </summary>
    public class CommandBase
    {
        /// <summary>
        /// Gets current logger.
        /// </summary>
        protected ILogger<CommandBase> Logger { get; }

        /// <summary>
        /// Gets or sets current mapper.
        /// </summary>
        protected virtual IMapper Mapper { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandBase"/> class.
        /// </summary>
        public CommandBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandBase"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper.</param>
        public CommandBase(ILogger<CommandBase> logger, IMapper mapper)
        {
            this.Logger = logger;
            this.Mapper = mapper;
        }

        /// <summary>
        /// Generically validates an object.
        /// </summary>
        /// <param name="validator">The first name to join.</param>
        /// <param name="model">The last name to join.</param>
        /// <typeparam name="T">Class type who will be validated.</typeparam>
        protected void ValidateCommand<T>(AbstractValidator<T> validator, T model)
            where T : class
        {
            string errors = string.Empty;

            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var item in result.Errors)
                {
                    errors += $"{item.ErrorCode} - {item.ErrorMessage}";
                }

                throw new Exception(errors);
            }
        }
    }
}
