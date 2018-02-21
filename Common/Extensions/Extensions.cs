using NLog;

namespace Common.Extensions
{
    public static partial class Extensions
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public static PropertyCache PropertyCache = new PropertyCache();
    }
}
