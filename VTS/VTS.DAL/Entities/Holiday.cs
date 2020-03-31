using VTS.Core.Helpers;
using System;

namespace VTS.DAL.Entities
{
    public enum Categories
    {
        PaidDayOffs,
        UnPaidDayOffs,
        PaidSickness,
        UnPaidSickness
    }

    public class Holiday : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }

        public uint UserId { get; set; }
        public User User { get; set; }

        public Categories Category { get; set; }

        public uint Hours { get; set; }

        public DateTime Start { get; set; }

        public DateTime Expires { get; set; }

        public string Description { get; set; }

        public DateTime SubmissionTime { get; set; }

        public HolidayAcception HolidayAcception { get; set; }

        public Holiday()
        {
            this.SubmissionTime = DateTime.Now;
        }
    }
}