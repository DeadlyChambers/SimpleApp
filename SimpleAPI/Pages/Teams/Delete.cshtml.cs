using Microsoft.AspNetCore.Mvc;
using SimpleAPI.Business.Managers;
using SimpleAPI.DTO;
using SimpleAPI.PagePolicy;
using System.Threading.Tasks;

namespace SimpleAPI.Pages.Teams
{
    public class DeleteModel : UpdateBasePage
    {
        private readonly IEntitiesManager<TeamDto> _teamsManager;

        public DeleteModel(IEntitiesManager<TeamDto> teamsManager)
        {
            _teamsManager = teamsManager;
        }


        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _teamsManager.DeleteAsync(id.Value);

            return RedirectToPage("./Index");
        }
    }
}
