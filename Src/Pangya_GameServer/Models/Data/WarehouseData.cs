using Pangya_GameServer.Common;
using PangyaAPI.Helper.BinaryModels;
using PangyaAPI.Helper.Tools;
using PangyaAPI.IFF;
using PangyaAPI.IFF.Flags;
using PangyaAPI.IFF.Tools;
using PangyaAPI.PangyaClient.Data;
using PangyaAPI.SqlConnector.Tools;
using System;
using System.Text;
namespace Pangya_GameServer.Models.Data
{
   public class WarehouseData
    {
        public PlayerItem Header;
        public uint? ItemClubPoint { get; set; }

        public uint? ItemClubWorkCount { get; set; }

        public uint? ItemClubPointLog { get; set; }

        public uint? ItemClubPangLog { get; set; }

        public ushort ItemC0Slot { get; set; }

        public ushort ItemC1Slot { get; set; }

        public ushort ItemC2Slot { get; set; }

        public ushort ItemC3Slot { get; set; }

        public ushort ItemC4Slot { get; set; }

        public uint? ItemClubSlotCancelledCount { get; set; }

        public byte ItemGroup { get; set; }
        public bool ItemNeedUpdate { get; set; }
        public byte ItemIsValid { get; set; }

        public WarehouseData(uint ItemIndex, uint ItemTypeID)
        {
            Header.ItemIndex = ItemIndex;
            Header.ItemTypeID = ItemTypeID;
        }
        public WarehouseData() { }

        public WarehouseData(PlayerItem item)
        {
            Header = item;
            ItemGroup = (byte)item.ItemTypeID.GetItemGroup();
            ItemIsValid = 1;
        }
        public bool AddClubPoint(uint amount)
        {
            if (!(ItemClubPoint > 99999)) return false;
            ItemClubPoint += amount;
            Update();
            return true;
        }

        public bool Update(WarehouseData item)
        {
            Header = item.Header;
            this.ItemClubPoint = item.ItemClubPoint;
            this.ItemClubWorkCount = item.ItemClubWorkCount;
            this.ItemClubPointLog = item.ItemClubPointLog;
            this.ItemClubPangLog = item.ItemClubPangLog;
            this.ItemC0Slot = item.ItemC0Slot;
            this.ItemC1Slot = item.ItemC1Slot;
            this.ItemC2Slot = item.ItemC2Slot;
            this.ItemC3Slot = item.ItemC3Slot;
            this.ItemC4Slot = item.ItemC4Slot;
            this.ItemClubSlotCancelledCount = item.ItemClubSlotCancelledCount;
            return true;
        }

        public bool ClubAddSlot(byte AddType, byte Count = 1)
        {
            if (!(IFFTools.GetItemGroup(Header.ItemTypeID) == IffGroupFlag.ITEM_TYPE_CLUB)) return false;

            ItemClubWorkCount += 1;
            switch (AddType)
            {
                case 0:
                    {
                        ItemC0Slot++;
                        Count++;
                    }
                    break;
                case 1:
                    {
                        ItemC1Slot++;
                        Count++;
                    }
                    break;
                case 2:
                    {
                        ItemC2Slot++;
                        Count++;
                    }
                    break;
                case 3:
                    {
                        ItemC3Slot++;
                        Count++;
                    }
                    break;
                case 4:
                    {
                        ItemC0Slot++;
                        Count++;
                    }
                    break;
            }
            Update();
            return true;
        }

        public bool RemoveClubPoint(uint amount)
        {
            if (ItemClubPoint < amount) return false;

            ItemClubPoint -= amount;
            Update();
            return true;
        }

        public bool RemoveClubSlot(byte RemoveType, byte Count = 1)
        {
            if (!(IFFTools.GetItemGroup(Header.ItemTypeID) == IffGroupFlag.ITEM_TYPE_CLUB)) return false;

            ItemClubWorkCount--;

            if (ItemClubSlotCancelledCount >= 5)
            {
                return false;
            }
            ItemClubSlotCancelledCount++;

            switch (RemoveType)
            {
                case 0:
                    {
                        ItemC0Slot -= Count;
                    }
                    break;
                case 1:
                    {
                        ItemC1Slot -= Count;
                    }
                    break;
                case 2:
                    {
                        ItemC2Slot -= Count;
                    }
                    break;
                case 3:
                    {
                        ItemC3Slot -= Count;
                    }
                    break;
                case 4:
                    {
                        ItemC0Slot -= Count;
                    }
                    break;
            }
            Update();
            return true;
        }

        public bool AddQuantity(uint Value)
        {
            Header.ItemC0 += (ushort)Value;

            Update();
            return true;
        }

        public bool ClubAddStatus(byte Slot)
        {
            Update();
            switch (Slot)
            {
                case 0:
                    {
                        ItemC0Slot += 1;
                    }
                    break;
                case 1:
                    {
                        ItemC1Slot += 1;
                    }
                    break;
                case 2:
                    {
                        ItemC2Slot += 1;
                    }
                    break;
                case 3:
                    {
                        ItemC3Slot += 1;
                    }
                    break;
                case 4:
                    {
                        ItemC0Slot += 1;
                    }
                    break;
            }
            return true;
        }

        public bool ClubRemoveStatus(byte Slot)
        {
            Update();
            switch (Slot)
            {
                case 0:
                    {
                        if (Header.ItemC0 > 0)
                        {
                            Header.ItemC0 -= 1;
                            return true;
                        }
                        return false;
                    }
                case 1:
                    {
                        if (Header.ItemC1 > 0)
                        {
                            Header.ItemC1 -= 1;
                            return true;
                        }
                        return false;
                    }
                case 2:
                    {
                        if (Header.ItemC2 > 0)
                        {
                            Header.ItemC2 -= 1;
                            return true;
                        }
                        return false;
                    }
                case 3:
                    {
                        if (Header.ItemC3 > 0)
                        {
                            Header.ItemC3 -= 1;
                            return true;
                        }
                        return false;
                    }
                case 4:
                    {
                        if (Header.ItemC4 > 0)
                        {
                            Header.ItemC4 -= 1;
                            return true;
                        }
                        return false;
                    }
            }
            return true;
        }

        public bool ClubSetCanReset()
        {
            if (!(ItemGroup == (byte)IffGroupFlag.ITEM_TYPE_CLUB)) return false;

            return true;
        }

        public bool ClubSetReset()
        {
            if (!(ItemGroup == (byte)IffGroupFlag.ITEM_TYPE_CLUB)) return false;

            Header.ItemC0 = 0;
            Header.ItemC1 = 0;
            Header.ItemC2 = 0;
            Header.ItemC3 = 0;
            Header.ItemC4 = 0;

            ItemC0Slot = 0;
            ItemC1Slot = 0;
            ItemC2Slot = 0;
            ItemC3Slot = 0;
            ItemC4Slot = 0;

            ItemClubSlotCancelledCount = 0;

            ItemClubPointLog = 0;
            ItemClubPangLog = 0;
            Update();
            return true;
        }

        public ClubUpgradeData ClubSlotAvailable(byte Slot)
        {
            const uint Power = 2100, Con = 1700, Impact = 2400, Spin = 1900, Curve = 1900;


            var ClubData = new ClubStatus(Header.ItemTypeID);

            switch (Slot)
            {
                case 0:
                    {
                        if (Header.ItemC0 < (ClubData.Power + this.ItemC0Slot))
                        {
                            var result = new ClubUpgradeData()
                            {
                                Able = true,
                                Pang = (Header.ItemC0 * Power) + Power
                            };
                            return result;
                        }
                    }
                    break;
                case 1:
                    {
                        if (Header.ItemC1 < (ClubData.Control + ItemC1Slot))
                        {
                            var result = new ClubUpgradeData()
                            {
                                Able = true,
                                Pang = (Header.ItemC1 * Con) + Con
                            };
                            return result;
                        }
                    }
                    break;
                case 2:
                    {
                        if (Header.ItemC2 < (ClubData.Impact + Header.ItemC2))
                        {
                            var result = new ClubUpgradeData()
                            {
                                Able = true,
                                Pang = (Header.ItemC2 * Impact) + Impact
                            };
                            return result;
                        }
                    }
                    break;
                case 3:
                    {
                        if (Header.ItemC3 < (ClubData.Spin + Header.ItemC3))
                        {
                            var result = new ClubUpgradeData()
                            {
                                Able = true,
                                Pang = (Header.ItemC3 * Spin) + Spin
                            };
                            return result;
                        }
                    }
                    break;
                case 4:
                    {
                        if (Header.ItemC4 < (ClubData.Curve + Header.ItemC4))
                        {
                            var result = new ClubUpgradeData()
                            {
                                Able = true,
                                Pang = (Header.ItemC4 * Curve) + Curve
                            };
                            return result;
                        }
                    }
                    break;
            }

            return new ClubUpgradeData();
        }


        public void CreateNewItem()
        {
       
            this.ItemClubPoint = 0;
            this.ItemClubWorkCount = 0;
            this.ItemClubPointLog = 0;
            this.ItemClubPangLog = 0;
            this.ItemC0Slot = 0;
            this.ItemC1Slot = 0;
            this.ItemC2Slot = 0;
            this.ItemC3Slot = 0;
            this.ItemC4Slot = 0;
            this.ItemClubSlotCancelledCount = 0;
            this.ItemNeedUpdate = false;
            this.ItemIsValid = 1;
            this.ItemNeedUpdate = false;
            this.Header.ItemFlag = 0;
            this.Header.ItemEndDate = DateTime.Now.UnixTimeConvert();
            switch (IFFTools.GetItemGroup(this.Header.ItemTypeID))
            {
                case IffGroupFlag.ITEM_TYPE_CLUB:
                case IffGroupFlag.ITEM_TYPE_PART:
                    {
                        this.Header.ItemC0 = 0;
                    }
                    break;
            }
        }

        public void DeleteItem()
        {
            this.ItemIsValid = 0;
            this.ItemNeedUpdate = true;
        }

        public byte[] GetClubInfo()
        {
            var ClubData = new ClubStatus(Header.ItemTypeID);
            var Packet = new PangyaBinaryWriter();

            try
            {
                Packet.WriteStruct(Header, 18);
                Packet.WriteUInt16(Convert.ToUInt16(ClubData.Power + this.ItemC0Slot));
                Packet.WriteUInt16(Convert.ToUInt16(ClubData.Control + this.ItemC1Slot));
                Packet.WriteUInt16(Convert.ToUInt16(ClubData.Impact + this.ItemC2Slot));
                Packet.WriteUInt16(Convert.ToUInt16(ClubData.Spin + this.ItemC3Slot));
                Packet.WriteUInt16(Convert.ToUInt16(ClubData.Curve + ItemC4Slot));
                var GetBytes = Packet.GetBytes();
                return GetBytes;
            }
            finally
            {
                Packet.Dispose();
            }
        }

        public uint GetClubPoint()
        {
            return (uint)this.ItemClubPoint;
        }

        //public ClubStatus GetClubSlotStatus()
        //{
        //    ClubStatus Result = new ClubStatus();
        //    var club = IffEntry.Club.GetItem(ItemTypeID);
        //    if (club.Base.TypeID == ItemTypeID)
        //    {
        //        if (ItemC0Slot == 0)
        //        {
        //            Result.Power = club.C0;
        //        }
        //        if (ItemC1Slot == 0)
        //        {
        //            Result.Control = club.C1;
        //        }
        //        if (ItemC2Slot == 0)
        //        {
        //            Result.Impact = club.C2;
        //        }
        //        if (ItemC3Slot == 0)
        //        {
        //            Result.Spin = club.C3;
        //        }
        //        if (ItemC4Slot == 0)
        //        {
        //            Result.Curve = club.C4;
        //        }
        //        Result.ClubType = (ECLUBTYPE)club.ClubType;
        //    }
        //    else
        //    {
        //        Result.Power = this.ItemC0Slot;
        //        Result.Control = this.ItemC1Slot;
        //        Result.Impact = this.ItemC2Slot;
        //        Result.Spin = this.ItemC3Slot;
        //        Result.Curve = this.ItemC4Slot;
        //    }
        //    return Result;
        //}

        public byte[] GetItems()
        {
            using (var result = new PangyaBinaryWriter())
            {
                result.WriteStruct(this.Header);

                //if ((IffGroupFlag)ItemGroup == IffGroupFlag.ITEM_TYPE_CLUB)
                //{
                //    result.WriteStruct(this.Header, 180);
                //    result.Write(Convert.ToUInt32(ItemClubPoint ?? 0));
                //    result.Write(Convert.ToUInt32(ItemClubSlotCancelledCount ?? 0));
                //    result.Write(Convert.ToUInt64(ItemClubWorkCount??0));
                //}
                //else
                //{
                //}
                return result.GetBytes();
            }
        }

        public bool RemoveQuantity(uint Value)
        {
            if (Header.ItemTypeID == 467664918)
            {
                return true;
            }
            Header.ItemC0 -= (ushort)Value;
            if (Header.ItemC0 <= 0)
            {
                ItemIsValid = 0;
            }
            Update();
            return true;
        }
        public void Renew()
        {
            this.Header.ItemEndDate = DateTime.Now.AddDays(7).UnixTimeConvert();
            this.Header.ItemFlag = 0x60;
            this.ItemNeedUpdate = true;
        }

        public bool SetItemInformations(WarehouseData Info)
        {
            Header = Info.Header;         
            ItemClubPoint = Info.ItemClubPoint;
            ItemClubWorkCount = Info.ItemClubWorkCount;
            ItemClubPointLog = Info.ItemClubPointLog;
            ItemClubPangLog = Info.ItemClubPangLog;
            ItemC0Slot = Info.ItemC0Slot;
            ItemC1Slot = Info.ItemC1Slot;
            ItemC2Slot = Info.ItemC2Slot;
            ItemC3Slot = Info.ItemC3Slot;
            ItemC4Slot = Info.ItemC4Slot;
            ItemClubSlotCancelledCount = Info.ItemClubSlotCancelledCount;
            ItemGroup = (byte)IFFTools.GetItemGroup(Info.Header.ItemTypeID);
            ItemIsValid = Info.ItemIsValid;
            ItemNeedUpdate = false;
            return true;
        }

        public void Update()
        {
            if (!ItemNeedUpdate)
            { ItemNeedUpdate = true; }
        }

        public string GetSqlUpdateString()
        {
            StringBuilder SQLString;
            SQLString = new StringBuilder();

            try
            {
                SQLString.Append('^');
                SQLString.Append(Header.ItemIndex);
                SQLString.Append('^');
                SQLString.Append(Header.ItemC0);
                SQLString.Append('^');
                SQLString.Append(Header.ItemC1);
                SQLString.Append('^');
                SQLString.Append(Header.ItemC2);
                SQLString.Append('^');
                SQLString.Append(Header.ItemC3);
                SQLString.Append('^');
                SQLString.Append(Header.ItemC4);
                SQLString.Append('^');
                SQLString.Append(ItemIsValid);
                SQLString.Append('^');
                SQLString.Append(Compare.IfCompare<byte>(IFFEntry.IsSelfDesign(Header.ItemTypeID), 1, 0));
                SQLString.Append('^');
                SQLString.Append(Header.ItemUCCStatus ?? 0);
                SQLString.Append('^');
                SQLString.Append(Header.ItemUCCUnique ?? "0");
                SQLString.Append('^');
                SQLString.Append(Header.ItemEndDate.GetSQLTime());
                SQLString.Append('^');
                SQLString.Append(Header.ItemFlag ?? 0);
                SQLString.Append('^');
                // { CLUB SET DATA }
                SQLString.Append(ItemClubPoint);
                SQLString.Append('^');
                SQLString.Append(ItemClubWorkCount);
                SQLString.Append('^');
                SQLString.Append(ItemClubPointLog);
                SQLString.Append('^');
                SQLString.Append(ItemClubPangLog);
                SQLString.Append('^');
                SQLString.Append(ItemC0Slot);
                SQLString.Append('^');
                SQLString.Append(ItemC1Slot);
                SQLString.Append('^');
                SQLString.Append(ItemC2Slot);
                SQLString.Append('^');
                SQLString.Append(ItemC3Slot);
                SQLString.Append('^');
                SQLString.Append(ItemC4Slot);
                SQLString.Append('^');
                SQLString.Append(ItemClubSlotCancelledCount);
                SQLString.Append('^');
                SQLString.Append(Compare.IfCompare<byte>(IFFTools.GetItemGroup(Header.ItemTypeID) == IffGroupFlag.ITEM_TYPE_CLUB, 1, 0));
                SQLString.Append(',');
                // close for next player
                var data = SQLString.ToString();
                return data;
            }
            finally
            {
                SQLString.Clear();
            }
        }
    }
}
