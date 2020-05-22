using System;
using VTS.Core.Attributes;

namespace VTS.Web.Models
{
    /// <summary>
    /// ChangeBonusDayOffs model.
    /// </summary>
    public class ChangeDayOffsModel
    {
        /// <summary>
        /// Gets or sets user id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets time when started working.
        /// </summary>
        public DateTime? StartedWorking { get; set; }

        /// <summary>
        /// Gets or sets paid day offs.
        /// </summary>
        [PosNumber(ErrorMessage = "Кількість оплачуваних днів відпустки повинна бути невід'ємним числом")]
        public string PaidDayOffs { get; set; }

        /// <summary>
        /// Gets or sets unpaid day offs.
        /// </summary>
        [PosNumber(ErrorMessage = "Кількість неоплачуваних днів відпустки повинна бути невід'ємним числом")]
        public string UnPaidDayOffs { get; set; }

        /// <summary>
        /// Gets or sets paid sickness.
        /// </summary>
        [PosNumber(ErrorMessage = "Кількість оплачуваних лікарняних повинна бути невід'ємним числом")]
        public string PaidSickness { get; set; }

        /// <summary>
        /// Gets or sets unpaid sickness.
        /// </summary>
        [PosNumber(ErrorMessage = "Кількість неоплачуваних лікарняних повинна бути невід'ємним числом")]
        public string UnPaidSickness { get; set; }

        /// <summary>
        /// Gets or sets bonus paid day offs.
        /// </summary>
        [PosNumberTo15(ErrorMessage = "Кількість бонусних днів відпустки повинна бути в межах від 0 до 15")]
        public string BonusPaidDayOffs { get; set; }
    }
}
