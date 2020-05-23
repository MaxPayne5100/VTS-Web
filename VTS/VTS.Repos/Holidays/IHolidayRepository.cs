using System;
using VTS.DAL.Entities;
using VTS.Repos.Generic;

namespace VTS.Repos.Holidays
{
    /// <summary>
    /// Interface for Holiday Repository.
    /// </summary>
    public interface IHolidayRepository : IGenericRepository<Holiday, Guid>
    {
    }
}