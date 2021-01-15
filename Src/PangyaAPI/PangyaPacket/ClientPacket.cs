using PangyaAPI.Helper.BinaryModels;
using PangyaAPI.Crypt;
using PangyaAPI.Helper.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace PangyaAPI.PangyaPacket
{
    public class Packet
    {
        #region Private Fields
        private readonly MemoryStream _stream;
        /// <summary>
        /// Leitor do packet
        /// </summary>
        private PangyaBinaryReader Reader;

        /// <summary>
        /// Mensagem do Packet
        /// </summary>
        public byte[] Message { get; set; }

        private byte[] MessageCrypted { get; set; }
        #endregion

        #region Public Fields
        /// <summary>
        /// Id do Packet
        /// </summary>
        public short Id { get; set; }
        #endregion

        #region Constructor

        public Packet(byte[] message, byte key)
        {
            Id = BitConverter.ToInt16(new byte[] { message[5], message[6] }, 0);

            MessageCrypted = new byte[message.Length];
            Buffer.BlockCopy(message, 0, MessageCrypted, 0, message.Length); //Copia mensagem recebida criptografada
           Cryptor.pangya_client_decrypt(ref message, key);
            Message = message;
            _stream = new MemoryStream(Message);

            _stream.Seek(2, SeekOrigin.Current); //Seek Inicial
            Reader = new PangyaBinaryReader(_stream);
        }

        #endregion

        #region Methods Get
        public void Deserialize(ref uint deserialize)
        {
            Cryptor.pangya_deserialize(ref deserialize);
        }

        public uint Deserialize(uint deserialize)
        {
            Cryptor.pangya_deserialize(ref deserialize);
            return deserialize;
        }

        public uint GetSize
        {
            get => Reader.GetSize;
        }
        public uint GetPos
        {
            get => Reader.GetPosition;
        }

        public double ReadDouble()
        {
            return Reader.ReadDouble();
        }

        public byte ReadByte()
        {
            return Reader.ReadByte();
        }
        public short ReadInt16()
        {
            return Reader.ReadInt16();
        }
        public ushort ReadUInt16()
        {
            return Reader.ReadUInt16();
        }


        public uint ReadUInt32()
        {
            return Reader.ReadUInt32();
        }

        public int ReadInt32()
        {
            return Reader.ReadInt32();
        }

        public ulong ReadUInt64()
        {
            return Reader.ReadUInt64();
        }

        public long ReadInt64()
        {
            return Reader.ReadInt64();
        }

        public float ReadSingle()
        {
            return Reader.ReadSingle();
        }

        public string ReadPStr()
        {
            return Reader.ReadPStr();
        }
        public void Skip(int count)
        {
            Reader.Skip(count);
        }


        public void Seek(int offset, int origin)
        {
            Reader.Seek(offset, origin);
        }

        public object ReadObject(object obj)
        {
            return Reader.ReadObject(obj);
        }


        public object ReadObject(object obj, uint Count)
        {
            return Reader.ReadObject(obj, (int)Count);
        }


        public bool ReadObject(out object obj)
        {
            Reader.ReadObject(out obj);
            return true;
        }

        public T Read<T>() where T : struct
        {
            return Reader.Read<T>();
        }
        public IEnumerable<uint> Read(uint count)
        {
            return Reader.Read(count);
        }
        public object Read(object value, int Count)
        {
            return Reader.Read(value, Count);
        }

        public object Read(object value)
        {
            return Reader.Read(value);
        }

        public string ReadPStr(uint Count)
        {
            var data = new byte[Count];
            //ler os dados
            Reader.BaseStream.Read(data, 0, (int)Count);
            var value = Encoding.ASCII.GetString(data);
            return value;
        }

        public bool ReadPStr(out string value, uint Count)
        {
            return Reader.ReadPStr(out value, Count);
        }
        public bool ReadPStr(out string value)
        {
            return Reader.ReadPStr(out value);
        }
        public bool ReadDouble(out Double value)
        {
            return Reader.ReadDouble(out value);
        }
        public bool ReadBytes(out byte[] value)
        {
            return Reader.ReadBytes(out value);
        }
        public bool ReadByte(out byte value)
        {
            return Reader.ReadByte(out value);
        }
        public bool ReadInt16(out short value)
        {
            return Reader.ReadInt16(out value);
        }
        public bool ReadUInt16(out ushort value)
        {
            return Reader.ReadUInt16(out value);
        }

        public bool ReadUInt32(out uint value)
        {
            return Reader.ReadUInt32(out value);
        }

        public bool ReadInt32(out int value)
        {
            return Reader.ReadInt32(out value);
        }

        public bool ReadUInt64(out ulong value)
        {
            return Reader.ReadUInt64(out value);
        }

        public bool ReadInt64(out long value)
        {
            return Reader.ReadInt64(out value);
        }

        public bool ReadSingle(out float value)
        {
            return Reader.ReadSingle(out value);
        }


        public byte[] GetRemainingData
        {
            get => Reader.GetRemainingData();
        }
        public byte[] ReadBytes(int count)
        {
            return Reader.ReadBytes(count);
        }

        public void SetReader(PangyaBinaryReader read)
        {
            Reader = read;
        }

        public PangyaBinaryReader GetReader { get { return this.GetReader; } }

        public void Log()
        {
            WriteConsole.Write($"PacketSize({Message.Length})", ConsoleColor.Cyan);
            WriteConsole.Write($"{Message.HexDump()}", ConsoleColor.Cyan);
            WriteConsole.WriteLine();
        }

        public string StringLog()
        {
            return Message.HexDump();
        }
        #endregion
    }
}
