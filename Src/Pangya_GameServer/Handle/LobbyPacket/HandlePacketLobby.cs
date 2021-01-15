using Pangya_GameServer.Flags;
using Pangya_GameServer.Game.Model;
using Pangya_GameServer.GamePlayer;
using Pangya_GameServer.Handle.GamePacket;
using Pangya_GameServer.PacketCreator;
using PangyaAPI.Helper.BinaryModels;
using PangyaAPI.PangyaPacket;
using System;

namespace Pangya_GameServer.Handle.LobbyPacket
{
    public static class HandlePacketLobby
    {
        public static void PacketLobby(GamePacketFlag ID, GPlayer player, Packet packetReader)
        {
			var PLobby = player.Lobby;
			GameBase PlayerGame;
			switch (ID)
            {
				case GamePacketFlag.PLAYER_MATCH_HISTORY:
					{
						player.Response.Write(new byte[] { 0x0E, 0x01 });
						player.Response.WriteZero(260); //260 bytes
						player.SendResponse();

						if (player.GameID == ushort.MaxValue)
						{
							player.SendResponse(new byte[] { 0x2E, 0x02, 0x00, 0x00, 0x00, 0x00 });
							player.SendResponse(new byte[] { 0x20, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
						}
					}
					break;
				case GamePacketFlag.PLAYER_CHAT:
					{
					}
					break;
				case GamePacketFlag.PLAYER_WHISPER:
					{
					}
					break;
				case GamePacketFlag.PLAYER_CREATE_GAME:
					{
					}
					break;
				case GamePacketFlag.PLAYER_JOIN_GAME:
					{
					}
					break;
				case GamePacketFlag.PLAYER_GM_ENTER_ROOM:
					{
					}
					break;
				case GamePacketFlag.PLAYER_BUY_ITEM_GAME:
					{
					}
					break;
				case GamePacketFlag.PLAYER_GM_IDENTITY:
					{
						//this.HandlePlayerRequestIdentity(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_REQUEST_LOBBY_INFO:
					{
						//this.HandlePlayerRequestServerList(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_UPGRADE_CLUB_SLOT:
					{
						//this.HandlePlayerUpgrade(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_GM_SEND_NOTICE:
					{
						//this.HandlePlayerNotice(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_JOIN_MULTIGAME_GRANDPRIX:
					{
						//this.HandlePlayerEnterGrandPrix(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_LEAVE_MULTIGAME_GRANDPRIX:
					{
						//this.HandlePlayerLeaveGrandPrix(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_JOIN_MULTIGAME_LIST:
					{
						HandlePlayerJoinMultiplayerGamesList(player);
					}
					break;
				case GamePacketFlag.PLAYER_LEAVE_MULTIGAME_LIST:
					{
						HandlePlayerLeaveMultiplayerGamesList(player);
					}
					break;
				case GamePacketFlag.PLAYER_REQUEST_MESSENGER_LIST:
					{
						//this.HandlePlayerRequestMessengerList(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_GM_COMMAND:
					{
						//this.HandlePlayerGMCommand(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_OPEN_PAPEL:
					{
						//this.m_bongdaryShop.HandlePlayerOpenRareShop(player);
					}
					break;
				case GamePacketFlag.PLAYER_ENTER_ROOM:
					{
						//this.HandlePlayerUnknow00EB(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_OPENUP_SCRATCHCARD:
					{
					}
					break;
				case GamePacketFlag.PLAYER_ENTER_TO_SHOP:
					{
					}
					break;
				case GamePacketFlag.PLAYER_REQUEST_PLAYERINFO:
					{
						//this.HandlePlayerRequestInfo(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_OPEN_NORMAL_BONGDARI:
					{
					}
					break;
				case GamePacketFlag.PLAYER_CALL_ACHIEVEMENT:
					{
					}
					break;
				case GamePacketFlag.PLAYER_PLAY_SCRATCHCARD:
					{
					}
					break;
				case GamePacketFlag.PLAYER_ENTER_SCRATCHY_SERIAL:
					{
					}
					break;
				case GamePacketFlag.PLAYER_LOAD_QUEST:
					{
						//this.HandlePlayerRequestDailyQuest(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_RECYCLE_ITEM:
					{
						//this.HandlePlayerRecycleItem(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_ACCEPT_QUEST:
					{
						//this.HandlePlayerAcceptDailyQuest(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_GIVEUP_DAILY_QUEST:
					{
						//this.HandlePlayerGiveUpDailyQuest(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_SEND_INVITE:
					{
						//this.HandlePlayerSendInvite(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_REQUEST_CHECK_DAILY_ITEM:
					{
						//this.HandlePlayerRequestDailyReward(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_REQUEST_COOKIES_COUNT:
					{
						//this.HandlePlayerRequestCookiesCount(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_OPEN_MAILBOX:
					{
						//this.HandlePlayerRequestInbox(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_READ_MAIL:
					{
						//this.HandlePlayerRequestInboxDetails(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_RELEASE_MAILITEM:
					{
						//this.HandlePlayerMoveInboxGift(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_REQUEST_OFFLINE_PLAYER_INFO:
					{
						//this.HandlePlayerRequestOfflinePlayerInfo(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_SEND_MAIL:
					{
						//this.HandlerPlayerSendMail(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_DELETE_MAIL:
					{
						//this.HandlerPlayerDeleteMail(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_CLEAR_QUEST:
					{
						//this.HandlerPlayerClearQuest(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_CHANGE_MASCOT_MESSAGE:
					{
						//this.HandlePlayerSetMascotText(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_ENTER_TO_LOCKER:
					{
						//this.HandlePlayerRequestLocker(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_OPEN_LOCKER:
					{
						//this.HandlerPlayerRequestLockerAccess(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_CHANGE_LOCKERPWD:
					{
						//this.HandlePlayerChangeLockerPassword(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_GET_LOCKERPANG:
					{
						//this.HandlePlayerRequestLockerPangs(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_CALL_LOCKERITEMLIST:
					{
						//this.HandlePlayerRequestLockerPage(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_LOCKERPANG_CONTROL:
					{
						//this.HandlerPlayerPangsTransaction(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_REMOVE_ITEM:
					{
						//this.HandlePlayerDeleteItem(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_ENTER_GRANDPRIX:
					{
						//lobby.HandlePlayerEnterGrandPrixEvent(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_ASSIST_CONTROL:
					{
						//this.HandlePlayerSetAssistMode(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_CALL_GUILD_LIST:
					{
						//this.HandlePlayerRequestGuildList(player, packetReader);
					}
					break;
				case GamePacketFlag.PLAYER_CREATE_GUILD:
					{
					}
					break;
				case GamePacketFlag.PLAYER_GUILD_AVAIABLE:
					{
					}
					break;
				case GamePacketFlag.PLAYER_JOIN_GUILD:
					{
					}
					break;
				case GamePacketFlag.PLAYER_MEMORIAL:
					{
					}
					break;
				case GamePacketFlag.PLAYER_SEARCH_GUILD:
					{
					}
					break;
				case GamePacketFlag.PLAYER_UPGRADE_STATUS:
					{
					}
					break;
				default:
					{
						if (PLobby == null)
						{
							player.Send(Creator.ShowEnterLobby(2));
							return;
						}

						if (player.GameID == ushort.MaxValue)
						{
							return;
						}

						PlayerGame = PLobby[player.GameID];

						if (PlayerGame == null)
						{
							player.Send(Creator.ShowRoomError(GameCreateResultFlag.CREATE_GAME_CREATE_FAILED2));
							return;
						}
						HandlePacketGame.PacketGame(PlayerGame, ID, player, packetReader);
					}
                    break;
            }
        }

        private static void HandlePlayerLeaveMultiplayerGamesList(GPlayer player)
        {
			var LobbyPlayer = player.Lobby;

			if (LobbyPlayer == null)
				return;

			LobbyPlayer.LeaveMultiplayerGamesList(player);
		}

        private static void HandlePlayerJoinMultiplayerGamesList(GPlayer player)
        {
			var LobbyPlayer = player.Lobby;

			if (LobbyPlayer == null)
				return;

			LobbyPlayer.JoinMultiplayerGamesList(player);
		}

        static void PlayerSelectLobby(GPlayer player)
		{
			DailyLoginCheck(player, 0);
		}

		public static void DailyLoginCheck(this GPlayer session, byte Code)
		{
			using (var Packet = new PangyaBinaryWriter())
			{
				Packet.Write(new byte[] { 0x47, 0x02 });
				Packet.WriteUInt32(0);
				Packet.WriteByte(Code);//code
				Packet.WriteInt32(134219780);//item id
				Packet.WriteInt32(1);//quantidade antiga
				Packet.WriteInt32(134219785);
				Packet.WriteInt32(1);//qtd_next
				Packet.WriteInt32(10);//dias 
				session.SendResponse(Packet.GetBytes());
				Packet.Clear();
			}
		}

		public static void DailyLoginItem(this GPlayer session)
		{
			using (var Packet = new PangyaBinaryWriter())
			{
				Packet.Write(new byte[] { 0x47, 0x02 });//48
				Packet.WriteUInt32(0);
				Packet.WriteByte(1);//code
				Packet.WriteInt32(134219780);//item id
				Packet.WriteInt32(1);//quantidade antiga
				Packet.WriteInt32(134219785);
				Packet.WriteInt32(1);//qtd_next
				Packet.WriteInt32(10);//dias 
				session.SendResponse(Packet.GetBytes());
			}
		}

		/// <summary>
		/// Player Enter main lobby
		/// </summary>
		/// <param name="session"></param>
		public static void HandleJoinMultiPlayerGameList(this GPlayer session)
		{
			var lobby = session.Lobby;

			if (lobby == null) return;

			lobby.JoinMultiplayerGamesList(session);
		}

		public static void HandleLeaveMultiPlayerGameList(this GPlayer session)
		{
			var lobby = session.Lobby;

			if (lobby == null) return;

			lobby.LeaveMultiplayerGamesList(session);
		}
	}
}
