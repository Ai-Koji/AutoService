﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AutoService.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AutoServiceEntities : DbContext
    {
        private static AutoServiceEntities context;
        public AutoServiceEntities()
            : base("name=AutoServiceEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public static AutoServiceEntities GetContext()
        {
            if (context == null)
                context = new AutoServiceEntities();
            return context;
        }

        public DbSet<Order> Order { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }
        public DbSet<PickupPoint> PickupPoint { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<sysdiagrams> sysdiagrams { get; set; }
        public DbSet<User> User { get; set; }
    }
}
