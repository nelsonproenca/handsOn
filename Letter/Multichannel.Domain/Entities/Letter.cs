using System;
using System.Collections.Generic;
using Multichannel.Core.Entities;

namespace Multichannel.Domain.Entities
{
    /// <summary>
    /// Letter class.
    /// </summary>
    public class Letter : BaseEntity
    {
        /// <summary>
        /// Gets or sets templateID.
        /// </summary>
        public int TemplateID { get; set; }

        /// <summary>
        /// Gets or sets template.
        /// </summary>
        public Template Template { get; set; }

        /// <summary>
        /// Gets or sets receivers.
        /// </summary>
        public virtual List<Receiver> Receivers { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether sentStatus.
        /// </summary>
        public bool SentStatus { get; set; }

        /// <summary>
        /// Gets or sets sentDate.
        /// </summary>
        public DateTime SentDate { get; set; }

        /// <summary>
        /// Gets or sets requestID.
        /// </summary>
        public Guid RequestID { get; set; } = Guid.Empty;
    }
}
