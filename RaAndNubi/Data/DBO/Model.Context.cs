﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RaAndNubi.Data.DBO
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RaAndNubiEntities : DbContext
    {
        public RaAndNubiEntities()
            : base("name=RaAndNubiEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Nubi> Nubi { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Ra> Ra { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
