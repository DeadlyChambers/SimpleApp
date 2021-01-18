using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleAPI.Common.Utilities;
using SimpleAPI.DataAccess;
using SimpleAPI.DTO.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAPI.Business.Managers
{
    /// <summary>
    /// These interface is specifically shared by the common entities with single primary
    /// keys
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntitiesManager<T> where T : IEntityRecord
    {
        Task<T> AddAsync(T teamDto);
        Task<PagedList<T>> GetPageAsync(IPage pageInformation);
        Task<T> GetOrDefaultAsync(int id);

        Task<T> UpdateAsync(T teamDto);

        Task<int> DeleteAsync(int id);

    }

    /// <summary>
    /// Instead of using an id of an int, there are multiple Ints, and one string called Id
    /// that creates a pipe delimited set of ids
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMultiEntitiesManager<T> where T : IMultiEntityRecord
    {
        Task<T> AddAsync(T teamDto);
        Task<PagedList<T>> GetPageAsync(IPage pageInformation);
        Task<T> GetOrDefaultAsync(int gameId, int teamId, int playerId);

        Task<T> UpdateAsync(T teamDto);

        Task<string> DeleteAsync(int gameId, int teamId, int playerId);

    }

}