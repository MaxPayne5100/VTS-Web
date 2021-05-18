using System;
using System.ComponentModel.DataAnnotations;

namespace VTS.Web.Models
{
    /// <summary>
    /// Approve Booking model.
    /// </summary>
    public class ApproveBookingModel
    {
        /// <summary>
        /// Gets or sets user id [to be notified].
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets holiday id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets description.
        /// </summary>
        [Required(ErrorMessage = "Опис причини не був вказаний")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets state.
        /// </summary>
        [Required(ErrorMessage = "Статус не було вказано")]
        public string State { get; set; }
    }
}
