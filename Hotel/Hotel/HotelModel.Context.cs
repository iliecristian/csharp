﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hotel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class HotelEntities : DbContext
    {
        public HotelEntities()
            : base("name=HotelEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Angajat> Angajat { get; set; }
        public virtual DbSet<Camera> Camera { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Dotare> Dotare { get; set; }
        public virtual DbSet<Imagine> Imagine { get; set; }
        public virtual DbSet<Oferta> Oferta { get; set; }
        public virtual DbSet<Rezervare> Rezervare { get; set; }
        public virtual DbSet<ServiciiSuplimentare> ServiciiSuplimentare { get; set; }
    
        public virtual ObjectResult<GetAllClients_Result> GetAllClients()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllClients_Result>("GetAllClients");
        }
    }
}
