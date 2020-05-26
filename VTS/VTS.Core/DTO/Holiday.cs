using System;

namespace VTS.Core.DTO
{
    /// <summary>
    /// Holiday DTO.
    /// </summary>
    public class Holiday
    {
        /// <summary>
        /// Gets or sets Holiday identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets User identifier.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets User.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets hours.
        /// </summary>
        public uint Hours { get; set; }

        /// <summary>
        /// Gets or sets Holiday starts.
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// Gets or sets Holiday expiration.
        /// </summary>
        public DateTime Expires { get; set; }

        /// <summary>
        /// Gets or sets description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets submission time.
        /// </summary>
        public DateTime SubmissionTime { get; set; }

        /// <summary>
        /// Gets or sets HolidayAcception.
        /// </summary>
        public HolidayAcception HolidayAcception { get; set; }
    }
}