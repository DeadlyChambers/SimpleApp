using Microsoft.AspNetCore.Mvc;
using SimpleAPI.Business.Managers;
using SimpleAPI.Common.Utilities;
using SimpleAPI.DTO;
using System.Threading.Tasks;

namespace SimpleAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class TeamsController : ControllerBase
    {
        private readonly IEntitiesManager<TeamDto> _teamsManager;


        public TeamsController(IEntitiesManager<TeamDto> teamsManager)
        {
            _teamsManager = teamsManager;
        }

        //public async Task Edit(TeamDto team)
        //{

        //    var team = await _teamsManager.UpdateAsync(Team);
        //    if (team == null)
        //        return NotFound();
        //    return RedirectToPage("./Index");
        //}

        [HttpGet]
        public async Task<PagedList<TeamDto>> Get([FromQuery]int id)
        {
            return await _teamsManager.GetPageAsync(new PagedList<TeamDto>
            {
                CurrentPage = id
            });
        }

        [HttpPost]        
        public  async Task<TeamDto> Post([FromBody]TeamDto team)
        {
            return (await _teamsManager.AddAsync(team));
        }

        [HttpDelete]
        public async Task Delete([FromRoute]int teamid)
        {
            await _teamsManager.DeleteAsync(teamid);
        }
    }
}