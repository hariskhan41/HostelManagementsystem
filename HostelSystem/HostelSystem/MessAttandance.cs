//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HostelSystem
{
    using System;
    using System.Collections.Generic;
    
    public partial class MessAttandance
    {
        public int Id { get; set; }
        public Nullable<int> RoomNo { get; set; }
        public string StudentId { get; set; }
        public Nullable<int> FoodId { get; set; }
        public System.DateTime DateMarked { get; set; }
        public int AtdCount { get; set; }
    
        public virtual Fooditem Fooditem { get; set; }
        public virtual RoomsDb RoomsDb { get; set; }
        public virtual StudentDb StudentDb { get; set; }
    }
}
