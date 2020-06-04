using System;
using System.Collections.Generic;
using System.Text;

namespace Multichannel.Core.Entities
{
    /// <summary>
    /// BaseEntity class.
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Gets or sets letterID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether isDeleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets consumer identifier.
        /// </summary>
        public Guid TenantIdentifier { get; set; }
    }
}
