using System.Collections.Generic;

namespace VTS.Web.Models
{
    /// <summary>
    /// Manager options model.
    /// </summary>
    public class ManagerOptionsModel
    {
        /// <summary>
        /// Gets or sets manager id.
        /// </summary>
        public string ManagerId { get; set; }

        /// <summary>
        /// Gets or sets groups.
        /// </summary>
        public IEnumerable<Core.DTO.Manager> Managers { get; set; }
    }
}