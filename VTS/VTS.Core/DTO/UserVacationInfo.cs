using System;

namespace VTS.Core.DTO
{
    /// <summary>
    /// UserVacationInfo DTO.
    /// </summary>
    public class UserVacationInfo
    {
        /// <summary>
        /// Gets or sets UserVacationInfo GUID identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets User identifier.
        /// </summary>
        public uint UserId { get; set; }

        /// <summary>
        /// Gets or sets User.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets paid day offs.
        /// </summary>
        public uint PaidDayOffs { get; set; }

        /// <summary>
        /// Gets or sets bonus paid day offs.
        /// </summary>
        public uint BonusPaidDayOffs { get; set; }

        /// <summary>
        /// Gets or sets unpaid day offs.
        /// </summary>
        public uint UnPaidDayOffs { get; set; }

        /// <summary>
        /// Gets or sets paid sickness.
        /// </summary>
        public uint PaidSickness { get; set; }

        /// <summary>
        /// Gets or sets unpaid sickness.
        /// </summary>
        public uint UnPaidSickness { get; set; }

        /// <summary>
        /// Gets or sets time when started working.
        /// </summary>
        public DateTime StartedWorking { get; set; }
    }
}
