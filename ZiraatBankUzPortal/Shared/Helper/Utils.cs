using NLog;
using System.Globalization;

namespace ZiraatBankUzPortal.Shared.Helper
{
    public static class Utils
    {
        public enum NLogType
        {
            Info,
            Warn,
            Error,
            Debug,
            Trace,
            Fatal
        }

        public static void NLogMessage(Type declaringType, string text, NLogType messageType)
        {
            switch (messageType)
            {
                case NLogType.Info:
                    LogManager.GetLogger(declaringType.FullName).Info(text);
                    break;
                case NLogType.Warn:
                    LogManager.GetLogger(declaringType.FullName).Warn(text);
                    break;
                case NLogType.Error:
                    LogManager.GetLogger(declaringType.FullName).Error(text);
                    break;
                case NLogType.Debug:
                    LogManager.GetLogger(declaringType.FullName).Debug(text);
                    break;
                case NLogType.Trace:
                    LogManager.GetLogger(declaringType.FullName).Trace(text);
                    break;
                case NLogType.Fatal:
                    LogManager.GetLogger(declaringType.FullName).Fatal(text);
                    break;
                default:
                    LogManager.GetLogger(declaringType.FullName).Info(text);
                    break;
            };
        }
        public static DateTime ToDateTime(string tarih)
        {
            DateTime dateTime;
            tarih = tarih.Trim().Replace('/', '.').Replace('-', '.').Replace('_', '.');

            if (tarih.IndexOf(':') != -1)
                tarih = tarih.Substring(0, tarih.IndexOf(':') - 2).Trim();

            if (DateTime.TryParseExact(tarih, "yyyy.MM.dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                return dateTime;
            else if (DateTime.TryParseExact(tarih, "d.M.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                return dateTime;
            else if (DateTime.TryParseExact(tarih, "d.MMM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                return dateTime;
            else if (DateTime.TryParseExact(tarih, "MMM d.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                return dateTime;
            else if (DateTime.TryParseExact(tarih, "DDD MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                return dateTime;
            else if (DateTime.TryParseExact(tarih, "M.d.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                return dateTime;
            else if (DateTime.TryParseExact(tarih, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                return dateTime;
            else
            {
                long ticks;
                if (long.TryParse(tarih, out ticks))
                    return new DateTime(ticks);
                return new DateTime(1900, 1, 1);
            }

        }
    }
}
