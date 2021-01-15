using System;
namespace Pangya_GameServer.Common
{
    public struct AddItemData
    {
        public bool Status { get; set; }
        public uint ItemIndex { get; set; }
        public uint ItemTypeID { get; set; }
        public uint ItemOldQty { get; set; }
        public uint ItemNewQty { get; set; }
        public string ItemUCCKey { get; set; }
        public byte ItemFlag { get; set; }
        public DateTime? ItemEndDate { get; set; }
        // AddData
        public void SetData(bool FStatus, uint FItemIndex, uint FItemTypeId, uint FItemOldQty, uint FItemNewQty, string FItemUCCKey, Byte FItemFlag, DateTime FItemEndDate)
        {
            this.Status = FStatus;
            this.ItemIndex = FItemIndex;
            this.ItemTypeID = FItemTypeId;
            this.ItemOldQty = FItemOldQty;
            this.ItemNewQty = FItemNewQty;
            this.ItemUCCKey = FItemUCCKey;
            this.ItemFlag = FItemFlag;
            this.ItemEndDate = FItemEndDate;
        }

        public AddItemData Create()
        {
            SetData(false, 0, 0, 0, 0, string.Empty, 0, DateTime.MinValue);
            return this;
        }
    }
}