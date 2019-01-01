namespace BurgerPrince.Database
{
    using BurgerPrince.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class BurgerPrinceContext : IdentityDbContext
    {
        // Your context has been configured to use a 'BurgerPrinceContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'BurgerPrince.Database.BurgerPrinceContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'BurgerPrinceContext' 
        // connection string in the application configuration file.
        public BurgerPrinceContext()
            : base("name=BurgerPrinceContext")
        {
        }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}