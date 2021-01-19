using Microsoft.Extensions.Logging;
using SimpleAPI.Business.Managers;
using SimpleAPI.Common.Utilities;
using SimpleAPI.DTO;
using SimpleAPI.PagePolicy;
using System.Threading.Tasks;

namespace SimpleAPI.Pages.Teams
{
    public class IndexModel : AnoynBase<IndexModel>
    {

        private readonly IEntitiesManager<TeamDto> _teamsManager;
        public IndexModel(IEntitiesManager<TeamDto> teamsManager, ILogger<IndexModel> logger) : base(logger)
        {
            _teamsManager = teamsManager;
        }


        public PagedList<TeamDto> PagedRecords { get; set; }

        public async Task OnGetAsync(int? pageIndex)
        {
            LogUserInfo();

            if (PagedRecords == null)
                PagedRecords = new PagedList<TeamDto>();
            if (pageIndex.HasValue)
                PagedRecords.CurrentPage = pageIndex.Value;
            PagedRecords = await _teamsManager.GetPageAsync(PagedRecords);
        }
    }
}
