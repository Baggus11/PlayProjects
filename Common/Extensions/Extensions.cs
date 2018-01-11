using NLog;

namespace Common.Extensions
{
    public static partial class Extensions
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public static PropertyCacher PropertyCache = new PropertyCacher();
    }
}
