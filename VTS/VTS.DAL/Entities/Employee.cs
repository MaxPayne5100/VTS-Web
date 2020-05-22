using VTS.Core.Infrastructure;

namespace VTS.DAL.Entities
{
    /// <summary>
    /// Employee Entity.
    /// </summary>
    public class Employee : IIdentifiable<int>
    {
        /// <summary>
        /// Gets or sets Employee identifier.
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
        /// Gets or sets Manager identifier.
        /// </summary>
        public int ManagerId { get; set; }

        /// <summary>
        /// Gets or sets Manager.
        /// </summary>
        public Manager Manager { get; set; }
    }
}