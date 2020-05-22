using VTS.DAL.Entities;
using VTS.Repos.Generic;

namespace VTS.Repos.Clerks
{
    /// <summary>
    /// Interface for Clerk Repository.
    /// </summary>
    public interface IClerkRepository : IGenericRepository<Clerk, int>
    {
    }
}