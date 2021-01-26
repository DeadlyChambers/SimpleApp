using Microsoft.AspNetCore.Mvc;
using SimpleAPI.Business.Managers;
using SimpleAPI.Common.Utilities;
using SimpleAPI.DTO;
using System.Threading.Tasks;

namespace SimpleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

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
        public async Task<PagedList<TeamDto>> Get([FromQuery]int? id = 1)
        {            
            return await _teamsManager.GetPageAsync(new PagedList<TeamDto>
            {
                CurrentPage = id.Value
            });
        }

        [HttpPost]        
        public  async Task<TeamDto> Post([FromBody]TeamDto team)
        {
            return (await _teamsManager.AddAsync(team));
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _teamsManager.DeleteAsync(id);
        }
    }
}