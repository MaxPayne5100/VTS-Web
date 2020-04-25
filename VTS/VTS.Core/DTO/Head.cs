namespace VTS.Core.DTO
{
    /// <summary>
    /// Head DTO.
    /// </summary>
    public class Head
    {
        /// <summary>
        /// Gets or sets Head identifier.
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// Gets or sets User identifier.
        /// </summary>
        public uint UserId { get; set; }

        /// <summary>
        /// Gets or sets User.
        /// </summary>
        public User User { get; set; }
    }
}
