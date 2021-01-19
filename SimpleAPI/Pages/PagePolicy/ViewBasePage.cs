using Microsoft.AspNetCore.Authorization;
using SimpleAPI.DataAccess;

namespace SimpleAPI.PagePolicy
{
    /// <summary>
    /// Any page that requires a login at the minimum
    /// </summary>
    [Authorize(Roles = SCCRoleConst.ReadRoles)]
    public class ViewBasePage : PolicyBase<ViewBasePage>
    {
        public ViewBasePage() : base(null)
        {

        }
    }
}
