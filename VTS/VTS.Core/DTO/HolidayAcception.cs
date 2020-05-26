using System;

namespace VTS.Core.DTO
{
    /// <summary>
    /// HolidayAcception DTO.
    /// </summary>
    public class HolidayAcception
    {
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
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets description.
        /// </summary>
        public string Description { get; set; }
    }
}
