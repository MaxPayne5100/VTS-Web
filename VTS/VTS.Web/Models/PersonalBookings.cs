using System;
using System.Collections.Generic;

namespace VTS.Web.Models
{
    /// <summary>
    /// Model for personal bookings.
    /// </summary>
    public class PersonalBookings
    {
        /// <summary>
        /// Gets or sets date after which bookings should be found.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets category in which bookings should be found.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets status on which bookings should be found.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets list of bookings.
        /// </summary>
        public IEnumerable<Core.DTO.Holiday> Bookings { get; set; }
    }
}
