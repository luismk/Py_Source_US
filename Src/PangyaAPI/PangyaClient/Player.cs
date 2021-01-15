using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using PangyaAPI.Helper.BinaryModels;
using PangyaAPI.Helper;
using PangyaAPI.Server;
using PangyaAPI.SqlConnector.DataBase;
using PangyaAPI.PangyaClient.Data;

namespace PangyaAPI.PangyaClient
{
    public abstract partial class Player : IDisposeable
    {
        #region Public Fields

        /// <summary>
        /// Cliente está conectado
        /// </summary>
        public bool Connected { get; set; }

        /// <summary>
        /// Servidor em que o cliente está conectado
        /// </summary>
        public TcpServer Server { get; set; }

        /// <summary>
        /// Conexão do cliente
        /// </summary>
        public TcpClient Tcp { get; set; }
        /// <summary>
        /// Chave de criptografia e decriptografia
        /// </summary>
        public byte Key { get; private set; }

        public PangyaBinaryWriter Response { get; set; }
        /// <summary>
        /// Identificador da conexão
        /// </summary>
        public uint ConnectionID { get; set; }
        public UInt32 GetUID { get; set; }
        public byte GetFirstLogin { get; set; }
        public string GetLogin { get; set; } = string.Empty;
        public string GetNickname { get; set; } = string.Empty;
        public byte GetSex { get; set; }
        public byte GetCapability { get; set; } = 0;
        public string GetAuth1 { get; set; } = String.Empty;
        public string GetAuth2 { get; set; } = String.Empty;

        public PlayerStatistic UserStatistic;
        public byte GetLevel { get { return UserStatistic.Level; } set { UserStatistic.Level = value; } }

        /// <summary>
        /// IP Adress Client
        /// </summary>

        /// </summary>
        public string GetAdress
        {
            get
            {
                if (Tcp.Connected)
                {
                    return ((IPEndPoint)Tcp.Client.RemoteEndPoint).Address.ToString();
                }
                else
                {
                    return "IP NULL";
                }
            }
        }

        public string GetPort
        {
            get
            {
                if (Tcp.Connected)
                {
                    return ((IPEndPoint)Tcp.Client.RemoteEndPoint).Port.ToString();
                }
                else
                {
                    return "PORT NULL";
                }
            }
        }


        public NetworkStream NetworkStream
        {
            get
            {
                return Tcp.GetStream();
            }
        }

        public PangyaEntities _db { get; set; }

        #endregion

        #region Constructor

        public Player(TcpClient tcp)
        {
            Tcp = tcp;
            //Gera uma chave dinâmica(Max Value hexadecimal value: FF (255))
            Key = 16/*Convert.ToByte(new Random().Next(0, 15))*/;
            Response = new PangyaBinaryWriter(new MemoryStream());
            UserStatistic.Init();
            Connected = true;

            _db = new PangyaEntities();
        }

        public void Disconnect()
        {
            Server.DisconnectPlayer(this);
        }
        #endregion

        #region Player Send Packets 

        public void SendFile(string name)
        {
            var data = File.ReadAllBytes(name);
            Send(data);
        }
        /// <summary>
        /// not compression
        /// </summary>
        /// <param name="session"></param>
        /// <param name="data"></param>
        public void SendPacket(byte[] data)
        {
            SendEncrypted(data);
        }
        public void Send(byte[] data)
        {
            data = Crypt.ServerCipher.Encrypt(data, Key, 0);
            SendEncrypted(data);
        }

        public void Send(PangyaBinaryWriter resp)
        {
            var data = Crypt.ServerCipher.Encrypt(resp.GetBytes(), Key, 0);
            SendEncrypted(data);
        }


        public void SendResponse()
        {
            var DataCompression = Crypt.ServerCipher.Encrypt(Response.GetBytes(), Key, 0);
            Response.Clear();
            SendEncrypted(DataCompression);
        }

        public void SendResponse(byte[] data)
        {
            var DataCompression = Crypt.ServerCipher.Encrypt(data, Key, 0);
            SendEncrypted(DataCompression);
        }

        public void SendResponse(PangyaBinaryWriter packet)
        {
            SendResponse(packet.GetBytes());
        }

        public void SendEncrypted(byte[] buffer)
        {
            if (Tcp.Connected)
            {
                Tcp.Client.Send(buffer, SocketFlags.None);
            }
        }

        public void Close()
        {
            if (Tcp.Connected)
            {
                Tcp.Close();
            }
        }

        #endregion

        #region Dispose

        // booleano para controlar se
        // o método Dispose já foi chamado
        public bool Disposed { get; set; }

        // método privado para controle
        // da liberação dos recursos
        private void Dispose(bool disposing)
        {
            // Verifique se Dispose já foi chamado.
            if (!this.Disposed)
            {
                if (disposing)
                {
                    // Liberando recursos gerenciados
                    this.Connected = false;
                    Tcp.Dispose();
                }

                // Seta a variável booleana para true,
                // indicando que os recursos já foram liberados
                Disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }



        /// <summary>
        /// Destrutor
        /// </summary>
        ~Player()
        {
            Dispose(false);
        }


        #endregion
    }
}
