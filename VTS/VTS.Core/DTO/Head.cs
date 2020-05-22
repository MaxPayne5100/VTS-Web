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
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets User identifier.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets User.
        /// </summary>
        public User User { get; set; }
    }
}
