using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleAPI.Common.Utilities;
using SimpleAPI.DataAccess;
using SimpleAPI.DTO;
using SimpleAPI.DTO.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace SimpleAPI.Business.Managers
{
    /// <summary>
    /// THe super basic CRUD operations are happening with tTeam/Player/Game, but I know there is
    /// a way I can reuse this code. I am just struggling to see it right now.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BasicEntitiesManager<T> : IEntitiesManager<T> where T : IEntityRecord
    {
        private readonly FootballContext _context;
        private readonly IMapper _mapper;
        public BasicEntitiesManager(FootballContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<T> AddAsync(T newDto)
        {
            var typeT = typeof(T);
            if (typeT == typeof(PlayerDto))
            {
                var entity = _mapper.Map<Player>(newDto);
                entity.Createddate = DateTime.Now;
                await _context.Players.AddAsync(entity);
            }
            else if (typeT == typeof(GameDto))
            {
                var entity = _mapper.Map<Game>(newDto);
                entity.Createddate = DateTime.Now;
                await _context.Games.AddAsync(entity);
            }

            else
            {
                var entity = _mapper.Map<Team>(newDto);
                entity.Createddate = DateTime.Now;
                await _context.Teams.AddAsync(entity);
            }
            newDto.Id = await _context.SaveChangesAsync();
            return newDto;
        }

        public async Task<PagedList<T>> GetPageAsync(IPage pageInformation)
        {
            var typeT = typeof(T);
            if (typeT == typeof(PlayerDto))
            {
                pageInformation.TotalRecords = await _context.Players.CountAsync();
                var pagedInforationSanitized = new PagedList<T>(pageInformation);
                if (pageInformation.TotalRecords == 0)
                {
                    return pagedInforationSanitized;
                }
                var totalToSkip = (pagedInforationSanitized.CurrentPage - 1) * pagedInforationSanitized.PageSize;
                var recordsQ = _context.Players.Skip(totalToSkip)
                    .Take(pagedInforationSanitized.PageSize).IgnoreAutoIncludes();
                var records = recordsQ.Select(x => _mapper.Map<T>(x));
                pagedInforationSanitized.Items = await records.ToListAsync();
                return pagedInforationSanitized;
            }
            else if (typeT == typeof(GameDto))
            {
                pageInformation.TotalRecords = await _context.Games.CountAsync();

                var pagedInforationSanitized = new PagedList<T>(pageInformation);
                if (pageInformation.TotalRecords == 0)
                {
                    return pagedInforationSanitized;
                }
                var totalToSkip = (pagedInforationSanitized.CurrentPage - 1) * pagedInforationSanitized.PageSize;
                var recordsQ = _context.Games.Include(x => x.Hometeam).Include(x => x.Awayteam).Skip(totalToSkip)
                    .Take(pagedInforationSanitized.PageSize);
                var records = recordsQ.Select(x => _mapper.Map<T>(x));
                pagedInforationSanitized.Items = await records.ToListAsync();
                return pagedInforationSanitized;
            }
            else
            {
                pageInformation.TotalRecords = await _context.Teams.CountAsync();
               
                var pagedInforationSanitized = new PagedList<T>(pageInformation);
                if (pageInformation.TotalRecords == 0)
                {
                    return pagedInforationSanitized;
                }
                var totalToSkip = (pagedInforationSanitized.CurrentPage - 1) * pagedInforationSanitized.PageSize;
                var recordsQ = _context.Teams.Skip(totalToSkip)
                    .Take(pagedInforationSanitized.PageSize).IgnoreAutoIncludes();
                var records = recordsQ.Select(x => _mapper.Map<T>(x));
                pagedInforationSanitized.Items = await records.ToListAsync();
                return pagedInforationSanitized;
            }
        }

        public async Task<T> GetOrDefaultAsync(int id)
        {
            var typeT = typeof(T);
            if (typeT == typeof(PlayerDto))
            {
                var record = await _context.Players.FindAsync(id);
                if (record == null)
                    return default(T);
                return _mapper.Map<T>(record);
            }
            else if (typeT == typeof(TeamDto))
            {
                var record = await _context.Teams.FindAsync(id);
                if (record == null)
                    return default(T);
                return _mapper.Map<T>(record);
            }
            else
            {
                var record = await _context.Games
                    .Include(g => g.Awayteam)
                    .Include(g => g.Hometeam)
                .FirstOrDefaultAsync(m => m.Gameid == id);
                if (record == null)
                    return default(T);
                return _mapper.Map<T>(record);
            }
        }

        public async Task<T> UpdateAsync(T updatedDto)
        {
            var typeT = typeof(T);
            if (typeT == typeof(PlayerDto))
            {
                var currentEntity = await _context.Players.FindAsync(updatedDto.Id);

                if (currentEntity == null)
                    return default(T);
                var currentDto = _mapper.Map<T>(currentEntity);

                if (updatedDto.IsDirty(currentDto))
                {
                    var updateEntity = _mapper.Map<Player>(updatedDto);
                    currentEntity.Firstname = updateEntity.Firstname;
                    currentEntity.Lastname = updateEntity.Lastname;
                    currentEntity.Middlename = updateEntity.Middlename;
                    currentEntity.Height = updateEntity.Height;
                    currentEntity.Weight = updateEntity.Weight;

                    currentEntity.College = updateEntity.College;
                    currentEntity.Positions = updateEntity.Positions;
                    currentEntity.Status = updateEntity.Status;
                    currentEntity.DraftYear = updateEntity.DraftYear;
                    currentEntity.Dob = updateEntity.Dob;
                    currentEntity.PlayingStatus = updateEntity.PlayingStatus;
                    currentEntity.Modifieddate = updatedDto.Modifieddate = DateTime.Now;
                    _context.Attach(currentEntity).State = EntityState.Modified;
                }
            }
            else if (typeT == typeof(TeamDto))
            {
                var currentEntity = await _context.Teams.FindAsync(updatedDto.Id);

                if (currentEntity == null)
                    return default(T);
                var currentDto = _mapper.Map<T>(currentEntity);

                if (updatedDto.IsDirty(currentDto))
                {
                    var updateEntity = _mapper.Map<Team>(updatedDto);
                    currentEntity.Conference = updateEntity.Conference;
                    currentEntity.Division = updateEntity.Division;
                    currentEntity.Location = updateEntity.Location;
                    currentEntity.Name = updateEntity.Name;
                    currentEntity.Modifieddate = updatedDto.Modifieddate = DateTime.Now;
                    _context.Attach(currentEntity).State = EntityState.Modified;
                }

            }
            else
            {
                var currentEntity = await _context.Games
                    .Include(g => g.Awayteam)
                    .Include(g => g.Hometeam)
                .FirstOrDefaultAsync(m => m.Gameid == updatedDto.Id); 
                if (currentEntity == null)
                    return default(T);
                var currentDto = _mapper.Map<T>(currentEntity);
                if (updatedDto.IsDirty(currentDto)){
                    var updateEntity = _mapper.Map<Game>(updatedDto);
                    updatedDto.Modifieddate = updateEntity.Modifieddate = DateTime.Now;
                    _context.Attach(updateEntity).State = EntityState.Modified;
                }
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

        public async Task<int> DeleteAsync(int id)
        {
            var typeT = typeof(T);
            if (typeT == typeof(PlayerDto))
            {
                var entity = await _context.Players.FindAsync(id);

                if (entity != null)
                {
                    _context.Players.Remove(entity);
                    await _context.SaveChangesAsync();
                }
                return id;
            }
            else if (typeT == typeof(TeamDto))
            {
                var entity = await _context.Teams.FindAsync(id);

                if (entity != null)
                {
                    _context.Teams.Remove(entity);
                    await _context.SaveChangesAsync();
                }
                return id;
            }
            else
            {
                var entity = await _context.Games.FindAsync(id);

                if (entity != null)
                {
                    _context.Games.Remove(entity);
                    await _context.SaveChangesAsync();
                }
                return id;
            }
        }
    }
}
