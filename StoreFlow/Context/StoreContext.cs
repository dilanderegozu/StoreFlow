using Microsoft.EntityFrameworkCore;
using StoreFlow.Entities;

namespace StoreFlow.Context
{
    public class StoreContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;initial Catalog=StoreFlowDb;integrated Security=true; trust server certificate=true");
        }
        public DbSet<Category>  Categories { get; set; }
        //yalın isim c# kısmı çoğul sql kısmı 
        public DbSet<Product>  Products { get; set; }
        public DbSet<Customer>  Customers { get; set; }
        public DbSet <Order> Orders { get; set; }
        public DbSet <Activity> Activities { get; set; }

        public DbSet<Todo> Todos { get; set; }
    }
}
