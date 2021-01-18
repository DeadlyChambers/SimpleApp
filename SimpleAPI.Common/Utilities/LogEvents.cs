using Microsoft.Extensions.Logging;

namespace SimpleAPI.Common.Utilities
{
    public static class LogEvents
    {
        public static EventId UserAuthInfo = new EventId(10, "User Auth");

        public static EventId StartUpInfo = new EventId(11, "Start Up");
    }
}
