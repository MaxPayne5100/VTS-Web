using VTS.Core.Infrastructure;

namespace VTS.DAL.Entities
{
    /// <summary>
    /// Clerk Entity.
    /// </summary>
    public class Clerk : IIdentifiable<uint>
    {
        /// <summary>
        /// Gets or sets Clerk identifier.
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// Gets or sets Head identifier.
        /// </summary>
        public uint HeadId { get; set; }

        /// <summary>
        /// Gets or sets Head.
        /// </summary>
        public Head Head { get; set; }
    }
}