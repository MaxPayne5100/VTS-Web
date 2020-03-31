using VTS.Core.Helpers;
using System;

namespace VTS.DAL.Entities
{
    public class UserVacationInfo : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }

        public uint UserId { get; set; }
        public User User { get; set; }

        public uint PaidDayOffs { get; set; }

        public uint BonusPaidDayOffs { get; set; }

        public uint UnPaidDayOffs { get; set; }

        public uint PaidSickness { get; set; }

        public uint UnPaidSickness { get; set; }

        public DateTime StartedWorking { get; set; }
    }
}