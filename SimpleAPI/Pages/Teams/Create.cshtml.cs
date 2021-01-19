using Microsoft.AspNetCore.Mvc;
using SimpleAPI.Business.Managers;
using SimpleAPI.DTO;
using SimpleAPI.PagePolicy;
using System.Threading.Tasks;

namespace SimpleAPI.Pages.Teams
{
    public class CreateModel : CreateBasePage
    {
        private readonly IEntitiesManager<TeamDto> _teamsManager;

        public CreateModel(IEntitiesManager<TeamDto> teamsManager)
        {
            _teamsManager = teamsManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public TeamDto Team { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Team = await _teamsManager.AddAsync(Team);

            return RedirectToPage("./Index");
        }
    }
}
