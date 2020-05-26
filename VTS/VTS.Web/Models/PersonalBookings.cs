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
        /// Gets or sets date after which booking should be found.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets list of bookings.
        /// </summary>
        public IEnumerable<Core.DTO.Holiday> Bookings { get; set; }
    }
}
