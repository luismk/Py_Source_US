using PangyaAPI.Helper.BinaryModels;
using PangyaAPI.IFF.Common;
using PangyaAPI.IFF.Flags;
using PangyaAPI.IFF.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
namespace PangyaAPI.IFF.Collections
{
    public class BallCollection : List<Ball>
    {
        #region Fields
        IFFHeader IFF_FILE_HEADER;
        public bool Update { get; set; }
        #endregion

        public bool Load(MemoryStream data)
        {
            Ball Ball;

            if (data == null || data.Length == 0)
            {
                MessageBox.Show(" data\\Ball.iff is not loaded", "Pangya.IFF");
                return false;
            }

            try
            {
                using (var Reader = new PangyaBinaryReader(data))
                {
                    if (new string(Reader.ReadChars(2)) == "PK")
                    {
                        throw new Exception("The given IFF file is a ZIP file, please unpack it before attempting to parse it");
                    }
                    Reader.Seek(0, 0);

                    IFF_FILE_HEADER = (IFFHeader)Reader.Read(new IFFHeader());

                    long recordLength = (Reader.GetSize - 8L) / IFF_FILE_HEADER.RecordCount;

                    var IffStructSize = Tools.IFFTools.SizeStruct(new Ball());
                    if (IffStructSize != recordLength)
                    {
                        throw new Exception($"Ball.iff the structure size is incorrect, Real: {recordLength}, Ball.cs: {IffStructSize} ");
                    }

                    for (int i = 0; i < IFF_FILE_HEADER.RecordCount; i++)
                    {
                        Ball = (Ball)Reader.Read(new Ball());

                        this.Add(Ball);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("[Error Struct]:  " + ex.Message, "Pangya.IFF.Model.Ball");
                return false;
            }
        }

        //Destructor
        ~BallCollection()
        {
            this.Clear();
        }


        public string GetItemName(uint ID)
        {
            foreach (var item in this)
            {
                if (item.Base.TypeID == ID)
                {
                    return item.Base.Name;
                }
            }
            return "";
        }

        public bool IsExist(uint ID)
        {
            Ball ball = new Ball();
            if (!LoadBall(ID, ref ball))
            {
                return false;
            }
            if (ball.Base.Enabled == 1)
            {
                return true;
            }
            return false;
        }

        public uint GetPrice(uint ID)
        {
            Ball ball = new Ball();
            if (!LoadBall(ID, ref ball))
            {
                return 99999999;
            }
            return ball.Base.ItemPrice;
        }


        public sbyte GetShopPriceType(uint ID)
        {
            Ball ball = new Ball();
            if (!LoadBall(ID, ref ball))
            {
                return -1;
            }
            return (sbyte)ball.Base.PriceType;
        }

        public bool IsBuyable(uint ID)
        {
            Ball ball = new Ball();
            if (!LoadBall(ID, ref ball))
            {
                return false;
            }
            if (ball.Base.Enabled == 1 && ball.Base.MoneyFlag == 0 || ball.Base.MoneyFlag == MoneyFlag.Active)
            {
                return true;
            }
            return false;
        }

        public UInt32 GetRealQuantity(UInt32 ID, UInt32 Qty)
        {
            Ball ball = new Ball();
            if (!LoadBall(ID, ref ball))
            {
                return 0;
            }
            if ((ball.Base.Enabled == 1) && (ball.Power > 0))
            {
                return ball.Power;
            }
            return Qty;
        }


        bool LoadBall(uint ID, ref Ball ball)
        {
            var load = this.Where(c => c.Base.TypeID == ID);
            if (load.Any())
            {
                ball = load.First();
                return false;
            }
            return true;
        }

        public Ball LoadBall(uint ID)
        {
            Ball ball = new Ball();
            if (!LoadBall(ID, ref ball))
            {
                return ball;
            }
            return ball;
        }

    }
}
