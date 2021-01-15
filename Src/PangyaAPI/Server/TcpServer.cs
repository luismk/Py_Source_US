using PangyaAPI.Auth.AuthPacket;
using PangyaAPI.Auth.Client;
using PangyaAPI.Helper;
using PangyaAPI.PangyaClient;
using PangyaAPI.PangyaPacket;
using PangyaAPI.Helper.Tools;
using System;
using System.Net;
using System.Net.Sockets;
using PangyaAPI.Auth.Common;
using System.IO;
using PangyaAPI.Helper.BinaryModels;
using PangyaAPI.SqlConnector.DataBase;
using System.Linq;

namespace PangyaAPI.Server
{
    public abstract partial class TcpServer
    {
        #region Fields
        public GenericDisposableCollection<Player> Players { get; set; } = new GenericDisposableCollection<Player>();

        public uint NextConnectionId { get; set; }

        //tcpListener
        private TcpListener m_Listener;
       
        private byte[] MsgBufferRead { get; set; } = new byte[500000];


        public int ClientCount { get; set; }

        public bool IsRunning { get; set; }

        public IPAddress Adress { get; set; }

        public int Port { get; set; }

        public ClientAuth AuthServer;

        public ServerInfo Data;

        public bool ShowLog { get; set; }

        public bool OpenServer;
        #endregion

        #region Public Delegates
        public delegate void ConnectedEvent(Player player);
        public delegate void PlayerKeyConnectionEvent(Player player);
        public delegate void DisconnectEvent(Player player);
        public delegate void PacketReceivedEvent(Player player, Packet packet);
        #endregion

        #region Public Events
        /// <summary>
        /// Client Connect
        /// </summary>
        public event ConnectedEvent OnClientConnected;
        /// <summary>
        /// Client Disconnect
        /// </summary>
        public event DisconnectEvent OnClientDisconnected;
        /// <summary>
        /// Receives Client Data 
        /// </summary>
        public event PacketReceivedEvent OnPacketReceived;

        public event PlayerKeyConnectionEvent OnClientKey;
        #endregion

        #region Abstract Methods
        /// <summary>
        /// Envia chave para o player
        /// </summary>
        protected abstract void SendKey(Player player);

        protected abstract Player OnConnectPlayer(TcpClient tcp);

        public abstract void DisconnectPlayer(Player Player);

        protected abstract void ServerExpection(Player Client, Exception Ex);

        public abstract void ServerStart();

        public abstract Player GetClientByConnectionId(UInt32 ConnectionId);

        public abstract Player GetPlayerByNickname(string Nickname);

        public abstract Player GetPlayerByUsername(string Username);

        public abstract Player GetPlayerByUID(UInt32 UID);

        public abstract bool GetPlayerDuplicate(UInt32 UID);
        public abstract bool PlayerDuplicateDisconnect(UInt32 UID);

        public abstract void RunCommand(string[] Command);

        #region AuthServer 

        protected abstract ClientAuth AuthServerConstructor();

        protected abstract void OnAuthServerPacketReceive(ClientAuth client, AuthPacketInfo packet);

        #endregion

        #endregion

        #region Constructor
        public TcpServer(string localIPAddress, int listenPort)
        {

            Adress = IPAddress.Parse(localIPAddress);
            Port = listenPort;

            Players = new GenericDisposableCollection<Player>();
            MsgBufferRead = new byte[500000];
            m_Listener = new TcpListener(Adress, Port);
            m_Listener.Server.SendBufferSize = 1024 * 8;
            m_Listener.Server.ReceiveBufferSize = 1024 * 8;
            m_Listener.Server.NoDelay = true;
            m_Listener.AllowNatTraversal(true);

            //if (ConnectToAuthServer(AuthServerConstructor()) == false)
            //{
            //    WriteConsole.WriteLine("[ERROR_START_AUTH]: Não foi possível se conectar ao AuthServer");
            //    Console.ReadKey();
            //    Environment.Exit(1);
            //}
        }
        public TcpServer()
        {
            Players = new GenericDisposableCollection<Player>();
            MsgBufferRead = new byte[500000];           
        }
        #endregion

        #region Methods
        public void Start()
        {
            if (!IsRunning)
            {
                Adress = IPAddress.Parse(Data.IP);
                Port = (int)Data.Port;
                m_Listener = new TcpListener(Adress, Port);
                m_Listener.Server.SendBufferSize = 1024 * 8;
                m_Listener.Server.ReceiveBufferSize = 1024 * 8;
                m_Listener.Server.NoDelay = true;
                m_Listener.AllowNatTraversal(true);
                IsRunning = true;
                m_Listener.Start();
                m_Listener.BeginAcceptTcpClient(HandleTcpClientAccepted, m_Listener);
            }
        }

        public void Start(int backlog)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                m_Listener.Start(backlog);
                m_Listener.BeginAcceptTcpClient(HandleTcpClientAccepted, m_Listener);
            }
        }


        public void Stop()
        {
            if (IsRunning)
            {
                IsRunning = false;
                m_Listener.Stop();
                lock (Players)
                {
                    CloseAllClient();
                }
            }
        }

        private void HandleTcpClientAccepted(IAsyncResult ar)
        {
            if (IsRunning)
            {
                var _Client = m_Listener.EndAcceptTcpClient(ar);
                if (ClientCount > 10)
                {
                    //TODO
                    _Client.Close();
                    return;
                }


                var _Session = OnConnectPlayer(_Client);

                lock (Players)
                {
                    ClientCount++;
                    Players.Add(_Session);
                    OnClientConnected(_Session);
                }

                if (_Session.Tcp.Connected)
                {
                    var _Stream = _Session.NetworkStream;

                    _Stream.BeginRead(MsgBufferRead, 0, MsgBufferRead.Length, HandleDataReceived, _Session);

                    m_Listener.BeginAcceptTcpClient(HandleTcpClientAccepted, ar.AsyncState);
                }
            }
        }

        private void HandleDataReceived(IAsyncResult ar)
        {
            int bytesRead = 0;
            byte[] message = new byte[0];
            if (IsRunning && ((Player)ar.AsyncState).Tcp.Connected)
            {
                var _Session = (Player)ar.AsyncState;
                var _Stream = _Session.NetworkStream;
                try
                {
                    bytesRead = _Stream.EndRead(ar);
                    if (bytesRead >= 5)
                    {
                        //variável para armazenar a mensagem recebida
                        message = new byte[bytesRead];

                        //Copia mensagem recebida
                        Buffer.BlockCopy(MsgBufferRead, 0, message, 0, bytesRead);

                        //ler o proximo pacote(se houver)
                        _Stream.BeginRead(MsgBufferRead, 0, MsgBufferRead.Length, HandleDataReceived, _Session);

                        //checa se o tamanho da mensagem é zerada
                        var packet = new Packet(message, _Session.Key);
                        //Dispara evento OnPacketReceived
                        PlayerRequestPacket(_Session, packet);
                    }
                    else
                    {
                        Players.Remove(_Session);
                        OnClientDisconnected?.Invoke(_Session);
                        DisconnectPlayer(_Session);
                    }


                }
                catch
                {
                    lock (Players)
                    {
                        Players.Remove(_Session);

                        OnClientDisconnected(_Session);
                        return;
                    }
                }
            }
        }

        protected void PlayerRequestPacket(Player player, Packet packet)
        {
            WriteConsole.WriteLine($"[PLAYER_PACKET_LOG]: {player.GetAdress}:{player.GetPort}", ConsoleColor.Green);
            packet.Log();
            OnPacketReceived?.Invoke(player, packet);
        }

        private void PlayerClientDisconnected(Player session)
        {
            ClientCount--;
            session.Dispose();
            OnClientDisconnected?.Invoke(session);
        }

        public void OnClientKeyConnection(Player session)
        {
            OnClientKey?.Invoke(session);
        }

        public void Close(Player pSession, bool pRemove = true)
        {
            if (pSession != null)
            {
                pSession.Close();
                if (pRemove)
                    Players.Remove(pSession);
                OnClientDisconnected(pSession);
            }
        }

        public void CloseAllClient()
        {
            foreach (var _Session in Players.Model)
                Close(_Session, false);
            ClientCount = 0;
            Players.Model.Clear();
        }

        /// <summary>
        /// Conecta-se ao AuthServer
        /// </summary>
        public bool ConnectToAuthServer(ClientAuth client)
        {
            AuthServer = client;
            AuthServer.OnDisconnect += OnAuthServerDisconnected;
            AuthServer.OnPacketReceived += AuthServer_OnPacketReceived;
            return AuthServer.Connect();
        }

        /// <summary>
        /// É Disparado quando um packet é recebido do AuthServer
        /// </summary>
        private void AuthServer_OnPacketReceived(ClientAuth authClient, AuthPacketInfo packet)
        {
            OnAuthServerPacketReceive(authClient, packet);
        }

        /// <summary>
        /// É disparado quando não há conexão com o AuthServer
        /// </summary>
        private void OnAuthServerDisconnected()
        {
            Console.WriteLine("Servidor parado.");
            Console.WriteLine("Não foi possível estabelecer conexão com o authServer!");
            Console.ReadKey();
            Environment.Exit(1);
        }

        #endregion  

        public void SendToAll(byte[] Data)
        {
            for (int i = 0; i < Players.Count; i++)
            {
                Players[i].SendResponse(Data);
            }
        }

        public void Notice(string message)
        {
            var response = new PangyaBinaryWriter(new MemoryStream());

            response.Write(new byte[] { 0x42, 0x00 });
            response.WritePStr($"[NOTICE]: {message}");
            SendToAll(response.GetBytes());
            WriteConsole.WriteLine($"[NOTICE]: {message}");
        }

        public void ShowHelp()
        {
            Console.WriteLine(Environment.NewLine);
            WriteConsole.WriteLine("Welcome To Pangya-Server!" + Environment.NewLine);

            Console.WriteLine("See available console commands:" + Environment.NewLine);

            Console.WriteLine("help      | Displays console commands");
            Console.WriteLine("topnotice | Displays message to users who are playing Game");
            Console.WriteLine("kickuser  | Disconnect by UserName");// kickuid luismk
            Console.WriteLine("kicknick  | Disconnect by Nick");// kickuid MK(e48)
            Console.WriteLine("kickuid   | Disconnect by UID");// kickuid 1 
            Console.WriteLine("clear     | Clear Console");
            Console.WriteLine("cls       | Clear console");
            Console.WriteLine("quit      | Close By Server");
            Console.WriteLine("Start     | Open Server");//opens the server if it is undergoing maintenance
            Console.WriteLine("Stop      | Close Server");//closes the server for maintenance

            Console.WriteLine(Environment.NewLine);
        }

        public void UpdateServer()
        {
            var db = new PangyaEntities();
            var query = $"UPDATE [dbo].[Pangya_Server] Set UsersOnline = '{Players.Count}' where ServerID = '{Data.UID}'";

            db.Database.SqlQuery<PangyaEntities>(query).FirstOrDefault();
        }

    }
}
