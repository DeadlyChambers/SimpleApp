using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SimpleAPI.Common.Utilities;
using System.Linq;

namespace SimpleAPI.PagePolicy
{
    public abstract class PolicyBase<T> : PageModel
    {
        protected PolicyBase(ILogger<T> logger)
        {
            Logger = logger;

        }

        protected readonly ILogger<T> Logger;

        /// <summary>
        /// Will log the Users Claims to Debug stream
        /// </summary>
        public void LogUserInfo()
        {
            if (User.Identity.IsAuthenticated)
                Logger.LogDebug(LogEvents.UserAuthInfo, "User Claims are {0}", $"{string.Join("|", User?.Claims?.Select(x => x.Type + " " + x.Value))}");
            else
            {
                Logger.LogDebug(LogEvents.UserAuthInfo, "Anonymous User");
            }
        }

        public bool IsCreator { get; set; }
        public bool IsEditor { get; set; }
    }

    [AllowAnonymous]

    public class AnoynBase<T> : PolicyBase<T>
    {
        public AnoynBase(ILogger<T> logger) : base(logger)
        {
        }

    }
}
