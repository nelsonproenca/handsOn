using System;
using System.Collections.Generic;
using System.Text;

namespace Multichannel.Core.Contracts
{
    /// <summary>
    /// SentLetters class.
    /// </summary>
    public class SentLetters
    {
        /// <summary>
        /// Gets or sets letterID.
        /// </summary>
        public int LetterID { get; set; }

        /// <summary>
        /// Gets or sets tempateId.
        /// </summary>
        public int TemplateID { get; set; }

        /// <summary>
        /// Gets or sets receivers.
        /// </summary>
        public string[] Receivers { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether sentStatus.
        /// </summary>
        public bool SentStatus { get; set; }

        /// <summary>
        /// Gets or sets sentDate.
        /// </summary>
        public DateTime SentDate { get; set; }

        /// <summary>
        /// Gets or sets consumer identifier.
        /// </summary>
        public Guid TenantIdentifier { get; set; }

        /// <summary>
        /// Gets or sets requestID.
        /// </summary>
        public Guid RequestID { get; set; } = Guid.Empty;
    }
}
