using System;
using VTS.Core.Infrastructure;

namespace VTS.DAL.Entities
{
    /// <summary>
    /// Holiday Entity.
    /// </summary>
    public class Holiday : IIdentifiable<Guid>
    {
        /// <summary>
        /// Type of Holiday category.
        /// </summary>
        public enum Categories
        {
            /// <summary>
            /// PaidDayOffs category
            /// </summary>
            PaidDayOffs,

            /// <summary>
            /// UnPaidDayOffs category
            /// </summary>
            UnPaidDayOffs,

            /// <summary>
            /// PaidSickness category
            /// </summary>
            PaidSickness,

            /// <summary>
            /// UnPaidSickness category
            /// </summary>
            UnPaidSickness,
        }

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
        public Categories Category { get; set; }

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

        /// <summary>
        /// Initializes a new instance of the <see cref="Holiday"/> class.
        /// </summary>
        public Holiday()
        {
            this.SubmissionTime = DateTime.Now;
        }
    }
}