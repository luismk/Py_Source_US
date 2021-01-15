using Pangya_GameServer.Common;
using Pangya_GameServer.Flags;
using Pangya_GameServer.GamePlayer;
using Pangya_GameServer.PacketCreator;
using PangyaAPI.Helper.BinaryModels;
using PangyaAPI.PangyaPacket;
using PangyaAPI.Helper.Tools;
using System;
using PangyaAPI.PangyaClient.Data;
using PangyaAPI.Helper;
namespace Pangya_GameServer.Game.Model
{
    public partial class GameBase
    {
        #region Handle Reader Packet_Game
        public void HandlePacket(GamePacketFlag ID, GPlayer player, Packet packet)
        {
            switch (ID)
            {
                case GamePacketFlag.PLAYER_SEND_INVITE_CONFIRM:
                    {

                    }
                    break;
                case GamePacketFlag.PLAYER_USE_ITEM:
                    {
                        PlayerUseItem(player, packet);
                    }
                    break;
                case GamePacketFlag.PLAYER_PRESS_READY:
                    {
                        PlayerGameReady(player, packet);
                    }
                    break;
                case GamePacketFlag.PLAYER_START_GAME:
                    {
                        PlayerStartGame();
                    }
                    break;
                case GamePacketFlag.PLAYER_LOAD_OK:
                    {
                        PlayerLoadSuccess(player);
                    }
                    break;
                case GamePacketFlag.PLAYER_SHOT_DATA:
                    {
                        PlayerShotData(player, packet);
                    }
                    break;
                case GamePacketFlag.PLAYER_ENTER_TO_ROOM:
                    {
                        PlayerEnterToRoom(player, packet);
                    }
                    break;
                case GamePacketFlag.PLAYER_ACTION:
                    {
                        PlayerAction(player, packet);
                    }
                    break;
                case GamePacketFlag.PLAYER_CLOSE_SHOP:
                case GamePacketFlag.PLAYER_OPEN_SHOP:
                case GamePacketFlag.PLAYER_ENTER_SHOP:
                case GamePacketFlag.PLAYER_EDIT_SHOP_NAME:
                case GamePacketFlag.PLAYER_SHOP_ITEMS:
                case GamePacketFlag.PLAYER_BUY_SHOP_ITEM:
                case GamePacketFlag.PLAYER_SHOP_CREATE_VISITORS_COUNT:
                case GamePacketFlag.PLAYER_SHOP_PANGS:
                    {
                        WriteConsole.Warning("HANDLE PACKET NO FOUND");
                    }
                    break;
                case GamePacketFlag.PLAYER_CHANGE_GAME_OPTION:
                    {
                        PlayerGameSetting(packet);
                    }
                    break;
                case GamePacketFlag.PLAYER_1ST_SHOT_READY:
                    {
                        if (FirstShot == false && player.GameInfo.Role == PlayerTypeFlag.Master)
                        {
                            PlayerFirstShotReady();
                        }
                    }
                    break;
                case GamePacketFlag.PLAYER_LOADING_INFO:
                    {
                        PlayerLoading(player, packet);
                    }
                    break;
                case GamePacketFlag.PLAYER_GAME_ROTATE:
                    {
                        PlayerGameRotate(player, packet);
                    }
                    break;
                case GamePacketFlag.PLAYER_CHANGE_CLUB:
                    {
                        PlayerChangeClub(player, packet);
                    }
                    break;
                case GamePacketFlag.PLAYER_GAME_MARK:
                    {
                    }
                    break;
                case GamePacketFlag.PLAYER_ACTION_SHOT:
                    {
                        PlayerShotInfo(player, packet);
                    }
                    break;
                case GamePacketFlag.PLAYER_SHOT_SYNC:
                    {
                        PlayerSyncShot(player, packet);
                    }
                    break;
                case GamePacketFlag.PLAYER_HOLE_INFORMATIONS:
                    {
                        PlayerHoleData(player, packet);
                    }
                    break;
                case GamePacketFlag.PLAYER_REQUEST_ANIMALHAND_EFFECT:
                    {
                        PlayerAnimalEffect(player);
                    }
                    break;
                case GamePacketFlag.PLAYER_MY_TURN:
                    {
                    }
                    break;
                case GamePacketFlag.PLAYER_HOLE_COMPLETE:
                    {
                        PlayerSendResult(player, packet);
                    }
                    break;
                case GamePacketFlag.PLAYER_CHAT_ICON:
                    {
                        PlayerChatIcon(player, packet);
                    }
                    break;
                case GamePacketFlag.PLAYER_SLEEP_ICON:
                    {
                        PlayerSleepIcon(player, packet);
                    }
                    break;
                case GamePacketFlag.PLAYER_MATCH_DATA:
                    {
                        PlayerMatchData(player, packet);
                    }
                    break;
                case GamePacketFlag.PLAYER_MOVE_BAR:
                    {

                    }
                    break;
                case GamePacketFlag.PLAYER_PAUSE_GAME:
                    {
                        PlayerGamePause(player, packet);
                    }
                    break;
                case GamePacketFlag.PLAYER_QUIT_SINGLE_PLAYER:
                    {
                        PlayerLeavePractice();
                    }
                    break;
                case GamePacketFlag.PLAYER_CALL_ASSIST_PUTTING:
                    {
                        PlayerPutt(player, packet);
                    }
                    break;
                case GamePacketFlag.PLAYER_USE_TIMEBOOSTER:
                    {
                        PlayerUseBooster(player);
                    }
                    break;
                case GamePacketFlag.PLAYER_DROP_BALL:
                    {
                        PlayerDropBall(packet);
                    }
                    break;
                case GamePacketFlag.PLAYER_CHANGE_TEAM:
                    {
                    }
                    break;
                case GamePacketFlag.PLAYER_VERSUS_TEAM_SCORE:
                    {
                    }
                    break;
                case GamePacketFlag.PLAYER_POWER_SHOT:
                    {
                        PlayerUsePowerShot(player, packet);
                    }
                    break;
                case GamePacketFlag.PLAYER_WIND_CHANGE:
                    {
                        Holes.RebuildGameHole(fGameData.Map);
                    }
                    break;
                case GamePacketFlag.PLAYER_SEND_GAMERESULT:
                    {
                        PlayerSendFinalResult(player, packet);
                    }
                    break;
                case GamePacketFlag.PLAYER_REQUEST_RING_EFFECTS:
                    {
                        PlayerUseRingEffects(player, packet);
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion


        #region Handle Response Packet_Game
        protected void GoToNextHole()
        {
            if (Holes.GoToNext())
            {
                Send(new byte[] { 0x65, 0x00 });
            }
            else
            {
                SendGameResult();
            }
        }

        protected void SendGameResult()
        {
            var result = new PangyaBinaryWriter();

            result.Write(new byte[] { 0x65, 0x00 });
            result.Write(Count);
            foreach (var player in Players)
            {
                result.Write(player.ConnectionID);
                result.Write(player.GameInfo.SlotNumber);
                result.Write((byte)2); // total point
                result.Write((byte)5); // course shot count
                result.Write((ushort)player.GameInfo.ScoreData.EXP); // player xp
                result.Write(0x67000000);//pang
                result.Write(0);//un
                result.Write(0xD2000000);//bonus pang
                result.Write(0);//un2
                result.Write(0);//un3
                result.Write(0);//un4
            }
            Send(result);
            Started = false;
        }

        protected void SendWind()
        {
            var Weather = Holes.CurrentHole;
            Send(Creator.ShowWind(Weather.WindPower, Weather.WindDirection));
        }

        protected void SendWeather()
        {
            var Weather = Holes.CurrentHole;
            Send(Creator.ShowWeather(Weather.Weather));
        }

        protected void SendMatchData(GPlayer player)
        {
            player.Response.Write(new byte[] { 0x79, 0x00 });
            player.Response.Write(player.GameInfo.ScoreData.EXP); //XP
            player.Response.Write(Trophy);
            player.Response.Write(new byte[] { 0x00, 0x02 });
            player.Response.Write(LuckyAward);//luck
            player.Response.Write(0);
            player.Response.Write(BestSpeeder);//bestspeed
            player.Response.Write(0);
            player.Response.Write(BestDrive);//bestdrive
            player.Response.Write(0);
            player.Response.Write(BestChipIn);//bestchipIn
            player.Response.Write(0);
            player.Response.Write(LongestPutt);//LongPutt
            player.Response.Write(0);
            player.Response.Write(BestRecovery);//BestRecovery
            player.Response.Write(0);
            player.Response.Write(Gold);//trophy-Gold
            player.Response.Write(0);
            player.Response.Write(Silver1);//trophy-silve1
            player.Response.Write(0);
            player.Response.Write(Silver2);//trophy-silve2
            player.Response.Write(0);
            player.Response.Write(Bronze1);//trophy-bronze1
            player.Response.Write(0);
            player.Response.Write(Bronze2);//trophy-bronze2
            player.Response.Write(0);
            player.Response.Write(Bronze3);//trophy-bronze3
            player.SendResponse();
        }

        protected void PlayerGamePause(GPlayer player, Packet packet)
        {
            var action = packet.ReadByte();

            if (GameType == GameTypeFlag.VERSUS_STROKE)
            {
                Send(Creator.ShowPlayerPauseGame(player.ConnectionID, action));
            }
            else
            {
                player.SendResponse(Creator.ShowPlayerPauseGame(player.ConnectionID, action));
            }
        }

        protected void PlayerGameRotate(GPlayer player, Packet packet)
        {
            var Angle = packet.ReadSingle();

            if (GameType == GameTypeFlag.VERSUS_STROKE)
            {
                Send(Creator.ShowPlayerRotate(player.ConnectionID, Angle));
            }
            else
            {
                player.SendResponse(Creator.ShowPlayerRotate(player.ConnectionID, Angle));
            }
        }

        protected void PlayerChangeClub(GPlayer player, Packet packet)
        {
            var ClubType = packet.ReadByte();

            if (GameType == GameTypeFlag.VERSUS_STROKE)
            {
                Send(Creator.ShowPlayerChangeClub(player.ConnectionID, ClubType));
            }
            else
            {
                player.SendResponse(Creator.ShowPlayerChangeClub(player.ConnectionID, ClubType));
            }
        }

        protected void PlayerDropBall(Packet packet)
        {
            var Pos = new Point3D();

            Pos = (Point3D)packet.Read(Pos);

            Send(Creator.ShowDropBall(Pos));
        }

        protected void PlayerUsePowerShot(GPlayer player, Packet packet)
        {
            var active = (PowerShotFlag)packet.ReadByte();

            Send(Creator.ShowPowerShot(player.ConnectionID, active));
        }

        protected void PlayerUseRingEffects(GPlayer player, Packet packet)
        {
            Send(Creator.ShowRingEffects(player.ConnectionID, packet.GetRemainingData));
        }

        protected void PlayerMatchData(GPlayer player, Packet packet)
        {
            var Response = new PangyaBinaryWriter();
            Response.Write(new byte[] { 0xF7, 0x01 });
            Response.Write(player.ConnectionID);
            Response.Write((byte)player.GameInfo.HolePos);
            Response.Write(packet.ReadBytes(87));
            Send(Response.GetBytes());
        }

        protected void PlayerPutt(GPlayer player, Packet packet)
        {
            uint TypeID = packet.ReadUInt32();

            if (!(TypeID == 0x1BE00016))
            {
                return;
            }
            player.SendResponse(Creator.ShowAssistPutting(TypeID, player.GetUID));
        }

        protected void PlayerSendResult(GPlayer player, Packet packet)
        {
            var Statistics = packet.Read<PlayerStatistic>();

            player.GameInfo.ScoreData.Statistic = Statistics;
            player.GameInfo.ScoreData.HoleComplete = true;
            HoleComplete = true;
        }

        protected void PlayerGameReady(GPlayer player, Packet packet)
        {
            var S = packet.ReadByte();

            player.GameInfo.ReadyToPlay = S > 0;

            Send(Creator.ShowGameReady(player.ConnectionID, S));
        }

        protected void PlayerUseItem(GPlayer PL, Packet packet)
        {
            var TypeID = packet.ReadUInt32();

            PL.SendResponse(Creator.ShowPlayerUseItem(PL.ConnectionID, TypeID));
        }

        protected void PlayerUseBooster(GPlayer player)
        {
            if (GameType == GameTypeFlag.VERSUS_STROKE)
            {
                Send(Creator.ShowPlayerTimeBoost(player.ConnectionID));
            }
            else
            {
                player.SendResponse(Creator.ShowPlayerTimeBoost(player.ConnectionID));
            }
        }

        protected void PlayerChatIcon(GPlayer player, Packet packet)
        {
            var IconType = packet.ReadUInt16();

            Send(Creator.ShowGameIcon(player.ConnectionID, IconType));
        }

        protected void PlayerSleepIcon(GPlayer player, Packet packet)
        {
            var I = packet.ReadByte();

            Write(Creator.ShowSleep(player.ConnectionID, I));

            PlayerJoin(this, player);
        }

        protected void PlayerAnimalEffect(GPlayer player)
        {
            Send(Creator.ShowAnimalEffect(player.ConnectionID));
        }

        protected void PlayerHoleData(GPlayer PL, Packet packet)
        {
            var H = (HoleData)packet.Read(new HoleData());

            CurrentHole = new Point3D()
            {
                X = H.X,
                Z = H.Z
            };

            PL.GameInfo.HolePos3D.X = H.X;
            PL.GameInfo.HolePos3D.Z = H.Z;
            PL.GameInfo.HolePos = H.HolePosition;
            PL.GameInfo.ScoreData.ParCount = (sbyte)H.Par;
            if (!((fGameData.NaturalMode & 2) == 0))
            {
                switch (H.Par)
                {
                    case 4:
                        PL.GameInfo.ScoreData.ShotCount = 2;
                        break;
                    case 5:
                        PL.GameInfo.ScoreData.ShotCount = 3;
                        break;
                    default:
                        {
                            PL.GameInfo.ScoreData.ShotCount = 1;
                        }
                        break;
                }
            }
            else
            {
                PL.GameInfo.ScoreData.ShotCount = 1;
            }
            SendHoleData(PL);
        }

        protected void PlayerGameSetting(Packet packet)
        {
            packet.Skip(2);
            packet.ReadByte(out byte count); //contagem 
            for (int i = 1; i <= count; ++i)
            {
                packet.ReadByte(out byte action);//action enum

                switch ((GameShift)action)
                {
                    case GameShift.SHIFT_NAME: //name
                        {
                            packet.ReadPStr(out string NAME);
                            if (NAME.Length > 0)
                                fGameData.Name = NAME;
                        }
                        break;
                    case GameShift.SHIFT_PWD: //senha
                        {
                            packet.ReadPStr(out string PWD);
                            if (PWD.Length > 0)
                                fGameData.Password = PWD;
                        }
                        break;
                    case GameShift.SHIFT_STROK: //game_type
                        {
                            packet.ReadByte(out byte Game_Type);

                            fGameData.GameType = (GameTypeFlag)Game_Type;
                        }
                        break;
                    case GameShift.SHIFT_MAP: //MAP
                        {
                            fGameData.Map = (GameMapFlag)packet.ReadByte();
                        }
                        break;
                    case GameShift.SHIFT_NUMHOLE: //hole total
                        {
                            fGameData.HoleTotal = (byte)packet.ReadByte();
                        }
                        break;
                    case GameShift.SHIFT_MODE: //mode
                        {
                            fGameData.Mode = (GameModeFlag)packet.ReadByte();
                        }
                        break;
                    case GameShift.SHIFT_VSTIME: //vs_time = turntime
                        {
                            packet.ReadByte(out byte VSTIME);

                            fGameData.VSTime = (uint)VSTIME * 1000;
                        }
                        break;
                    case GameShift.SHIFT_MAXPLAYER: //max_player
                        {
                            packet.ReadByte(out byte MaxPlayer);

                            if (MaxPlayer <= Players.Count)
                                fGameData.MaxPlayers = MaxPlayer;
                        }
                        break;
                    case GameShift.SHIFT_MATCHTIME: //Match_time = game time
                        {
                            packet.ReadByte(out byte GameTime);

                            fGameData.GameTime = (uint)(60 * GameTime) * 1000;
                        }
                        break;
                    case GameShift.SHIFT_IDLE: //Idle
                        {
                            packet.ReadByte(out Idle);
                        }
                        break;
                    case GameShift.SHIFT_HOLENUM: //HoleNumber
                        {
                            packet.ReadByte(out HoleNumber);
                        }
                        break;
                    case GameShift.SHIFT_HOLELOCK: //LockHole
                        {
                            packet.ReadUInt32(out LockHole);
                        }
                        break;
                    case GameShift.SHIFT_NATURAL: //natural mode
                        {
                            packet.ReadUInt32(out uint NaturalMode);

                            fGameData.NaturalMode = NaturalMode;
                        }
                        break;
                    default:
                        WriteConsole.WriteLine($"[PlayerGameSetting]: Unknown Setting type: [{action}] Count > {count} ");
                        break;
                }
            }
            GameUpdate();
            Update(this);
        }

        protected void PlayerFirstShotReady()
        {
            Send(Creator.ShowFirstShotReady());
            FirstShot = true;
        }

        protected void PlayerEnterToRoom(GPlayer player, Packet packet)
        {
            var ConID = packet.ReadUInt32();
            Console.WriteLine("[PLAYER_TO_ROOM] => " + ConID);
            player.Send(Creator.ShowRoomEntrance(ConID));
        }

        protected void PlayerAction(GPlayer player, Packet packet)
        {

            try
            {
                if (!packet.ReadByte(out byte Action))
                {
                    return;
                }

                switch ((ActionFlag)Action)
                {
                    case ActionFlag.PLAYER_ACTION_ROTATION:
                        {
                        }
                        break;
                    case ActionFlag.PLAYER_ACTION_APPEAR:
                        {
                            player.GameInfo.Action.Vector = (Point3D)packet.Read(new Point3D());
                        }
                        break;
                    case ActionFlag.PLAYER_ACTION_SUB:
                        {
                            if (!packet.ReadUInt32(out player.GameInfo.Action.Posture))
                            {
                                return;
                            }
                        }
                        break;
                    case ActionFlag.PLAYER_ACTION_WALK:
                        {
                            var move = new Point3D();
                            player.GameInfo.AddWalk((Point3D)packet.Read(move));
                            Console.WriteLine("[PLAYER_MOVE] => " + player.GameInfo.Action.Distance(move));
                        }
                        break;
                    case ActionFlag.PLAYER_ACTION_ANIMATION:
                        {
                            if (!packet.ReadPStr(out player.GameInfo.Action.Animation))
                            {
                                return;
                            }

                            Console.WriteLine("[PLAYER_ACTION_ANIMATION] => " + player.GameInfo.Action.Animation);

                        }
                        break;
                    case ActionFlag.PLAYER_ACTION_HEADER:
                        {
                            if (!packet.ReadUInt32(out player.GameInfo.Action.Animate))
                            {
                                return;
                            }
                        }
                        break;

                    case ActionFlag.PLAYER_ANIMATION_WITH_EFFECTS:
                        {
                            if (!packet.ReadPStr(out player.GameInfo.Action.Animation))
                            {
                                return;
                            }

                            Console.WriteLine("[PLAYER_ANIMATION_WITH_EFFECTS] => " + player.GameInfo.Action.Animation);
                        }
                        break;
                    case ActionFlag.PLAYER_ACTION_NULL:
                    case ActionFlag.PLAYER_ACTION_UNK:
                    default:
                        {
                            packet.Log();
                        }
                        break;
                }

            }
            finally
            {
            }
            Send(Creator.ShowPlayerAction(player.ConnectionID, packet.GetRemainingData));
        }
        #endregion
    }
}
