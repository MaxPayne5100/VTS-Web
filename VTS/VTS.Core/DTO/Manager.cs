namespace VTS.Core.DTO
{
    /// <summary>
    /// Manager DTO.
    /// </summary>
    public class Manager
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
    }
}
