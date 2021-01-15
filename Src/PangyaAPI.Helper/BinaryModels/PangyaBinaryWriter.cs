using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
namespace PangyaAPI.Helper.BinaryModels
{
    public class PangyaBinaryWriter : BinaryWriter
    {
        public Encoding Enc = ConfigEncoding.GetEncoding();
        public PangyaBinaryWriter()
        {
            this.OutStream = new MemoryStream();
        }

        public PangyaBinaryWriter(Stream output) : base(output)
        {
           
        }

        public uint GetSize
        {
            get { return (uint)BaseStream.Length; }
        }
        /// <summary>
        /// GetBytes Written in Binary
        /// </summary>
        /// <returns>Array Of Bytes</returns>
        public byte[] GetBytes()
        {
            if (OutStream is MemoryStream)
                return ((MemoryStream)OutStream).ToArray();


            using (var memoryStream = new MemoryStream())
            {
                memoryStream.GetBuffer();
                OutStream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public void Clear()
        {
            this.Flush();
            this.Close();
            this.OutStream = new MemoryStream();
        }

        public void SetEncoding(Encoding encoding)
        {
            Enc = encoding;
        }
        public bool WriteStr(string message, int length)
        {

            try
            {
                if (message == null)
                {
                    message = string.Empty;
                }
                var ret = new byte[length];
                Enc.GetBytes(message).Take(length).ToArray().CopyTo(ret, 0);
                Write(ret);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool WriteStr(string message)
        {
            try
            {
                WriteStr(message, message.Length);

            }
            catch
            {
                return false;
            }
            return true;

        }

        public bool WritePStr(string data)
        {
            if (data == null) data = "";
            try
            {
                var encoded = Enc.GetBytes(data);
                var length = data.Length;
                if (length >= ushort.MaxValue)
                {
                    return false;
                }
                Write((short)length);
                Write(encoded);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool Write(byte[] message, int length)
        {
            try
            {
                if (message == null)
                    message = new byte[length];

                var result = new byte[length];

                Buffer.BlockCopy(message, 0, result, 0, message.Length);

                Write(result);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool WriteZero(int Lenght)
        {
            try
            {
                Write(new byte[Lenght]);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool WriteUInt16(ushort value)
        {
            try
            {
                Write(value);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool WriteUInt16(int value)
        {
            try
            {
                Write((ushort)value);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool WriteUInt16(uint value)
        {
            try
            {
                Write((ushort)value);
            }
            catch
            {
                return false;
            }
            return true;
        }


        public bool WriteByte(byte value)
        {
            try
            {
                Write(value);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool WriteByte(int value)
        {
            try
            {
                Write(Convert.ToByte(value));
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool WriteSingle(float value)
        {
            try
            {
                Write(value);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool WriteUInt32(uint value)
        {
            try
            {
                Write(value);
            }
            catch
            {
                return false;
            }
            return true;
        }


        public bool WriteUInt32(int value)
        {
            try
            {
                Write(Convert.ToUInt32(value));
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool WriteInt32(int value)
        {
            try
            {
                Write(value);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool WriteUInt64(ulong value)
        {
            try
            {
                Write(value);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool WriteInt64(long value)
        {
            try
            {
                Write(value);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool WriteDouble(double value)
        {
            try
            {
                Write(value);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool WriteStruct(object value)
        {
            try
            {
                int size = Marshal.SizeOf(value);
                byte[] arr = new byte[size];

                IntPtr ptr = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(value, ptr, true);
                Marshal.Copy(ptr, arr, 0, size);
                Marshal.FreeHGlobal(ptr);
                Write(arr);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool WriteStruct(object value, int length)
        {
            try
            {
                int size = Marshal.SizeOf(value);
                byte[] arr = new byte[size];

                IntPtr ptr = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(value, ptr, true);
                Marshal.Copy(ptr, arr, 0, size);
                Marshal.FreeHGlobal(ptr);

                var result = new byte[length];

                Buffer.BlockCopy(arr, 0, result, 0, length);

                Write(result);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool WriteHexArray(string _value)
        {
            try
            {
                _value = _value.Replace(" ", "");
                int _size = _value.Length / 2;
                byte[] _result = new byte[_size];
                for (int ii = 0; ii < _size; ii++)
                    WriteByte(Convert.ToByte(_value.Substring(ii * 2, 2), 16));
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Write Pangya Time
        /// </summary>
        /// <returns></returns>
        public bool WriteTime(DateTime? date)
        {
            try
            {
                if (date.HasValue == false || date?.Ticks == 0)
                {
                    Write(new byte[16]);
                    return true;
                }
                WriteUInt16((ushort)date?.Year);
                WriteUInt16((ushort)date?.Month);
                WriteUInt16(Convert.ToUInt16(date?.DayOfWeek));
                WriteUInt16((ushort)date?.Day);
                WriteUInt16((ushort)date?.Hour);
                WriteUInt16((ushort)date?.Minute);
                WriteUInt16((ushort)date?.Second);
                WriteUInt16((ushort)date?.Millisecond);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Write Pangya Time
        /// </summary>
        /// <returns></returns>
        public bool WriteTime()
        {
            DateTime date = DateTime.Now;
            try
            {
                WriteUInt16((ushort)date.Year);
                WriteUInt16((ushort)date.Month);
                WriteUInt16((ushort)date.DayOfWeek);
                WriteUInt16((ushort)date.Day);
                WriteUInt16((ushort)date.Hour);
                WriteUInt16((ushort)date.Minute);
                WriteUInt16((ushort)date.Second);
                WriteUInt16((ushort)date.Millisecond);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void WriteObject(object obj)
        {
            foreach (var property in obj.GetType().GetProperties())
            {
                Type type = property.PropertyType;

                TypeCode typeCode = Type.GetTypeCode(type);

                switch (typeCode)
                {
                    case TypeCode.Empty:
                        break;
                    case TypeCode.Object:
                        {
                            if (type.Name == "Byte[]")
                            {
                                Write((byte[])obj);
                            }
                        }
                        break;
                    case TypeCode.DBNull:
                        break;
                    case TypeCode.Boolean:
                        {
                            Write((bool)obj);
                        }
                        break;
                    case TypeCode.Char:
                        {
                            Write((char)obj);
                        }
                        break;
                    case TypeCode.SByte:
                        {
                            Write((sbyte)obj);
                        }
                        break;
                    case TypeCode.Byte:
                        {
                            Write((byte)obj);
                        }
                        break;
                    case TypeCode.Int16:
                        {
                            Write((short)obj);
                        }
                        break;
                    case TypeCode.UInt16:
                        {
                            Write((ushort)obj);
                        }
                        break;
                    case TypeCode.Int32:
                        {
                            Write((int)obj);

                        }
                        break;
                    case TypeCode.UInt32:
                        Write((uint)obj);

                        break;
                    case TypeCode.Int64:
                        {
                            Write((long)obj);

                        }
                        break;
                    case TypeCode.UInt64:
                        {
                            Write((ulong)obj);

                        }
                        break;
                    case TypeCode.Single:
                        {
                            Write((float)obj);
                        }
                        break;
                    case TypeCode.Double:
                        {
                            Write((double)obj);
                        }
                        break;
                    case TypeCode.Decimal:
                        {
                            Write((decimal)obj);
                        }
                        break;
                    case TypeCode.DateTime:
                        {
                            WriteTime((DateTime)obj);
                        }
                        break;
                    case TypeCode.String:
                        {
                            WritePStr((string)obj);
                        }
                        break;
                    default:
                        {
                            Console.WriteLine("Object Type Name: " + typeCode);
                        }
                        break;
                }
            }
        }
    }
}
