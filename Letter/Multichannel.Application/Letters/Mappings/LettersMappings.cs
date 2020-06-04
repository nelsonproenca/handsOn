using AutoMapper;
using Multichannel.Application.Letters.Models;
using Multichannel.Domain.Entities;

namespace Multichannel.Application.Letters.Mappings
{
    /// <summary>
    /// Letters Mappings class.
    /// </summary>
    public class LettersMappings : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LettersMappings"/> class.
        /// </summary>
        public LettersMappings()
        {
            CreateMap<Letter, LetterModel>()
                .ForMember(dest => dest.LetterID, opt => opt.MapFrom(o => o.Id))
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Receiver, ReceiverModel>().ReverseMap();
        }
    }
}
