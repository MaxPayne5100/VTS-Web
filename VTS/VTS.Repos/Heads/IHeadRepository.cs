using VTS.DAL.Entities;
using VTS.Repos.Generic;

namespace VTS.Repos.Heads
{
    /// <summary>
    /// Interface for Head Repository.
    /// </summary>
    public interface IHeadRepository : IGenericRepository<Head, int>
    {
    }
}