using Multichannel.Core.Entities;

namespace Multichannel.Domain.Entities
{
    /// <summary>
    /// Template class.
    /// </summary>
    public class Template : BaseEntity
    {
        /// <summary>
        /// Gets or sets path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets description.
        /// </summary>
        public string Description { get; set; }
    }
}
