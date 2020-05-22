using System.Collections.Generic;
using VTS.Core.Infrastructure;

namespace VTS.DAL.Entities
{
    /// <summary>
    /// User Entity.
    /// </summary>
    public class User : IIdentifiable<int>
    {
        /// <summary>
        /// Gets or sets User identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets User firstname.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets User lastname.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets User email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets User role.
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets Employee.
        /// </summary>
        public Employee Employee { get; set; }

        /// <summary>
        /// Gets or sets Head.
        /// </summary>
        public Head Head { get; set; }

        /// <summary>
        /// Gets or sets UserVacationInfo.
        /// </summary>
        public UserVacationInfo UserVacationInfo { get; set; }

        /// <summary>
        /// Gets or sets collection of Holidays.
        /// </summary>
        public ICollection<Holiday> Holidays { get; set; }
    }
}