using VTS.Core.Helpers;

namespace VTS.DAL.Entities
{
    public class Employee : IIdentifiable<uint>
    {
        public uint Id { get; set; }

        public uint UserId { get; set; }
        public User User { get; set; }

        public uint ManagerId { get; set; }
        public Manager Manager { get; set; }
    }
}