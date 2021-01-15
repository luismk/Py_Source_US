using System;
using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Common
{
    /// <summary>
    /// System time structure based on Windows internal SYSTEMTIME struct
    /// </summary>     
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    /// <example>
    /// Reference: https://github.com/pangyatools/PangLib/blob/master/PangLib.IFF/Models/General/SystemTime.cs#L9
    /// </example>
    public struct SystemTime
    {
        /// <summary>
        /// Year
        /// </summary>
        public ushort Year { get; set; }

        /// <summary>
        /// Month
        /// </summary>
        public ushort Month { get; set; }

        /// <summary>
        /// Day of Week
        /// </summary>
        public ushort DayOfWeek { get; set; }

        /// <summary>
        /// Day
        /// </summary>
        public ushort Day { get; set; }

        /// <summary>
        /// Hour
        /// </summary>
        public ushort Hour { get; set; }

        /// <summary>
        /// Minute
        /// </summary>
        public ushort Minute { get; set; }

        /// <summary>
        /// Second
        /// </summary>
        public ushort Second { get; set; }

        /// <summary>
        /// Millisecond
        /// </summary>
        public ushort MilliSecond { get; set; }

        public bool CheckValues()
        {
           return Year == 0 & Month == 0 && Day == 0 && Hour == 0 && Minute == 0 && Second == 0;
        }
    }
}
