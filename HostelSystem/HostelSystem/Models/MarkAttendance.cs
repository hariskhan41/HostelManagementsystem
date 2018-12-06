using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace HostelSystem.Models
{
    public class MarkAttendance
    {
        public int Id { get; set; }
        public Nullable<int> RoomNo { get; set; }
        public string StudentId { get; set; }
        public string regNo { get; set; }
        public string name { get; set; }
        public Nullable<int> FoodId { get; set; }
        public System.DateTime DateMarked { get; set; }
        public int AtdCount { get; set; }

        public string FoodName { get; set; }
    }
}