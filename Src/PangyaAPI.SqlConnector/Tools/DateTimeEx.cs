using System;
using System.Linq;
using System.Text;

namespace PangyaAPI.SqlConnector.Tools
{
    public static class DateTimeEx
    {
        public static string GetSQLTime(this DateTime? Date)
        {
            if (Date.HasValue == false || Date?.Ticks == 0)
            {
                Date = DateTime.Now;
            }

            StringBuilder BuilderString;

            BuilderString = new StringBuilder();
            try
            {

                BuilderString.Append(Date.Value.ToString("yyyy-MM-dd"));
                BuilderString.Append('T');
                BuilderString.Append(Date.Value.ToString("hh:mm:ss"));
                return BuilderString.ToString();
            }
            finally
            {
                BuilderString.Clear();
            }
        }


        public static string GetSQLTime(this uint? Date)
        {
            return GetSQLTime(UnixTimeConvert(Date));
        }
        /// <summary>
        /// Format : yyyy-MM-dd HH:mm:ss:fff
        /// </summary>
        /// <param name="Time">Use for formated</param>
        /// <returns></returns>
        public static string GetSQLTimeFormat(this DateTime? Date)
        {
            DateTime Time = (DateTime)Date;

            if (Time == null)
            {
                return DateTime.MinValue.ToString("yyyy/dd/mm HH:mm:ss:fff");
            }
            else
            {
                return Time.ToString("yyyy/dd/mm HH:mm:ss:fff");
            }
        }

        public static uint DaysBetween(this DateTime? d1, DateTime d2)
        {
            TimeSpan span = d1.Value.Subtract(d2);
            return Convert.ToUInt32(span.Days);
        }


        public static long UnixTimeConvert(this DateTime? unixtime)
        {
            if (unixtime.HasValue == false || unixtime?.Ticks == 0)
            { return 0; }
            TimeSpan timeSpan = (TimeSpan)(unixtime - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds;
        }

        public static uint UnixTimeConvert(this DateTime unixtime)
        {
            TimeSpan timeSpan = unixtime - new DateTime(1970, 1, 1, 0, 0, 0);
            return (uint)timeSpan.TotalSeconds;
        }

        public static DateTime UnixTimeConvert(this long unixtime)
        {
            var timeInTicks = unixtime * TimeSpan.TicksPerSecond;
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddTicks(timeInTicks);
            return dtDateTime;
        }

        public static DateTime UnixTimeConvert(this uint? unixtime)
        {
            if (unixtime == null)
            {
                unixtime = 0;
            }
            var timeInTicks = (uint)unixtime * TimeSpan.TicksPerSecond;
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddTicks(timeInTicks);
            return dtDateTime;
        }

        public static uint UnixTimeConvert(this uint unixtime)
        {
            var timeInTicks = unixtime * TimeSpan.TicksPerSecond;
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddTicks(timeInTicks);
            return uint.Parse(dtDateTime.ToString("yyyy/dd/MM/HH").Replace("/", ""));
        }


        public static byte[] GetFixTime(this DateTime? date)
        {
            if (date.HasValue == false || date?.Ticks == 0)
                return new byte[16];

            var DayOfWeek = Convert.ToUInt16(date?.DayOfWeek);
            return new ushort[]
            {
                (ushort)date?.Year,
                (ushort)date?.Month,
                 DayOfWeek,
                (ushort)date?.Day,
                (ushort)date?.Hour,
                (ushort)date?.Minute,
                (ushort)date?.Second,
                (ushort)date?.Millisecond
            }
            .SelectMany(v => BitConverter.GetBytes(v))
            .ToArray();
        }
    }
}
