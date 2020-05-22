using VTS.Core.Infrastructure;

namespace VTS.DAL.Entities
{
    /// <summary>
    /// Clerk Entity.
    /// </summary>
    public class Clerk : IIdentifiable<int>
    {
        /// <summary>
        /// Gets or sets Clerk identifier.
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
    }
}