namespace VTS.Core.DTO
{
    /// <summary>
    /// Employee DTO.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Gets or sets Employee identifier.
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

        /// <summary>
        /// Gets or sets Manager identifier.
        /// </summary>
        public uint ManagerId { get; set; }

        /// <summary>
        /// Gets or sets Manager.
        /// </summary>
        public Manager Manager { get; set; }
    }
}
