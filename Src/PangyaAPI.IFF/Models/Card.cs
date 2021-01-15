using PangyaAPI.IFF.Common;
using PangyaAPI.IFF.Flags;
using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Models
{
    /// <summary>
    /// Is Struct file Card.iff
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Card
    {
        [field: MarshalAs(UnmanagedType.Struct)]
        public IFFCommon Base;//iff base data
        public byte Rarity;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string Texture;
        public ushort PowerSlot;
        public ushort ControlSlot;
        public ushort AccuracySlot;
        public ushort SpinSlot;
        public ushort CurveSlot;
        public CardEffectFlag Effect;
        public ushort EffectValue;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string AdditionalTexture1;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string AdditionalTexture2;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string AdditionalTexture3;
        public ushort EffectTime;
        public ushort Volume;
        public ushort CardID;
        public uint Unknown0;
        public uint Unknown1;
    }
}
