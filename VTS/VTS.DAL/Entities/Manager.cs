using System.Collections.Generic;
using VTS.Core.Infrastructure;

namespace VTS.DAL.Entities
{
    /// <summary>
    /// Manager Entity.
    /// </summary>
    public class Manager : IIdentifiable<int>
    {
        /// <summary>
        /// Gets or sets Manager identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets Head identifier.
        /// </summary>
        public int HeadId { get; set; }

        /// <summary>
        /// Gets or sets Head.
        /// </summary>
        public Head Head { get; set; }

        /// <summary>
        /// Gets or sets collection of Subordinates.
        /// </summary>
        public ICollection<Employee> Subordinates { get; set; }
    }
}