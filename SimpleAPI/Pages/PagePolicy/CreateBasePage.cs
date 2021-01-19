using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleAPI.DataAccess;

namespace SimpleAPI.PagePolicy
{
    /// <summary>
    /// Any page that requires a login at the minimum
    /// </summary>
    [Authorize(Roles = SCCRoleConst.CreateRoles)]
    public class CreateBasePage : PageModel
    {
    }
}
