﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HostelManagementSystemEntities8 : DbContext
    {
        public HostelManagementSystemEntities8()
            : base("name=HostelManagementSystemEntities8")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AllotmentDb> AllotmentDbs { get; set; }
        public virtual DbSet<EmployeeDb> EmployeeDbs { get; set; }
        public virtual DbSet<Fooditem> Fooditems { get; set; }
        public virtual DbSet<HostelDb> HostelDbs { get; set; }
        public virtual DbSet<LoginDb> LoginDbs { get; set; }
        public virtual DbSet<MessAttandance> MessAttandances { get; set; }
        public virtual DbSet<MessMenue> MessMenues { get; set; }
        public virtual DbSet<RoomsDb> RoomsDbs { get; set; }
        public virtual DbSet<StudentDb> StudentDbs { get; set; }
    }
}
