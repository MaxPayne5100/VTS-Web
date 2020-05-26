using System;
using VTS.DAL.Entities;
using VTS.Repos.Generic;

namespace VTS.Repos.HolidaysAcception
{
    /// <summary>
    /// Interface for HolidayAcception Repository.
    /// </summary>
    public interface IHolidayAcceptionRepository : IGenericRepository<HolidayAcception, Guid>
    {
    }
}