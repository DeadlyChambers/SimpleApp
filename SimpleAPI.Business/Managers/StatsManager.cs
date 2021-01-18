using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleAPI.Common.Utilities;
using SimpleAPI.DataAccess;
using SimpleAPI.DTO.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAPI.Business.Managers
{
    public class StatsManager<T> : IMultiEntitiesManager<T> where T : IMultiEntityRecord
    {
        private readonly FootballContext _context;
        private readonly IMapper _mapper;
        public StatsManager(FootballContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<T> AddAsync(T newDto)
        {
            var entity = _mapper.Map<Stat>(newDto);
            //entity.Createddate = DateTime.Now;
            await _context.Stats.AddAsync(entity);

            await _context.SaveChangesAsync();
            return newDto;
        }

        public async Task<PagedList<T>> GetPageAsync(IPage pageInformation)
        {
            pageInformation.TotalRecords = await _context.Stats.CountAsync();
            var pagedInforationSanitized = new PagedList<T>(pageInformation);
            if (pageInformation.TotalRecords == 0)
            {
                return pagedInforationSanitized;
            }
            var totalToSkip = (pagedInforationSanitized.CurrentPage - 1) * pagedInforationSanitized.PageSize;
            var recordsQ = _context.Stats.Skip(totalToSkip)
                .Take(pagedInforationSanitized.PageSize).IgnoreAutoIncludes();
            var records = recordsQ.Select(x => _mapper.Map<T>(x));
            pagedInforationSanitized.Items = await records.ToListAsync();
            return pagedInforationSanitized;
        }

        public async Task<T> GetOrDefaultAsync(int gameId, int teamId, int playerId)
        {
            var record = await _context.Stats.
                Include(t => t.Team)
                .Include(p => p.Player)
                .Include(g => g.Game)
                .FirstOrDefaultAsync(s => s.Gameid == gameId && s.Teamid == teamId && s.Playerid == playerId);
            if (record == null)
                return default(T);
            return _mapper.Map<T>(record);
        }

        public async Task<T> UpdateAsync(T updatedDto)
        {
            var currentEntity = await _context.Stats.FindAsync(updatedDto.Gameid, updatedDto.Teamid, updatedDto.Playerid);

            if (currentEntity == null)
                return default(T);
            var currentDto = _mapper.Map<T>(currentEntity);

            if (updatedDto.IsDirty(currentDto))
            {
                var updateEntity = _mapper.Map<Stat>(updatedDto);
                //Make sure this is actually updating the entity
                _context.Attach(updateEntity).State = EntityState.Modified;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //Do some logging here potentially
                throw;
            }
            return updatedDto;
        }

        public async Task<string> DeleteAsync(int gameId, int teamId, int playerId)
        {
            var entity = await _context.Stats.FindAsync(gameId, teamId, playerId);

            if (entity != null)
            {
                _context.Stats.Remove(entity);
                await _context.SaveChangesAsync();
            }
            return $"{gameId}|{teamId}|{playerId}";
        }
    }
}
