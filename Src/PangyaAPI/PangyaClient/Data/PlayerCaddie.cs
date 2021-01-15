using System;
using System.Runtime.InteropServices;
namespace PangyaAPI.PangyaClient.Data
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PlayerCaddie
    {
        public uint Index {get; set;}
        public uint TypeID {get; set;}
        public uint Skin_TypeID {get; set;}
        public byte Level {get; set;}
        public uint Experience {get; set;}
        public byte Type {get; set;}// 2 PANG 0 COOKIE
        public short Day_Left {get; set;}
        public short Skin_Hour_Left {get; set;}
        public byte Unknown {get; set;}
        public short Pay_Day {get; set;}
    }
}
