using Pangya_GameServer.GamePlayer;
using Pangya_GameServer.PlayerLobby.Common;
using PangyaAPI.Helper.BinaryModels;
using PangyaAPI.Helper.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pangya_GameServer.PlayerLobby.Collection
{
    public class LobbyList : List<Lobby>
    {
        public LobbyList(IniFile Ini)
        {
            byte i;
            try
            {
                var LobbyCount = Ini.ReadByte("Lobby", "LobbyCount", 0);
                for (i = 1; i <= LobbyCount; i++)
                {
                    var lobby = new Lobby(new LobbyInfo
                    {
                        Name = Ini.ReadString("Lobby", $"LobbyName_{i}", $"#Lobby {i}"),
                        Unknown0 = new byte[] { 0x00, 0x01, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x10, 0x06, 0x07, 0x1A, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x14, 0x00, 0x00, 0x64, 0x02, 0x00, 0x1A, 0x00, 0x00, 0x00, 0x00 },
                        MaxPlayers = Ini.ReadUInt16("Lobby", $"LobbyMaxUser_{i}", 100),
                        Id = Ini.ReadByte("Lobby", $"LobbyID_{i}", i),
                        Flag = Ini.ReadUInt32("Lobby", $"LobbyFlag_{i}", 0)
                    });
                    Add(lobby);
                }
            }
            catch
            { }
            finally
            {
                if (Ini != null)
                    Ini.Dispose();
            }
            WriteConsole.WriteLine("[SERVER_SYSTEM_CHANNEL]: Canais foram carregados !", ConsoleColor.Green);
        }

        public byte[] Build(bool CreateLobbyList = false)
        {
            var resp = new PangyaBinaryWriter();
            if (CreateLobbyList)
            {
                resp.Write(new byte[] { 0x4D, 0x00 });
            }
            resp.WriteByte(Count);
            for (int i = 0; i < Count; i++)
            {
                resp.WriteStruct(this[i].Info);
            }
            return resp.GetBytes();
        }


        public void HandleRemoveLobbyPlayer(GPlayer player)
        {
            this.FirstOrDefault(l => l.Players.ToList().Any(p => p?.GetUID == player.GetUID))?.Players?.ToList().Remove(player);
        }
        public void DestroyLobbies()
        {
            this.Clear();
        }

        public void ShowLobby()
        {
            foreach (var lobby in this)
            {
                WriteConsole.WriteLine($"[SHOW_LOBBY_INFO]: LobbyPlayers [{lobby.Players.Count}/{lobby.Info.MaxPlayers}] LobbyID [{lobby.Info.Id}] LobbyName [{lobby.Info.Name}]", ConsoleColor.Green);
            }
        }

        public Lobby GetLobby(Lobby lobby)
        {
            foreach (var result in this)
            {
                if (result.Info.Id == lobby.Info.Id)
                {
                    return result;
                }
            }
            return null;
        }

        public Lobby GetLobby(byte LobbyID)
        {
            foreach (var result in this)
            {
                if (result.Info.Id == LobbyID)
                {
                    return result;
                }
            }
            return null;
        }
    }
}
