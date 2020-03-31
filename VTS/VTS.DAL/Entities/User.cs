using VTS.Core.Helpers;
using System;
using System.Collections.Generic;

namespace VTS.DAL.Entities
{
    public class User : IIdentifiable<uint>
    {
        public uint Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public Employee Employee { get; set; }

        public Head Head { get; set; }
        
        public UserVacationInfo UserVacationInfo { get; set; }

        public ICollection<Holiday> Holidays { get; set; }
    }
}