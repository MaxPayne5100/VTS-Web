using VTS.Core.Helpers;
using System.Collections.Generic;

namespace VTS.DAL.Entities
{
    public class Manager : IIdentifiable<uint>
    {
        public uint Id { get; set; }

        public uint HeadId { get; set; }
        public Head Head { get; set; }

        public ICollection<Employee> Subordinates { get; set; }
    }
}