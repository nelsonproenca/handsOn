using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Multichannel.Application.Letters.Commands;
using Multichannel.Application.Letters.Mappings;
using Multichannel.Application.Letters.Models;
using Multichannel.Application.Letters.Queries;
using Multichannel.Application.Templates.Mappings;
using Multichannel.Core.Commands;
using MultiChannel.Persistence;

namespace Multichannel.Ioc
{
    public class DependencyInjectionIoc
    {
        public DependencyInjectionIoc()
        {
        }

        public static void ServiceIoc(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<ICommonCommands<LetterModel>, LettersCommands>();

            services.AddScoped<ICommonQueries<LetterModel>, LettersQueries>();

            services.AddDbContext<ILetterDbContext, LetterDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("LetterConnectionString")), ServiceLifetime.Scoped);

            services.AddAutoMapper(new[]
            {
                typeof(LettersMappings),
                typeof(TemplatesMappings),
            });
        }
    }
}
