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
