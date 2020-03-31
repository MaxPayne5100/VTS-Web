using VTS.Core.Helpers;
using System;

namespace VTS.DAL.Entities
{
    public enum StatusType
    {
        Pending,
        Approved,
        Canceled
    }

    public class HolidayAcception : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }

        public uint HeadId { get; set; }
        public Head Head { get; set; }

        public Guid HolidayId { get; set; }
        public Holiday Holiday { get; set; }

        public StatusType Status { get; set; }

        public string Description { get; set; }
    }
}