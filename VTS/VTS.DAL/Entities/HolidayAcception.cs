using System;
using VTS.Core.Infrastructure;

namespace VTS.DAL.Entities
{
    /// <summary>
    /// HolidayAcception Entity.
    /// </summary>
    public class HolidayAcception : IIdentifiable<Guid>
    {
        /// <summary>
        /// Type of HolidayAcception status.
        /// </summary>
        public enum StatusType
        {
            /// <summary>
            /// Pending status
            /// </summary>
            Pending,

            /// <summary>
            /// Approved status
            /// </summary>
            Approved,

            /// <summary>
            /// Canceled status
            /// </summary>
            Canceled,
        }

        /// <summary>
        /// Gets or sets HolidayAcception identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets Head identifier.
        /// </summary>
        public int HeadId { get; set; }

        /// <summary>
        /// Gets or sets Head.
        /// </summary>
        public Head Head { get; set; }

        /// <summary>
        /// Gets or sets Holiday identifier.
        /// </summary>
        public Guid HolidayId { get; set; }

        /// <summary>
        /// Gets or sets Holiday.
        /// </summary>
        public Holiday Holiday { get; set; }

        /// <summary>
        /// Gets or sets status.
        /// </summary>
        public StatusType Status { get; set; }

        /// <summary>
        /// Gets or sets description.
        /// </summary>
        public string Description { get; set; }
    }
}