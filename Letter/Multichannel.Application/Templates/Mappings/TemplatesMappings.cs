using AutoMapper;
using Multichannel.Application.Templates.Models;
using Multichannel.Domain.Entities;

namespace Multichannel.Application.Templates.Mappings
{
    /// <summary>
    /// Templates Mappings class.
    /// </summary>
    public class TemplatesMappings : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TemplatesMappings"/> class.
        /// </summary>
        public TemplatesMappings()
        {
            CreateMap<Template, TemplateModel>().ReverseMap();
        }
    }
}
