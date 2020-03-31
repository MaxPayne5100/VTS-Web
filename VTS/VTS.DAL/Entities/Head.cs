using VTS.Core.Helpers;
using System.Collections.Generic;

namespace VTS.DAL.Entities
{
    public class Head : IIdentifiable<uint>
    {
        public uint Id { get; set; }

        public uint UserId { get; set; }
        public User User { get; set; }

        public Clerk Clerk { get; set; }

        public Manager Manager { get; set; }

        public ICollection<HolidayAcception> HolidayAcception { get; set; }
    }
}