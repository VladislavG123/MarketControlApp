namespace MarketControlApp
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class AppContext : DbContext
    {
       
        public AppContext()
            : base("name=AppContext")
        {
        }

        
         public DbSet<Person> People { get; set; }
    }

    
}