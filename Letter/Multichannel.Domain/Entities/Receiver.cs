using System;
using Multichannel.Core.Entities;

namespace Multichannel.Domain.Entities
{
    /// <summary>
    /// Receiver class.
    /// </summary>
    public class Receiver : BaseEntity
    {
        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets postalCode.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets numberContract.
        /// </summary>
        public string NumberContract { get; set; }

        /// <summary>
        /// Gets or sets debtValue.
        /// </summary>
        public decimal DebtValue { get; set; }

        /// <summary>
        /// Gets or sets dueDate.
        /// </summary>
        public DateTime DueDate { get; set; }
    }
}
