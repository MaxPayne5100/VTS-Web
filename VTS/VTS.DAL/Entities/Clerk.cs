using VTS.Core.Helpers;

namespace VTS.DAL.Entities
{
    public class Clerk : IIdentifiable<uint>
    {
        public uint Id { get; set; }

        public uint HeadId { get; set; }
        public Head Head { get; set; }
    }
}