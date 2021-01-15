using PangyaAPI.IFF.Common;
using PangyaAPI.IFF.Flags;
using PangyaAPI.SqlConnector.Tools;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static System.Math;
namespace PangyaAPI.IFF.Tools
{
    public static class IFFTools
    {
        public static uint GenerateNewTypeID(this uint iffType, int characterId, int int_0, int group, int type, int serial)
        {
            if (group - 1 < 0)
            {
                group = 0;
            }
            return Convert.ToUInt32((iffType * Math.Pow(2.0, 26.0)) + (characterId * Math.Pow(2.0, 18.0)) + (int_0 * Math.Pow(2.0, 13.0)) + (group * Math.Pow(2.0, 11.0)) + (type * Math.Pow(2.0, 9.0)) + serial);
        }

        public static uint TypeIdItem(this uint TypeID)
        {
            return (uint)((int)((TypeID & 0x3fc0000) / Math.Pow(2.0, 18.0)));
        }
     
        public static uint[] GetTypeIdValues(this uint TypeID)
        {
            uint[] _TypeIDValues = new uint[6];
            _TypeIDValues[0] = ((uint)((TypeID & 0x3fc0000) / Math.Pow(2.0, 18.0)));
            _TypeIDValues[1] = (ushort)((TypeID & 0x3fc0000) / Math.Pow(2.0, 18.0));
            _TypeIDValues[2] = (ushort)((TypeID & 0xfc000000) / Math.Pow(2.0, 26.0));
            _TypeIDValues[3] = (ushort)((TypeID & 0x1f0000) / Math.Pow(2.0, 16.0));
            _TypeIDValues[4] = (ushort)((TypeID & 0x3e003) / Math.Pow(2.0, 13.0));
            _TypeIDValues[5] = (ushort)(TypeID & 0xff);
            return _TypeIDValues;
        }

        public static uint SizeStruct(object obj)
        {
            return (uint)Marshal.SizeOf(obj);
        }

        public static IffGroupFlag GetItemGroup(this uint TypeId)
        {
            uint result;
            result = (uint)Math.Round((TypeId & 0xFC000000) / Math.Pow(2.0, 26.0));
            return (IffGroupFlag)result;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static CardTypeFlag GetCardType(uint TypeID)
        {
            if (Round((TypeID & 0xFF000000) / Pow(2.0, 24.0)) == 0x7C)
            { return (CardTypeFlag)Round((TypeID & 0x00FF0000) / Pow(2.0, 16.0)); }

            if (Round((TypeID & 0xFF000000) / Pow(2.0, 24.0)) == 0x7D)
            {
                if (Round((TypeID & 0x00FF0000) / Pow(2.0, 16.0)) == 0x40)
                { }
            }
            return CardTypeFlag.NPC;
        }

        public static uint GetCharacter(uint TypeID)
        {
            var Character = ((uint)((TypeID & 0x3fc0000) / Pow(2.0, 18.0)));
            return Character;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static byte GetAuxType(uint ID)
        {
            byte result;

            result = (byte)Round((ID & 0x001F0000) / Pow(2.0, 16.0));

            return result;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static uint GetCaddieTypeIDBySkinID(uint SkinTypeID)
        {
            uint result;
            uint CaddieTypeID;
            CaddieTypeID = (uint)Round(a: ((SkinTypeID & 0x0FFF0000) >> 16) / 32);
            result = (CaddieTypeID + 0x1C000000) + ((SkinTypeID & 0x000F0000) >> 16);
            return result;
        }

        public static bool CardCheckPosition(this uint TypeID, uint Slot)
        {
            bool result = true;

            switch (Slot)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    {
                        if (!(GetCardType(TypeID) == CardTypeFlag.Normal))
                        {
                            result = false;
                        }

                    }
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                    {
                        if (!(GetCardType(TypeID) == CardTypeFlag.Caddie))
                        {
                            result = false;
                        }

                    }
                    break;
                case 9:
                case 10:
                    {
                        if (!(GetCardType(TypeID) == CardTypeFlag.NPC))
                        {
                            result = false;
                        }
                    }
                    break;
            }

            return result;
        }

        public static DateTime ToDateTime(this SystemTime system)
        {
            if (system.CheckValues())
            {
                return DateTime.Now;
            }
            return new DateTime(system.Year, system.Month, system.Day, system.Hour, system.Minute, system.Second);
        }

        public static SystemTime ToSystemTime(this DateTime date)
        {
            return new SystemTime
            {
                Year = (ushort)date.Year,
                Month = (ushort)date.Month,
                Day = (ushort)date.Day,
                DayOfWeek = (ushort)date.DayOfWeek,
                Hour = (ushort)date.Hour,
                Minute = (ushort)date.Minute,
                Second = (ushort)date.Second,
                MilliSecond = (ushort)date.Millisecond
            };
        }


        public static uint ToUnixTime(this SystemTime system)
        {
            return system.ToDateTime().UnixTimeConvert();
        }
    }
}
