using Pangya_GameServer.Flags;
using PangyaAPI.IFF;
using System;
using System.Linq;

namespace Pangya_GameServer.Common
{
    public class ClubStatus
    {
        public ushort Power { get; set; }
        public ushort Control { get; set; }
        public ushort Impact { get; set; }
        public ushort Spin { get; set; }
        public ushort Curve { get; set; }
        public ClubTypeFlag ClubType { get; set; }
        public byte ClubSPoint { get; set; }


        public ClubStatus()
        { 
        }

        public ClubStatus(uint TypeID)
        {
            var ClubInfo = IFFEntry.ClubSet.First(c=> c.Base.TypeID == TypeID);
            Power = ClubInfo.MaxPow;
            Control = ClubInfo.MaxCon;
            Impact = ClubInfo.MaxImp;
            Spin = ClubInfo.MaxSpin;
            Curve = ClubInfo.MaxCurve;
            ClubType = (ClubTypeFlag)ClubInfo.ClubType;
            ClubSPoint = (byte)ClubInfo.ClubSPoint;
        }

        // ClubStatus
        public byte[] GetClubArray()
        {
            byte[] result = new byte[5];
            result[0] = (byte)Power;
            result[1] = (byte)Control;
            result[2] = (byte)Impact;
            result[3] = (byte)Spin;
            result[4] = (byte)Curve;
            return result;
        }

        public ClubStatus GetClub(ClubStatus ClubData)
        {
            ClubStatus result;
            result = this + ClubData;
            return result;
        }

        public int GetClubTotal(ClubStatus ClubData, bool IsRankUp)
        {
            int result;
            result = (Power + Control + Impact + Spin + Curve + ClubData.Power + ClubData.Control + ClubData.Impact + ClubData.Spin + ClubData.Curve);
            if (IsRankUp)
            {
                result += 1;
            }
            return result;
        }


        public static ClubStatus operator -(ClubStatus X, ClubStatus Y)
        {
            ClubStatus result = new ClubStatus()
            {
                Power = Convert.ToUInt16(Y.Power - X.Power),
                Control = Convert.ToUInt16(Y.Control - X.Control),
                Impact = Convert.ToUInt16(Y.Impact - X.Impact),
                Spin = Convert.ToUInt16(Y.Spin - X.Spin),
                Curve = Convert.ToUInt16(Y.Curve - X.Curve)
            };

            return result;
        }

        public static ClubStatus operator +(ClubStatus X, ClubStatus Y)
        {
            ClubStatus result = new ClubStatus()
            {
                Power = Convert.ToUInt16(X.Power + Y.Power),
                Control = Convert.ToUInt16(X.Control + Y.Control),
                Impact = Convert.ToUInt16(X.Impact + Y.Impact),
                Spin = Convert.ToUInt16(X.Spin + Y.Spin),
                Curve = Convert.ToUInt16(X.Curve + Y.Curve)
            };
            return result;
        }

    }
}
