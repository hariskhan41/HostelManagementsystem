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
    
    public partial class AllotmentDb
    {
        public int AllotmentId { get; set; }
        public string StudentId { get; set; }
        public Nullable<int> Roomid { get; set; }
        public bool Astatus { get; set; }
        public System.DateTime StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
    
        public virtual RoomsDb RoomsDb { get; set; }
        public virtual StudentDb StudentDb { get; set; }
    }
}
