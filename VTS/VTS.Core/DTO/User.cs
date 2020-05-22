namespace VTS.Core.DTO
{
    /// <summary>
    /// User DTO.
    /// </summary>
    public class User
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
        /// Gets or sets Employee Manager.
        /// </summary>
        public int ManagerId { get; set; }
    }
}
