//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PangyaAPI.SqlConnector.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Daily_Quest
    {
        public int ID { get; set; }
        public int QuestTypeID1 { get; set; }
        public int QuestTypeID2 { get; set; }
        public int QuestTypeID3 { get; set; }
        public Nullable<System.DateTime> RegDate { get; set; }
        public Nullable<byte> Day { get; set; }
    }
}