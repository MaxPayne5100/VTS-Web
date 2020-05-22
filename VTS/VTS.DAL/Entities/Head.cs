using System.Collections.Generic;
using VTS.Core.Infrastructure;

namespace VTS.DAL.Entities
{
    /// <summary>
    /// Head Entity.
    /// </summary>
    public class Head : IIdentifiable<int>
    {
        /// <summary>
        /// Gets or sets Head identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets User identifier.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets User.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets Clerk.
        /// </summary>
        public Clerk Clerk { get; set; }

        /// <summary>
        /// Gets or sets Manager.
        /// </summary>
        public Manager Manager { get; set; }

        /// <summary>
        /// Gets or sets collection of HolidayAcception.
        /// </summary>
        public ICollection<HolidayAcception> HolidayAcception { get; set; }
    }
}