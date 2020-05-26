using System;
using System.ComponentModel.DataAnnotations;

namespace VTS.Web.Models
{
    /// <summary>
    /// Booking model.
    /// </summary>
    public class BookingModel
    {
        /// <summary>
        /// Gets or sets Holiday starts.
        /// </summary>
        [Required(ErrorMessage = "Початок відпуску не був вказаний")]
        public DateTime Start { get; set; }

        /// <summary>
        /// Gets or sets Holiday expiration.
        /// </summary>
        [Required(ErrorMessage = "Закінчення відпуску не було вказано")]
        public DateTime Expires { get; set; }

        /// <summary>
        /// Gets or sets category.
        /// </summary>
        [Required(ErrorMessage = "Категорію відпуску не було вказано")]
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets description.
        /// </summary>
        [Required(ErrorMessage = "Опис відпуску не був вказаний")]
        public string Description { get; set; }
    }
}
