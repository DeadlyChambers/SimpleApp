using Microsoft.AspNetCore.Mvc;
using SimpleAPI.Business.Managers;
using SimpleAPI.DTO;
using SimpleAPI.PagePolicy;
using System.Threading.Tasks;

namespace SimpleAPI.Pages.Teams
{
    public class DetailsModel : ViewBasePage
    {
        private readonly IEntitiesManager<TeamDto> _teamsManager;

        public DetailsModel(IEntitiesManager<TeamDto> teamsManager)
        {
            _teamsManager = teamsManager;
        }

        public TeamDto Team { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Team = await _teamsManager.GetOrDefaultAsync(id.Value);

            if (Team == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
