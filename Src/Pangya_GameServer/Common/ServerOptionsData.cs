using Pangya_GameServer.Flags;
using PangyaAPI.Helper.BinaryModels;
using System.Runtime.InteropServices;
namespace Pangya_GameServer.Common
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ServerOptionsData
    {
        public ushort Unknown0 { get; set; }
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Unknown1 { get; set; }
        public uint Unknown3 { get; set; }
        public ServerOptionFlag MaintenanceFlags { get; set; }
        public uint Unknown4 { get; set; }
        public uint Property { get; set; }

        public void Set(ServerOptionFlag FuncFlags, uint property)
        {
            Unknown0 = 2;
            Unknown1 = new byte[]
            {
                0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF
            };
            Unknown3 = 0;
            Unknown4 = 0;
            MaintenanceFlags = FuncFlags;
            Property = property;
        }

        public byte[] GetInfo()
        {
            Unknown0 = 2;
            Unknown1 = new byte[]
            {
                0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF
            };
            Unknown3 = 0;
            Unknown4 = 0;
            MaintenanceFlags = ServerOptionFlag.MAINTENANCE_FLAG_PAPELSHOP;
            Property = 2048;
            using (var resp = new PangyaBinaryWriter())
            {
                resp.WriteStruct(this);
                return resp.GetBytes();
            }
        }
    }

    public class ServerOptions
    {
        ServerOptionsData m_data;

        public ServerOptions(ServerOptionFlag optionFlag, uint property)
        {
            SetOptions(optionFlag, property);
        }

        public ServerOptionsData OptionsData { get { return m_data; } }

        public void SetOptions(ServerOptionFlag optionFlag, uint property)
        {
            m_data = new ServerOptionsData();
            m_data.Set(optionFlag, property);
        }
    }
}
